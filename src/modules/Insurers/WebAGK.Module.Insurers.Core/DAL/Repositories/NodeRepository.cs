using WebAGK.Module.Insurers.Core.Entities;
using WebAGK.Module.Insurers.Core.Repositories;
using WebAGK.Shared.Infrastructure.Repositories;

namespace WebAGK.Module.Insurers.Core.DAL.Repositories;

public class NodeRepository(InsurersDbContext dbContext)
    : Repository<Node, InsurersDbContext>(dbContext), INodeRepository;