using Base.DataAccess.Repositories.Base.Abstract;
using LigoraX.Domain.Entities;

namespace LigoraX.Repositories.Abstractions.Modules
{
	public interface IPredictionRepository : IAsyncMutationRepositoryBase<t_prediction>, IMutationRepositoryBase<t_prediction>
	{
	}
}