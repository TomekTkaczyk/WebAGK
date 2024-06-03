namespace AGK.Application.Dto;
public record UserDTO
(
	TypeEntityId Id,
	string UserName,
	string FirstName,
	string LastName
) : DTO;
