using Microsoft.AspNetCore.Http;
using WebAGK.Shared.Abstractions.Services;
using WebAGK.Module.Employees.Core.DTO;
using WebAGK.Module.Employees.Core.Entities;
using WebAGK.Module.Employees.Core.Exceptions;
using WebAGK.Module.Employees.Core.Policies;
using WebAGK.Module.Employees.Core.Repositories;

namespace WebAGK.Module.Employees.Core.Services;

internal class EmployeeService(
	IEmployeeRepository employeeRepository,
	IStoredFileRepository storedFileRepository,
	IEmployeeDeletionPolicy employeeDeletionPolicy) : IEmployeeService
{
	private readonly IEmployeeRepository _employeeRepository = employeeRepository;
	private readonly IEmployeeDeletionPolicy _employeeDeletionPolicy = employeeDeletionPolicy;

	public async Task AddAsync(EmployeeDto dto, CancellationToken cancellationToken)
	{
		dto.Id = Guid.NewGuid();
		await _employeeRepository.AddAsync(new Employee
		{
			Id = dto.Id,
			Firstname = dto.Firstname,
			Lastname = dto.Lastname
		}, cancellationToken);
	}

	public async Task<EmployeeDetailsDto> GetAsync(Guid id, CancellationToken cancellationToken)
	{
		var employee = await _employeeRepository.GetAsync(id, cancellationToken) ?? throw new EmployeeNotFoundException(id);

		var dto = Map<EmployeeDetailsDto>(employee);
		dto.Description = employee.Description;

		return dto;
	}

	public async Task<IReadOnlyList<EmployeeDto>> GetAllAsync(CancellationToken cancellationToken)
	{

		var employees = await _employeeRepository.GetAllAsync(cancellationToken);

		return employees.Select(Map<EmployeeDto>).ToList();
	}

	public async Task UpdateAsync(EmployeeDetailsDto dto, CancellationToken cancellationToken)
	{

		var employee = await _employeeRepository.GetAsync(dto.Id, cancellationToken) ?? throw new EmployeeNotFoundException(dto.Id);

		employee.Firstname = dto.Firstname;
		employee.Lastname = dto.Lastname;
		employee.Description = dto.Description;

		await _employeeRepository.UpdateAsync(employee, cancellationToken);
	}

	public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
	{
		var employee = await _employeeRepository.GetAsync(id, cancellationToken) ?? throw new EmployeeNotFoundException(id);

		if(await _employeeDeletionPolicy.CanDeleteAsync(employee) is false) {
			throw new CannotDeleteEmployeeException(id);
		}

		await _employeeRepository.DeleteAsync(employee, cancellationToken);
	}

	public async Task<Guid> AddFileAsync(IFormFile file, CancellationToken cancellationToken)
	{
		return await storedFileRepository.AddAsync(file, cancellationToken);
	}

	public async Task<(byte[], string, string)> GetFileAsync(Guid id, CancellationToken cancellationToken)
	{
		return await storedFileRepository.GetFileAsync(id, cancellationToken);
	}

	private static T Map<T>(Employee employee) where T : EmployeeDto, new()
	{
		return new T()
		{
			Id = employee.Id,
			Firstname = employee.Firstname,
			Lastname = employee.Lastname,
		};
	}

}
