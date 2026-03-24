using Base.DataAccess.Repositories.Base.Abstract;
using LigoraX.Domain.Entities;

namespace LigoraX.Repositories.Abstractions.Modules
{
	public interface IUserRepository : IAsyncMutationRepositoryBase<t_user>, IMutationRepositoryBase<t_user>
	{
	}
}
