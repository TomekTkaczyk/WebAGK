using MediatR;
using Microsoft.EntityFrameworkCore;
using WebAGK.Module.Insurers.Core.Entities;
using WebAGK.Module.Insurers.Core.Repositories;
using WebAGK.Module.Insurers.UseCases.Queries.GetInsurer;
using WebAGK.Shared.Infrastructure.Repositories;

namespace WebAGK.Module.Insurers.UseCases.Commands.UpdateInsurer;

internal sealed class UpdateInsurerHandler(
    IInsurerRepository repository,
    IInsurerUnitOfWork unitOfWork) : IRequestHandler<UpdateInsurerCommand> {
    public async Task Handle(UpdateInsurerCommand request, CancellationToken cancellationToken) {
        var _insurer = await repository
            .Get(new ByIdSpecification<Insurer>(request.Id))
            .SingleOrDefaultAsync(cancellationToken)
            ?? throw new InsurerNotFoundException(request.Id);
        
        _insurer.Name = request.Name;
        _insurer.Description = request.Description;
        
        repository.Update(_insurer);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}