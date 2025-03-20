using WebAGK.Module.Insurers.Core.Entities;
using WebAGK.Module.Insurers.Core.Repositories;
using WebAGK.Shared.Infrastructure.Entities;
using WebAGK.Shared.Infrastructure.Repositories;

namespace WebAGK.Module.Insurers.Core.DAL.Repositories;

internal class NodeRepository(InsurersDbContext dbContext)
    : Repository<Node<Agent>, InsurersDbContext>(dbContext), INodeRepository {
}