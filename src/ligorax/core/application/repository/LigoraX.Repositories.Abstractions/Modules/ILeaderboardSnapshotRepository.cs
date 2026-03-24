using Base.DataAccess.Repositories.Base.Abstract;
using LigoraX.Domain.Entities;

namespace LigoraX.Repositories.Abstractions.Modules
{
	public interface ILeaderboardSnapshotRepository : IAsyncMutationRepositoryBase<t_leaderboard_snapshot>, IMutationRepositoryBase<t_leaderboard_snapshot>
	{
	}
}
