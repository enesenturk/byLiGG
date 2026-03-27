using byLiGG.Application.Constants;
using byLiGG.Authorization.Models.Request;
using byLiGG.Authorization.Models.Request.Authorization;
using byLiGG.Domain.Entities;
using byLiGG.Repositories.Abstractions.Modules;
using MediatR;
using System.Diagnostics;

namespace byLiGG.Application.UseCases.Pipelines
{
	public class RequestMetricsBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
		where TRequest : IBaseRequest<TResponse>
	{

		#region CTOR

		private readonly IRequestTelemetryRepository _requestTelemetryRepository;

		public RequestMetricsBehavior(IRequestTelemetryRepository requestTelemetryRepository)
		{
			_requestTelemetryRepository = requestTelemetryRepository;
		}

		#endregion

		public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
		{
			Stopwatch stopwatch = Stopwatch.StartNew();
			bool isSuccess = true;

			try
			{
				return await next();
			}
			catch (Exception)
			{
				isSuccess = false;

				throw;
			}
			finally
			{
				stopwatch.Stop();

				Guid userId = ExtractUserId(request);

				await _requestTelemetryRepository.AddAsync(new t_request_telemetry
				{
					t_user_id = userId,
					handler_name = typeof(TRequest).Name,
					duration_ms = (int)stopwatch.ElapsedMilliseconds,
					is_success = isSuccess,
				}, userId);
			}
		}

		#region Behind the Scenes

		private static Guid ExtractUserId(TRequest request)
		{
			if (request is IAuthorizationRequest<TResponse> auth)
				return auth.Authorization?.ExecutingUserId ?? SystemConstants.SystemUserId;

			if (request is IAuthorizationFreeRequest<TResponse> authFree)
				return authFree.AuthorizationFree?.ExecutingUserId ?? SystemConstants.SystemUserId;

			return SystemConstants.SystemUserId;
		}

		#endregion
	}
}
