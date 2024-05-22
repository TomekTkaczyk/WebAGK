using AGK.Application.Dto;
using AGK.Domain.Entities;

namespace AGK.Application.Mappers;

internal static class UserMapper
{
	internal static UserDTO ToDTO(this User user) {

		var userDTO = new UserDTO(
			user.Id,
			user.Username,
			user.Firstname,
			user.Lastname);
		
		return userDTO;
	}
}
