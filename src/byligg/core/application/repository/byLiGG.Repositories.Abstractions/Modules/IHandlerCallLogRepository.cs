using Base.DataAccess.Repositories.Base.Abstract;
using byLiGG.Domain.Entities;

namespace byLiGG.Repositories.Abstractions.Modules
{
	public interface IHandlerCallLogRepository : IAsyncMutationRepositoryBase<t_handler_call_log>, IMutationRepositoryBase<t_handler_call_log>
	{
	}
}
