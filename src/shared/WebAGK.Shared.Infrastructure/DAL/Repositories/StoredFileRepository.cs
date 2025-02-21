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
		var storedFile = new StoredFile();
		var fileInfo = new FileInfo(file.FileName);
		var filePath = Path.Combine(_filesFolder, storedFile.Id.ToString() + fileInfo.Extension);
		await using var stream = new FileStream(filePath, FileMode.Create);
		await file.CopyToAsync(stream, cancellationToken);

		storedFile.FileName = file.FileName;
		storedFile.FileStoragePath = filePath;
		storedFile.FileStorageName = storedFile.Id.ToString() + Path.GetExtension(file.FileName);
	
		await _storedFiles.AddAsync(storedFile, cancellationToken);
		await _context.SaveChangesAsync(cancellationToken);
	
		return storedFile.Id;	
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
		var file = await _storedFiles.SingleOrDefaultAsync(x => x.Id == id, cancellationToken)
			?? throw new FileNotFoundException();

		if (!File.Exists(file.FileStoragePath)) throw new FileNotFoundException(file.FileName);
		
		var provider = new FileExtensionContentTypeProvider();
		if(!provider.TryGetContentType(file.FileStorageName, out var contentType)) {
			contentType = "application/octet-stream";
		}
		var bytes = await File.ReadAllBytesAsync(file.FileStorageName, cancellationToken);
		return (bytes, contentType, file.FileName);
	}
}
