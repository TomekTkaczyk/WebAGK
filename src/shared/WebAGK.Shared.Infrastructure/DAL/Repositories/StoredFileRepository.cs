using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WebAGK.Shared.Abstractions;
using WebAGK.Shared.Abstractions.Entities;
using WebAGK.Shared.Abstractions.Repositories;
using WebAGK.Shared.Infrastructure.Entities;

namespace WebAGK.Shared.Infrastructure.DAL.Repositories;
internal class StoredFileRepository : IStoredFileRepository
{
	private readonly InfrastructureDbContext _context;
	private readonly IClock _clock;
	private readonly DbSet<StoredFile> _storedFiles;
	private readonly string _filesFolder;

	public StoredFileRepository(
		InfrastructureDbContext context,
		IConfiguration configuration,
		IClock clock)
	{
		_context = context;
		_clock = clock;
		_storedFiles = context.Set<StoredFile>();

		_filesFolder = Path.Combine(
			AppDomain.CurrentDomain.BaseDirectory,
			configuration.GetSection("StoredFilePath:path").Value);

		if(Directory.Exists(_filesFolder) is false) {
			if(Directory.CreateDirectory(_filesFolder).Exists is false) {
				throw new DirectoryNotFoundException();
			}
		}
	}
	
	public async Task<Guid> AddAsync(IFormFile file, CancellationToken cancellationToken) {
		var _storedFile = new StoredFile();
		var _fileInfo = new FileInfo(file.FileName);
		var _filePath = Path.Combine(_filesFolder, _storedFile.Id.ToString() + _fileInfo.Extension);
		await using var _stream = new FileStream(_filePath, FileMode.Create);
		await file.CopyToAsync(_stream, cancellationToken);

		_storedFile.FileName = file.FileName;
		_storedFile.FileStoragePath = _filePath;
		_storedFile.FileStorageName = _storedFile.Id + Path.GetExtension(file.FileName);
	
		await _storedFiles.AddAsync(_storedFile, cancellationToken);
		await _context.SaveChangesAsync(cancellationToken);
	
		return _storedFile.Id;	
	}
		
	public async Task<IStoredFile> GetAsync(Guid id, CancellationToken cancellationToken) 
		=> await _storedFiles.SingleOrDefaultAsync(x => x.Id == id, cancellationToken);
	
	public async Task DeleteAsync(IStoredFile file, CancellationToken cancellationToken) {
		_storedFiles.Remove(file as StoredFile ?? throw new Exception());
		await _context.SaveChangesAsync(cancellationToken);
		if(File.Exists(file.FileStoragePath)) { 
			File.Delete(file.FileStoragePath);
		}
	}
	
	public async Task<(byte[], string, string)> GetFileAsync(Guid id, CancellationToken cancellationToken) {
		var _file = await _storedFiles.SingleOrDefaultAsync(x => x.Id == id, cancellationToken)
			?? throw new FileNotFoundException();

		if (!File.Exists(_file.FileStoragePath)) throw new FileNotFoundException(_file.FileName);
		
		var _provider = new FileExtensionContentTypeProvider();
		if(!_provider.TryGetContentType(_file.FileStorageName, out var _contentType)) {
			_contentType = "application/octet-stream";
		}
		var _bytes = await File.ReadAllBytesAsync(_file.FileStorageName, cancellationToken);
		return (_bytes, _contentType, _file.FileName);
	}
}
