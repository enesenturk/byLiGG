using byLiGG.Repositories.Abstractions.Modules;
using byLiGG.Repositories.Modules;
using Microsoft.Extensions.DependencyInjection;

namespace byLiGG.Repositories
{
	public static class ServiceRegistration
	{
		public static void AddRepositoryServices(this IServiceCollection services)
		{
			services.AddScoped<IBadgeRepository, EfBadgeRepository>();
			services.AddScoped<ICompetitionRepository, EfCompetitionRepository>();
			services.AddScoped<ICountryRepository, EfCountryRepository>();
			services.AddScoped<IDerbyRepository, EfDerbyRepository>();
			services.AddScoped<ILeaderboardSnapshotRepository, EfLeaderboardSnapshotRepository>();
			services.AddScoped<ILoginLogRepository, EfLoginLogRepository>();
			services.AddScoped<IMatchRepository, EfMatchRepository>();
			services.AddScoped<IPredictionRepository, EfPredictionRepository>();
			services.AddScoped<IPrivateLeagueRepository, EfPrivateLeagueRepository>();
			services.AddScoped<IPrivateLeagueMemberRepository, EfPrivateLeagueMemberRepository>();
			services.AddScoped<IRequestTelemetryRepository, EfRequestTelemetryRepository>();
			services.AddScoped<IScoringRuleRepository, EfScoringRuleRepository>();
			services.AddScoped<ISystemPropertyRepository, EfSystemPropertyRepository>();
			services.AddScoped<ISystemPropertyTypeRepository, EfSystemPropertyTypeRepository>();
			services.AddScoped<ITeamRepository, EfTeamRepository>();
			services.AddScoped<IUserBadgeRepository, EfUserBadgeRepository>();
			services.AddScoped<IUserRepository, EfUserRepository>();
			services.AddScoped<IUserStatRepository, EfUserStatRepository>();
		}
	}
}