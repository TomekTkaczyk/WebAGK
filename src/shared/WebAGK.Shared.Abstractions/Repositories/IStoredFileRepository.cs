using Microsoft.AspNetCore.Http;
using WebAGK.Shared.Abstractions.Entities;

namespace WebAGK.Shared.Abstractions.Repositories;
public interface IStoredFileRepository
{
	Task<Guid> AddAsync(IFormFile file, CancellationToken cancellationToken);
	
	Task<IStoredFile> GetAsync(Guid id, CancellationToken cancellationToken);
	
	Task DeleteAsync(IStoredFile file, CancellationToken cancellationToken);
	
	Task<(byte[], string, string)> GetFileAsync(Guid id, CancellationToken cancellationToken);
}
