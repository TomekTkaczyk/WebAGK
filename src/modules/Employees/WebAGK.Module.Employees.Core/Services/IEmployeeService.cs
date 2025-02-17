using Microsoft.AspNetCore.Http;
using WebAGK.Module.Employees.Core.DTO;

namespace WebAGK.Module.Employees.Core.Services;

internal interface IEmployeeService
{
	Task AddAsync(EmployeeDto dto, CancellationToken cancellationToken);
	Task<EmployeeDetailsDto> GetAsync(Guid id, CancellationToken cancellationToken);
	Task<IReadOnlyList<EmployeeDto>> GetAllAsync(CancellationToken cancellationToken);
	Task UpdateAsync(EmployeeDetailsDto dto, CancellationToken cancellationToken);
	Task DeleteAsync(Guid id, CancellationToken cancellationToken);
	Task<Guid> AddFileAsync(IFormFile file, CancellationToken cancellationToken);
}
