using Base.DataAccess.Repositories.Base.Abstract;
using LigoraX.Domain.Entities;

namespace LigoraX.Repositories.Abstractions.Modules
{
	public interface IMatchRepository : IAsyncMutationRepositoryBase<t_match>, IMutationRepositoryBase<t_match>
	{
	}
}