using Base.Validation.Pipelines;
using byLiGG.Application.UseCases.Modules.User.Commands.CreateUserRegistrationCommand.BusinessRules;
using byLiGG.Application.UseCases.Pipelines;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace byLiGG.Application.UseCases
{
	public static class ServiceRegistration
	{
		public static IServiceCollection AddUseCaseServices(this IServiceCollection services)
		{
			var assembly = Assembly.GetExecutingAssembly();

			services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(assembly));
			services.AddAutoMapper(cfg =>
			{
				cfg.ReplaceMemberName("_", "");

				cfg.AddMaps(assembly);
			});
			services.AddValidatorsFromAssembly(assembly);

			services.AddPipelines();

			services.AddBusinessRules();
			services.AddUseCaseInternals();

			return services;
		}

		#region Behind the Scenes

		public static IServiceCollection AddPipelines(this IServiceCollection services)
		{
			services.AddScoped(typeof(IPipelineBehavior<,>), typeof(RequestMetricsBehavior<,>));
			services.AddScoped(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));

			return services;
		}

		private static IServiceCollection AddBusinessRules(this IServiceCollection services)
		{
			services.AddScoped<Create_UserRegistration_Command_BusinessRules>();

			return services;
		}

		private static IServiceCollection AddUseCaseInternals(this IServiceCollection services)
		{

			return services;
		}

		#endregion

	}
}