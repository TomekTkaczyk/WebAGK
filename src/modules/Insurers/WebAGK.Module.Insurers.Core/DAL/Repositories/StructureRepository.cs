using WebAGK.Module.Insurers.Core.Entities;
using WebAGK.Module.Insurers.Core.Repositories;
using WebAGK.Shared.Infrastructure.Repositories;

namespace WebAGK.Module.Insurers.Core.DAL.Repositories;

internal class StructureRepository(
    IInsurerRepository insurerRepository,
    IAgentRepository agentRepository,
    InsurersDbContext dbContext)
    : Repository<Structure, InsurersDbContext>(dbContext), IStructureRepository {
    //
    // public async Task AddSubAgent(StructureNodeDbEntity structureNode, CancellationToken cancellationToken)
    // {
    //     var _updateQuery = $"UPDATE Agents SET Right = Right + 2 WHERE Right >= {structureNode.Left}";
    //     await dbContext.Database.ExecuteSqlRawAsync(_updateQuery, cancellationToken);
    //     _updateQuery = $"UPDATE Agents SET Left = Left + 2 WHERE Left > {structureNode.Left}";
    //     await dbContext.Database.ExecuteSqlRawAsync(_updateQuery, cancellationToken);
    //     
    //     structureNode.Left += 1;
    //     structureNode.Right = structureNode.Left + 2;
    //     repository.Add(structureNode);
    //     await dbContext.SaveChangesAsync(cancellationToken);
    // }
    //
    // public async Task RemoveSubAgent(StructureNodeDbEntity structureNode, CancellationToken cancellationToken)
    // {
    //     var _structureNode = await repository
    //         .Get(new ByIdSpecification<StructureNodeDbEntity>(structureNode.Id))
    //         .SingleOrDefaultAsync(cancellationToken)
    //         ?? throw new AgentNotFoundException();
    //
    //     var _width = _structureNode.Right - _structureNode.Left + 1;
    //     var _deleteQuery = $"DELETE FROM Agents WHERE Left BETWEEN {structureNode.Left} AND {structureNode.Right}";
    //     await dbContext.Database.ExecuteSqlRawAsync(_deleteQuery, cancellationToken);
    //     
    //     var _updateQuery = $"UPDATE Agents SET Right = Right - {_width} WHERE Right > {structureNode.Right}";
    //     await dbContext.Database.ExecuteSqlRawAsync(_updateQuery, cancellationToken);
    //     _updateQuery = $"UPDATE Agents SET Left = Left - {_width} WHERE Left > {structureNode.Right}";
    //     await dbContext.Database.ExecuteSqlRawAsync(_updateQuery, cancellationToken);
    // }
    //
    // public async Task MoveSubAgent(StructureNodeDbEntity structureNode, StructureNodeDbEntity newParent, CancellationToken cancellationToken)
    // {
    //     await RemoveSubAgent(structureNode, cancellationToken);
    //     var _agent = dbContext.Agents.FirstOrDefault(a => a.Id == structureNode.Id);
    //     if (_agent != null)
    //     {
    //         newParent.AddSubAgent(_agent, dbContext);
    //     }
    // }

}
