using AGK.Domain.ValueObjects;
using MediatR;

namespace AGK.Application.Features.Common;
public sealed record Delete<T>(EntityId Id) : IRequest;
