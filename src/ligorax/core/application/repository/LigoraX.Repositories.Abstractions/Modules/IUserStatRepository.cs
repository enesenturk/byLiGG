using Base.DataAccess.Repositories.Base.Abstract;
using LigoraX.Domain.Entities;

namespace LigoraX.Repositories.Abstractions.Modules
{
	public interface IUserStatRepository : IAsyncMutationRepositoryBase<t_user_stat>, IMutationRepositoryBase<t_user_stat>
	{
	}
}