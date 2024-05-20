using MediatR;

namespace AGK.Application.Features.Common;

public sealed record SetActive<T>( EntityId Id, bool ActiveStatus ) : IRequest;
