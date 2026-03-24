using Base.DataAccess.Repositories.Base.Concrete;
using LigoraX.Domain.Entities;
using LigoraX.Persistence.Contexts;
using LigoraX.Repositories.Abstractions.Modules;

namespace LigoraX.Repositories.Modules
{
	public class EfTeamRepository : EfMutationRepositoryBase<t_team, ligoraxContext>, ITeamRepository
	{
	}
}
