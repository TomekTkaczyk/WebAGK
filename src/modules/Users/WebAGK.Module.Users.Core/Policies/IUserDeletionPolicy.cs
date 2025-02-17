using WebAGK.Module.Users.Core.Entities;

namespace WebAGK.Module.Users.Core.Policies;

internal interface IUserDeletionPolicy
{
	Task<bool> CanDeleteAsync(User employee);
}
