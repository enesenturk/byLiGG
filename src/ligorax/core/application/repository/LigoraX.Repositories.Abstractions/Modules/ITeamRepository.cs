using Base.DataAccess.Repositories.Base.Abstract;
using LigoraX.Domain.Entities;

namespace LigoraX.Repositories.Abstractions.Modules
{
	public interface ITeamRepository : IAsyncMutationRepositoryBase<t_team>, IMutationRepositoryBase<t_team>
	{
	}
}
