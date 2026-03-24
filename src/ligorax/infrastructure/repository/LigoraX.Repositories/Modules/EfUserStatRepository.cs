using Base.DataAccess.Repositories.Base.Concrete;
using LigoraX.Domain.Entities;
using LigoraX.Persistence.Contexts;
using LigoraX.Repositories.Abstractions.Modules;

namespace LigoraX.Repositories.Modules
{
	public class EfUserStatRepository : EfMutationRepositoryBase<t_user_stat, ligoraxContext>, IUserStatRepository
	{
	}
}