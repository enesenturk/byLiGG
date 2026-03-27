using Base.DataAccess.Repositories.Base.Abstract;
using byLiGG.Domain.Entities;

namespace byLiGG.Repositories.Abstractions.Modules
{
	public interface IHandlerCallLogByUserRepository : IAsyncMutationRepositoryBase<t_handler_call_log_by_user>, IMutationRepositoryBase<t_handler_call_log_by_user>
	{
	}
}
