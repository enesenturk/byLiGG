using Base.DataAccess.Repositories.Base.Abstract;
using LigoraX.Domain.Entities;

namespace LigoraX.Repositories.Abstractions.Modules
{
	public interface ISystemPropertyRepository : IAsyncMutationRepositoryBase<t_system_property>, IMutationRepositoryBase<t_system_property>
	{
	}
}
