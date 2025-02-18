using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebAGK.Module.Agents.Core.DAL;
using WebAGK.Module.Agents.Core.DAL.Repositories;
using WebAGK.Module.Agents.Core.Repositories;
using WebAGK.Shared.Infrastructure.Database;

namespace WebAGK.Module.Agents.Core;
internal static class Extension
{
	public static IServiceCollection AddCore(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddScoped<IAgentRepository, AgentRepository>();
		services.AddDatatabase<AgentsDbContext>(configuration);
		
		return services;
	}
}
