using WebAGK.Module.Insurers.Core.DAL.DbModels;
using WebAGK.Module.Insurers.Core.Entities;
using WebAGK.Shared.Abstractions.Repositories;

namespace WebAGK.Module.Insurers.Core.DAL.Repositories;

public static class Extensions {
    public static ISpecification<InsurerDb> ToDbSpecification(this ISpecification<Insurer> domainSpec) {
        return domainSpec == null ? null : new InsurerDbSpecification(domainSpec);
    }
}