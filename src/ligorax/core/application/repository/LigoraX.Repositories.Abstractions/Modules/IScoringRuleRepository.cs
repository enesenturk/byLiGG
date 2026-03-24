using Base.DataAccess.Repositories.Base.Abstract;
using LigoraX.Domain.Entities;

namespace LigoraX.Repositories.Abstractions.Modules
{
	public interface IScoringRuleRepository : IAsyncMutationRepositoryBase<t_scoring_rule>, IMutationRepositoryBase<t_scoring_rule>
	{
	}
}
