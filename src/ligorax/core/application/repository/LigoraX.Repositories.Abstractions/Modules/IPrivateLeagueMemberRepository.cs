using Base.DataAccess.Repositories.Base.Abstract;
using LigoraX.Domain.Entities;

namespace LigoraX.Repositories.Abstractions.Modules
{
	public interface IPrivateLeagueMemberRepository : IAsyncMutationRepositoryBase<t_private_league_member>, IMutationRepositoryBase<t_private_league_member>
	{
	}
}
