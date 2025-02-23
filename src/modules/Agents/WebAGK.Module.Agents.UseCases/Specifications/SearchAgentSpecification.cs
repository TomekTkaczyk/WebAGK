using Microsoft.EntityFrameworkCore;
using WebAGK.Module.Agents.Core.Entities;
using WebAGK.Shared.Infrastructure.Repositories;

namespace WebAGK.Module.Agents.UseCases.Specifications;

internal sealed class SearchAgentSpecification(string searchText = null, bool? activeStatus = null) 
    : Specification<Agent>(
    agent => (activeStatus == null || agent.ActiveStatus == activeStatus)
    && (string.IsNullOrWhiteSpace(searchText)
    || EF.Functions.ILike(agent.LastName,$"%{searchText}%")
    || EF.Functions.ILike(agent.FirstName,$"%{searchText}%")
    || EF.Functions.ILike(agent.SecondName,$"%{searchText}%")
    || EF.Functions.ILike(agent.Description,$"%{searchText}%")))
{ }