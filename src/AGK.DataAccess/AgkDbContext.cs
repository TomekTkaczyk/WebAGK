using AGK.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AGK.DataAccess;

public sealed class AgkDbContext(DbContextOptions<AgkDbContext> dbContextOptions) : DbContext(dbContextOptions)
{
	// public DbSet<User> Users { get; set; }

}
