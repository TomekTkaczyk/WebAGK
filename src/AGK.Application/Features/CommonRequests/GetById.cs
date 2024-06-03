using AGK.Application.Dto;
using AGK.Application.Reponse;
using AGK.Domain.ValueObjects;
using MediatR;

namespace AGK.Application.Features.Common;

public sealed record GetById<T>(EntityId Id) 
	: IRequest<ApiResponse<T>> where T : DTO;
