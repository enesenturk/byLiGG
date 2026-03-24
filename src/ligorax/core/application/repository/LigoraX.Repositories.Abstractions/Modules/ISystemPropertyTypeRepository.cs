using Base.DataAccess.Repositories.Base.Abstract;
using LigoraX.Domain.Entities;

namespace LigoraX.Repositories.Abstractions.Modules
{
	public interface ISystemPropertyTypeRepository : IAsyncMutationRepositoryBase<t_system_property_type>, IMutationRepositoryBase<t_system_property_type>
	{
	}
}
