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

		private readonly IHandlerCallLogRepository _handlerCallLogRepository;
		private readonly IHandlerCallLogByUserRepository _handlerCallLogByUserRepository;

		public RequestMetricsBehavior(IHandlerCallLogRepository handlerCallLogRepository, IHandlerCallLogByUserRepository handlerCallLogByUserRepository)
		{
			_handlerCallLogRepository = handlerCallLogRepository;
			_handlerCallLogByUserRepository = handlerCallLogByUserRepository;
		}

		#endregion

		public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
		{
			Stopwatch stopwatch = Stopwatch.StartNew();
			bool isSuccess = true;
			string errorType = null;

			try
			{
				return await next();
			}
			catch (Exception ex)
			{
				isSuccess = false;
				errorType = ex.GetType().Name;

				throw;
			}
			finally
			{
				stopwatch.Stop();

				string handlerName = typeof(TRequest).Name;
				int durationMs = (int)stopwatch.ElapsedMilliseconds;
				Guid? userId = ExtractUserId(request);

				await _handlerCallLogRepository.AddAsync(new t_handler_call_log
				{
					handler_name = handlerName,
					duration_ms = durationMs,
					is_success = isSuccess,
					error_type = errorType,
				}, SystemConstants.SystemUserId);

				if (userId.HasValue)
				{
					await _handlerCallLogByUserRepository.AddAsync(new t_handler_call_log_by_user
					{
						t_user_id = userId.Value,
						handler_name = handlerName,
						duration_ms = durationMs,
						is_success = isSuccess,
					}, userId.Value);
				}
			}
		}

		#region Behind the Scenes

		private static Guid? ExtractUserId(TRequest request)
		{
			if (request is IAuthorizationRequest<TResponse> auth)
				return auth.Authorization?.ExecutingUserId;

			if (request is IAuthorizationFreeRequest<TResponse> authFree)
				return authFree.AuthorizationFree?.ExecutingUserId;

			return null;
		}

		#endregion
	}
}
