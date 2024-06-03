using AGK.DataAccess;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

namespace AGK.Tests.Fixtures;

public class AgkDbContextFactory
{
	public static AgkDbContext CreateDbContext(string connectionString) {
		var options = new DbContextOptionsBuilder<AgkDbContext>()
			.UseMySql(
				connectionString,
				new MariaDbServerVersion(ServerVersion.AutoDetect(connectionString)),
				b => {
					b.SchemaBehavior(MySqlSchemaBehavior.Ignore);
				})
			.Options;

		var context = new AgkDbContext(options);

		return context;
	}
}
