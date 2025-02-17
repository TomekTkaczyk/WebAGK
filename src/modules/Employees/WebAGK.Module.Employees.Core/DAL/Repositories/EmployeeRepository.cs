using Microsoft.EntityFrameworkCore;
using WebAGK.Module.Employees.Core.Entities;
using WebAGK.Module.Employees.Core.Repositories;

namespace WebAGK.Module.Employees.Core.DAL.Repositories;
internal class EmployeeRepository(EmployeesDbContext context) : IEmployeeRepository
{
	private readonly EmployeesDbContext _context = context;
	private readonly DbSet<Employee> _employees = context.Set<Employee>();

	public Task<Employee> GetAsync(Guid id, CancellationToken cancellationToken) => _employees.SingleOrDefaultAsync(p => p.Id == id, cancellationToken);

	public async Task<IReadOnlyList<Employee>> GetAllAsync(CancellationToken cancellationToken) => await _employees.ToListAsync(cancellationToken);

	public async Task AddAsync(Employee employee, CancellationToken cancellationToken)
	{
		_employees.Add(employee);

		await _context.SaveChangesAsync(cancellationToken);
	}

	public async Task DeleteAsync(Employee employee, CancellationToken cancellationToken)
	{
		_employees.Remove(employee);

		await _context.SaveChangesAsync(cancellationToken);
	}


	public async Task UpdateAsync(Employee employee, CancellationToken cancellationToken)
	{
		_employees.Update(employee);

		await _context.SaveChangesAsync(cancellationToken);
	}
}
