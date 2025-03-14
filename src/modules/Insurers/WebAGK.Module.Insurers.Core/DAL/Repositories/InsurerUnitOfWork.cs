using WebAGK.Module.Insurers.Core.Repositories;
using WebAGK.Shared.Abstractions;
using WebAGK.Shared.Abstractions.Contexts;
using WebAGK.Shared.Infrastructure.Repositories;

namespace WebAGK.Module.Insurers.Core.DAL.Repositories;

internal class InsurerUnitOfWork(InsurersDbContext dbContext, IClock clock, IContext context)
    : UnitOfWork<InsurersDbContext>(dbContext, clock, context), IInsurerUnitOfWork;
