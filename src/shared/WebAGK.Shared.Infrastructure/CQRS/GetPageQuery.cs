using MediatR;

namespace WebAGK.Shared.Infrastructure.CQRS;

public record GetPageQuery<T>(string SearchText, bool ActiveStatus, int PageNumber, int PageSize) 
    : IRequest<Page<T>> where T : class { }