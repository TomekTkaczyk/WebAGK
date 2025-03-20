using WebAGK.Module.Insurers.Core.Entities;
using WebAGK.Shared.Abstractions.Repositories;
using WebAGK.Shared.Infrastructure.Entities;

namespace WebAGK.Module.Insurers.Core.Repositories;

internal interface INodeRepository : IRepository<Node<Agent>> {
    
}