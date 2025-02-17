using Microsoft.EntityFrameworkCore;
using WebAGK.Module.Employees.Core.Entities;

namespace WebAGK.Module.Employees.Core.DAL;
internal class EmployeesDbContext(
	DbContextOptions<EmployeesDbContext> options) : DbContext(options)
{
	public DbSet<Employee> Employees { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.HasDefaultSchema("Employees");
		modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
	}
}
