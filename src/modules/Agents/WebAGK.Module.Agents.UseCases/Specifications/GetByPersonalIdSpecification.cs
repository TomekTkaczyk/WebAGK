using WebAGK.Module.Agents.Core.Entities;
using WebAGK.Shared.Infrastructure.Repositories;

namespace WebAGK.Module.Agents.UseCases.Specifications;

public class GetByPersonalIdSpecification(string personalId) 
    : Specification<Agent>(agent => agent.PersonalId.Equals(personalId));