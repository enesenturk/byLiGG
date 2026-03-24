using Base.DataAccess.Repositories.Base.Concrete;
using LigoraX.Domain.Entities;
using LigoraX.Persistence.Contexts;
using LigoraX.Repositories.Abstractions.Modules;

namespace LigoraX.Repositories.Modules
{
	public class EfCountryRepository : EfMutationRepositoryBase<t_country, ligoraxContext>, ICountryRepository
	{
	}
}
