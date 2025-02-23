using WebAGK.Module.Agents.Core.Entities;
using WebAGK.Shared.Abstractions.Repositories;

namespace WebAGK.Module.Agents.Core.Repositories;

public interface IAgentRepository : IActiveStatusRepository<Agent>;