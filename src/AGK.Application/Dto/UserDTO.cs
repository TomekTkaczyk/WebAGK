using AGK.Domain.ValueObjects;

namespace AGK.Application.Dto;
public record UserDTO
(
	EntityId Id,
	string UserName,
	string FirstName,
	string LastName
);
