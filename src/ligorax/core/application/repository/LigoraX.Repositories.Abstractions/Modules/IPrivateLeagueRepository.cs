using Base.DataAccess.Repositories.Base.Abstract;
using LigoraX.Domain.Entities;

namespace LigoraX.Repositories.Abstractions.Modules
{
	public interface IPrivateLeagueRepository : IAsyncMutationRepositoryBase<t_private_league>, IMutationRepositoryBase<t_private_league>
	{
	}
}
