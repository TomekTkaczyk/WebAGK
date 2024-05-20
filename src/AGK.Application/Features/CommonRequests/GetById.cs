using AGK.Application.Reponse;
using MediatR;

namespace AGK.Application.Features.Common;

public sealed record GetById<T>(EntityId Id) : IRequest<ApiResponse<T>>;
