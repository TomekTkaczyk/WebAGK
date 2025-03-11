using System.Linq.Expressions;
using WebAGK.Module.Insurers.Core.DAL.DbModels;
using WebAGK.Module.Insurers.Core.Entities;
using WebAGK.Shared.Infrastructure.Repositories;
using WebAGK.Shared.Infrastructure.Types;

namespace WebAGK.Module.Insurers.Core.DAL.Repositories;

public static class DomainDatabaseMapper {
    
    public static InsurerDb ToDbModel(this Insurer insurer) {
        if (insurer == null) {
            return null;
        }

        return new InsurerDb {
            Id = insurer.Id,
            Name = insurer.Name,
            Description = insurer.Description,
            Structure = insurer.Structure?.Select(node => node.ToDbModel(insurer.Id, null)).ToList()
        };
    }

    public static NodeDb ToDbModel(this Node<Agent> node, Guid insurerId, NodeDb parent = null) {
        if (node == null) {
            return null;
        }

        return new NodeDb {
            Id = Guid.NewGuid(),
            InsurerId = insurerId,
            ParentId = parent?.Id,
            AgentId = (node.Value as Agent)?.Id,
            Left = 0,
            Right = 0
         };
    }
    
    public static Insurer ToDomainModel(this InsurerDb insurerDb)
    {
        if (insurerDb == null) {
            return null;
        }

        var _insurer = Insurer.Create(insurerDb.Name, insurerDb.Description);
        _insurer.Structure = insurerDb.Structure?
            .Select(node => node.ToDomainModel(null))
            .ToList();

        return _insurer;
    }

    public static Node<Agent> ToDomainModel(this NodeDb nodeDb, Node<Agent> parent)
    {
        if (nodeDb == null) return null;

        var _domainNode = new Node<Agent>
        {
            Value = nodeDb.Agent,
            Parent = parent,
        };
        _domainNode.Nodes = nodeDb.Nodes?
            .Select(x => x.ToDomainModel(_domainNode))
            .ToList();

        return _domainNode;
    }
    
    public static void ApplyChanges(this InsurerDb insurerDb, Insurer insurer)
    {
        if (insurerDb == null || insurer == null) {
            return;
        }

        insurerDb.Name = insurer.Name;
        insurerDb.Description = insurer.Description;

        // Synchronizacja drzewa
        insurerDb.Structure = SyncNodes(insurerDb.Structure, insurer.Structure, insurerDb.Id, null);
    }

    private static ICollection<NodeDb> SyncNodes(
        ICollection<NodeDb> dbNodes, 
        ICollection<Node<Agent>> domainNodes, 
        Guid insurerId, 
        NodeDb parent)
    {
        if (domainNodes == null) {
            return dbNodes;
        }

        var _updatedNodes = new List<NodeDb>();

        foreach (var _domainNode in domainNodes)
        {
            var _existingNode = dbNodes?
                .FirstOrDefault(n => n.AgentId == _domainNode.Value?.Id);
            if (_existingNode == null) {
                _existingNode = _domainNode.ToDbModel(insurerId, parent);
            } else {
                _existingNode.Nodes = SyncNodes(_existingNode.Nodes, _domainNode.Nodes, insurerId, _existingNode);
            }

            _updatedNodes.Add(_existingNode);
        }

        return _updatedNodes;
    }
}