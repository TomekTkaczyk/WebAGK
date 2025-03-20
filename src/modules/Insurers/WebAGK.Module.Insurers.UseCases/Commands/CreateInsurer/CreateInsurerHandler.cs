using MediatR;
using Microsoft.EntityFrameworkCore;
using WebAGK.Module.Insurers.Core.Entities;
using WebAGK.Module.Insurers.Core.Exceptions;
using WebAGK.Module.Insurers.Core.Repositories;

namespace WebAGK.Module.Insurers.UseCases.Commands.CreateInsurer;

internal class CreateInsurerHandler(
    IInsurerRepository insurerRepository,
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
        // _insurer.Structure = await agentRepository
        //     .Get()
        //     .OrderBy(x => x.Name)
        //     .Select(x => new Node() {
        //         InsurerId = _insurer.Id,
        //         Agent = x
        //     })
        //     .ToListAsync(cancellationToken);
        //
        // _insurer.RenumberingStructure();
        
        insurerRepository.Add(_insurer);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        
        return _insurer.Id;
    }
}
