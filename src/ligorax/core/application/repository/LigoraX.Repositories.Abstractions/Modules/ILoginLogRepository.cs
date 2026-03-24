using Base.DataAccess.Repositories.Base.Abstract;
using LigoraX.Domain.Entities;

namespace LigoraX.Repositories.Abstractions.Modules
{
	public interface ILoginLogRepository : IAsyncMutationRepositoryBase<t_login_log>, IMutationRepositoryBase<t_login_log>
	{
	}
}
