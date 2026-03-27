using Base.DataAccess.Repositories.Base.Concrete;
using byLiGG.Domain.Entities;
using byLiGG.Persistence.Contexts;
using byLiGG.Repositories.Abstractions.Modules;

namespace byLiGG.Repositories.Modules
{
	public class EfHandlerCallLogByUserRepository : EfMutationRepositoryBase<t_handler_call_log_by_user, byliggContext>, IHandlerCallLogByUserRepository
	{
	}
}
