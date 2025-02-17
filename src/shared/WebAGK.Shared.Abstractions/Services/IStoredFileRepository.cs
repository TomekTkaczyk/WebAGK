using Microsoft.AspNetCore.Http;
using WebAGK.Shared.Abstractions.Entities;

namespace WebAGK.Shared.Abstractions.Services;
public interface IStoredFileRepository
{
	Task<Guid> AddAsync(IFormFile file, CancellationToken cancellationToken);
	Task<StoredFile> GetAsync(Guid id, CancellationToken cancellationToken);
	Task DeleteAsync(StoredFile file, CancellationToken cancellationToken);
	Task<(byte[], string, string)> GetFileAsync(Guid id, CancellationToken cancellationToken);
}
