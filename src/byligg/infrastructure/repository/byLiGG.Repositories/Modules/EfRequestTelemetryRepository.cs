using Base.DataAccess.Repositories.Base.Concrete;
using byLiGG.Domain.Entities;
using byLiGG.Persistence.Contexts;
using byLiGG.Repositories.Abstractions.Modules;

namespace byLiGG.Repositories.Modules
{
	public class EfRequestTelemetryRepository : EfMutationRepositoryBase<t_request_telemetry, byliggContext>, IRequestTelemetryRepository
	{
	}
}