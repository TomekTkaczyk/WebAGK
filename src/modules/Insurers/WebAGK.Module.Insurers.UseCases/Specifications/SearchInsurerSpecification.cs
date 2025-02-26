using Microsoft.EntityFrameworkCore;
using WebAGK.Module.Insurers.Core.Entities;
using WebAGK.Shared.Infrastructure.Repositories;

namespace WebAGK.Module.Insurers.UseCases.Specifications;

internal sealed class SearchInsurerSpecification(string searchText = null, bool? activeStatus = null) 
    : Specification<Insurer>(
    insurer => (activeStatus == null || insurer.ActiveStatus == activeStatus)
    && (string.IsNullOrWhiteSpace(searchText)
    || EF.Functions.ILike(insurer.Name,$"%{searchText}%")
    || EF.Functions.ILike(insurer.Description,$"%{searchText}%")))
{ }