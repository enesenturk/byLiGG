using Base.DataAccess.Repositories.Base.Abstract;
using LigoraX.Domain.Entities;

namespace LigoraX.Repositories.Abstractions.Modules
{
	public interface ICountryRepository : IAsyncMutationRepositoryBase<t_country>, IMutationRepositoryBase<t_country>
	{
	}
}
