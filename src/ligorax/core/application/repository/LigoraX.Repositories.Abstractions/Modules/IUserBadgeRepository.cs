using Base.DataAccess.Repositories.Base.Abstract;
using LigoraX.Domain.Entities;

namespace LigoraX.Repositories.Abstractions.Modules
{
	public interface IUserBadgeRepository : IAsyncMutationRepositoryBase<t_user_badge>, IMutationRepositoryBase<t_user_badge>
	{
	}
}
