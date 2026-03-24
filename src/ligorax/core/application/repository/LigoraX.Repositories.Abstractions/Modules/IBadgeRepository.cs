using Base.DataAccess.Repositories.Base.Abstract;
using LigoraX.Domain.Entities;

namespace LigoraX.Repositories.Abstractions.Modules
{
	public interface IBadgeRepository : IAsyncMutationRepositoryBase<t_badge>, IMutationRepositoryBase<t_badge>
	{
	}
}