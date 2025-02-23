using WebAGK.Module.Agents.Core.Entities;
using WebAGK.Shared.Infrastructure.Repositories;

namespace WebAGK.Module.Agents.UseCases.Specifications;

public class GetByTaxIdSpecification(string taxId) 
    : Specification<Agent>(agent => agent.TaxId == taxId);