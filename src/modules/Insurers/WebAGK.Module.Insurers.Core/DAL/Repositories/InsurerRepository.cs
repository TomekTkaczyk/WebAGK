using WebAGK.Module.Insurers.Core.Entities;
using WebAGK.Module.Insurers.Core.Repositories;
using WebAGK.Shared.Infrastructure.Repositories;

namespace WebAGK.Module.Insurers.Core.DAL.Repositories;

internal class InsurerRepository(
    IStructureRepository structureRepository,
    INodeRepository nodeRepository,
    InsurersDbContext dbContext)
    : Repository<Insurer, InsurersDbContext>(dbContext), IInsurerRepository {
    
    private readonly InsurersDbContext _dbContext = dbContext;

    public void ClearStructure(Insurer insurer) {
        foreach (var _node in insurer.Structure.Nodes) {
            nodeRepository.Delete(_node);
        }
    }
}