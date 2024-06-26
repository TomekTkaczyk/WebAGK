﻿using AGK.Domain.Entities;
using AGK.Domain.ValueObjects;

namespace AGK.Application.Services;
public interface IUserManager
{
    Task<User> AddAsync(User user, CancellationToken cancellationToken = default);
	
    Task<int> UpdateAsync(User user, CancellationToken cancellationToken = default);
	
    Task<int> DeleteAsync(User user, CancellationToken cancellationToken = default);

	Task<User> FindByEmailAsync(Email email, CancellationToken cancellationToken = default);
    
    Task<User> FindByUserNameAsync(UserName userName, CancellationToken cancellationToken = default);
    
    Task<User> FindByIdAsync(EntityId id, CancellationToken cancellationToken = default);

    Task<bool> IsUniqueAsync(User user, CancellationToken cancellationToken = default);
}
