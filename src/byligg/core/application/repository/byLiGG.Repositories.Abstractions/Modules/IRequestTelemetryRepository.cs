using Base.DataAccess.Repositories.Base.Abstract;
using byLiGG.Domain.Entities;

namespace byLiGG.Repositories.Abstractions.Modules
{
	public interface IRequestTelemetryRepository : IAsyncMutationRepositoryBase<t_request_telemetry>, IMutationRepositoryBase<t_request_telemetry>
	{
	}
}
