using MediatR;
using Microsoft.EntityFrameworkCore;
using WebAGK.Module.Insurers.Core.Entities;
using WebAGK.Module.Insurers.Core.Events;
using WebAGK.Module.Insurers.Core.Repositories;
using WebAGK.Module.Insurers.UseCases.Queries.GetInsurer;
using WebAGK.Shared.Abstractions;
using WebAGK.Shared.Abstractions.Messaging;
using WebAGK.Shared.Infrastructure.Repositories;

namespace WebAGK.Module.Insurers.UseCases.Commands.DeleteInsurer;

internal class DeleteInsurerHandler(
    IInsurerRepository repository,
    IMessageBus messageBus,
    IClock clock,
    IInsurerUnitOfWork unitOfWork) 
    : IRequestHandler<DeleteInsurerCommand> {
    public async Task Handle(DeleteInsurerCommand request, CancellationToken cancellationToken) {
        var _insurer = await repository
            .Get(new ByIdSpecification<Insurer>(request.Id))
            .SingleOrDefaultAsync(cancellationToken)
            ?? throw new InsurerNotFoundException(request.Id);
        
        repository.Delete(_insurer);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        
        await messageBus.PublishAsync(new InsurerDeletedEvent(_insurer.Id));
    }
}