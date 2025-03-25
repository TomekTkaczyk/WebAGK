using MediatR;
using Microsoft.EntityFrameworkCore;
using WebAGK.Module.Insurers.Core.Entities;
using WebAGK.Module.Insurers.Core.Events;
using WebAGK.Module.Insurers.Core.Repositories;
using WebAGK.Module.Insurers.UseCases.Queries.GetInsurer;
using WebAGK.Shared.Abstractions;
using WebAGK.Shared.Abstractions.Messaging;
using WebAGK.Shared.Infrastructure.Repositories;

namespace WebAGK.Module.Insurers.UseCases.Commands.UpdateInsurer;

internal sealed class UpdateInsurerHandler(
    IInsurerRepository repository,
    IMessageBus messageBus,
    IClock clock,
    IInsurerUnitOfWork unitOfWork) 
    : IRequestHandler<UpdateInsurerCommand> {
    public async Task Handle(UpdateInsurerCommand request, CancellationToken cancellationToken) {
        var _insurer = await repository
            .Get(new ByIdSpecification<Insurer>(request.Id))
            .SingleOrDefaultAsync(cancellationToken)
            ?? throw new InsurerNotFoundException(request.Id);
        
        _insurer.Name = request.Name;
        _insurer.Description = request.Description;
        
        repository.Update(_insurer);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        
        await messageBus.PublishAsync(new InsurerUpdatedEvent(_insurer.Id));
    }
}