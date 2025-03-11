using WebAGK.Module.Insurers.Core.DAL.DbModels;
using WebAGK.Module.Insurers.Core.Entities;
using WebAGK.Module.Insurers.Core.Repositories;
using WebAGK.Shared.Abstractions.Repositories;
using WebAGK.Shared.Infrastructure.Repositories;

namespace WebAGK.Module.Insurers.Core.DAL.Repositories;

public class InsurerRepository(InsurersDbContext dbContext) 
    : Repository<InsurerDb, InsurersDbContext>(dbContext), IInsurerRepository{
    
    public Insurer Add(Insurer entity) {
        return base.Add(entity.ToDbModel()).ToDomainModel();
    }
    public void Update(Insurer entity) {
        base.Update(entity.ToDbModel());
    }
    public void Delete(Insurer entity) {
        base.Delete(entity.ToDbModel());
    }

    public IQueryable<Insurer> Get(ISpecification<Insurer> specification = null) {
        var _spec = specification.ToDbSpecification();
        return base.Get(_spec)
            .Select(x => x.ToDomainModel());
    }

    public IQueryable<Insurer> GetPage(ISpecification<Insurer> specification, int pageNumber, int pageSize) {
        var _spec = specification.ToDbSpecification();
        return base.GetPage(_spec, pageNumber, pageSize).Select(x => x.ToDomainModel());
    }
    public IQueryable<Insurer> GetPage(IQueryable<Insurer> query, int pageNumber, int pageSize) {
        return base.GetPage(query.Select(x => x.ToDbModel()), pageNumber, pageSize).Select(x => x.ToDomainModel());
    }
}
