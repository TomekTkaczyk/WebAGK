using WebAGK.Module.Insurers.Core.DAL.DbModels;
using WebAGK.Module.Insurers.Core.Entities;
using WebAGK.Shared.Abstractions.Repositories;

namespace WebAGK.Module.Insurers.Core.Repositories;

public interface IInsurerRepository : IRepository<InsurerDb> {
    
    Insurer Add(Insurer entity);

    void Update(Insurer entity);

    void Delete(Insurer entity);

    IQueryable<Insurer> Get(ISpecification<Insurer> specification = null);
    
    IQueryable<Insurer> GetPage(ISpecification<Insurer> specification, int pageNumber, int pageSize);

    IQueryable<Insurer> GetPage(IQueryable<Insurer> query, int pageNumber, int pageSize);
};