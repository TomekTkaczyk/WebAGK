using AGK.Application.Dto;
using AGK.Application.Reponse;
using AGK.Domain.ValueObjects;
using MediatR;

namespace AGK.Application.Features.Common;

public record SetActive<T>(EntityId Id,bool ActiveStatus)
	: IRequest<ApiResponse> where T : SelectorDTO;
