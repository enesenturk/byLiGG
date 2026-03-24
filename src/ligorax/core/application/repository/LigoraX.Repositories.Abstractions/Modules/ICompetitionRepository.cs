using Base.DataAccess.Repositories.Base.Abstract;
using LigoraX.Domain.Entities;

namespace LigoraX.Repositories.Abstractions.Modules
{
	public interface ICompetitionRepository : IAsyncMutationRepositoryBase<t_competition>, IMutationRepositoryBase<t_competition>
	{
	}
}
