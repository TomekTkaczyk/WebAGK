using MediatR;
using Microsoft.EntityFrameworkCore;
using WebAGK.Module.Insurers.Core.Entities;
using WebAGK.Module.Insurers.Core.Events;
using WebAGK.Module.Insurers.Core.Events.InsurerCreated;
using WebAGK.Module.Insurers.Core.Exceptions;
using WebAGK.Module.Insurers.Core.Repositories;
using WebAGK.Shared.Abstractions;
using WebAGK.Shared.Abstractions.Messaging;

namespace WebAGK.Module.Insurers.UseCases.Commands.CreateInsurer;

internal class CreateInsurerHandler(
    IInsurerRepository insurerRepository,
    IMessageBus messageBus,
    IClock clock,
    IInsurerUnitOfWork unitOfWork) 
    : IRequestHandler<CreateInsurerCommand, Guid> {
    public async Task<Guid> Handle(CreateInsurerCommand request, CancellationToken cancellationToken) {
        var _insurer = await insurerRepository
            .Get()
            .Where(x => x.Name == request.Name)
            .FirstOrDefaultAsync(cancellationToken);
        if (_insurer is not null) {
            throw new InvalidInsurerNameException();
        }

        _insurer = Insurer.Create(request.Name);
        insurerRepository.Add(_insurer);
        
        await unitOfWork.SaveChangesAsync(cancellationToken);

        await messageBus.PublishAsync(new InsurerCreatedEvent(_insurer.Id));

        return _insurer.Id;
    }
}
