using Base.DataAccess.Repositories.Base.Abstract;
using LigoraX.Domain.Entities;

namespace LigoraX.Repositories.Abstractions.Modules
{
	public interface IDerbyRepository : IAsyncMutationRepositoryBase<t_derby>, IMutationRepositoryBase<t_derby>
	{
	}
}
