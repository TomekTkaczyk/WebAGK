﻿using AGK.Application.Dto;
using AGK.Application.Features.Common;
using AGK.Application.Reponse;
using AGK.Application.Specifications.Common;
using AGK.Domain.Entities;
using AGK.Domain.Exceptions;
using AGK.Domain.Exceptions.Users;
using AGK.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AGK.Application.Features.UserManagment.Commands;
internal sealed class DeleteHandler(
	IUnitOfWork unitOfWork, 
	IUserRepository userRepository) 
	: IRequestHandler<Delete<UserSelectorDTO>,ApiResponse>
{
	private readonly IUnitOfWork _unitOfWork = unitOfWork;
	private readonly IUserRepository _userRepository = userRepository;

	public async Task<ApiResponse> Handle(Delete<UserSelectorDTO> request, CancellationToken cancellationToken) {

		var specification = new ByIdSpecification<User>(request.Id);
		var user = await _userRepository
			.Get(specification)
			.SingleAsync(cancellationToken)
			?? throw new UserNotFoundException();

		_userRepository.Delete(user);
		
		await _unitOfWork.SaveChangesAsync(cancellationToken);

		return ApiResponse.Success(System.Net.HttpStatusCode.NoContent);
	}
}
