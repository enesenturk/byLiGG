using Base.Exceptions.ExceptionModels;
using Base.PrimitiveTypeHelpers._DateTime.Entensions;
using byLiGG.Authorization.Models.Context.Authentication;
using byLiGG.Authorization.Models.Context.Authorization;
using byLiGG.Authorization.Models.Request;
using byLiGG.Authorization.Models.Request.Authentication;
using byLiGG.Authorization.Models.Request.Authorization;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace byLiGG.Presentation.Pipelines
{
	public class SetAuthContextBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
		where TRequest : IBaseRequest<TResponse>
	{

		#region CTOR

		private readonly IHttpContextAccessor _httpContextAccessor;

		public SetAuthContextBehaviour(IHttpContextAccessor httpContextAccessor)
		{
			_httpContextAccessor = httpContextAccessor;
		}

		#endregion

		public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
		{
			if (request is IAuthorizationRequest<TResponse> authorizationRequest)
			{
				Guid userId = GetUserIdFromClaims();

				authorizationRequest.Authorization = new AuthorizationContext
				{
					ExecutingUserId = userId,
					IpAddress = GetIpAddress(),
					Ticket = new AuthorizationTicket
					{
						UserId = userId,
						Timeout = GetTokenExpiry(),
					}
				};
			}
			else if (request is IAuthorizationFreeRequest<TResponse> authorizationFreeRequest)
			{
				Guid userId = GetUserIdFromClaims();

				authorizationFreeRequest.AuthorizationFree = new AuthorizationFreeContext(userId, GetIpAddress());
			}
			else if (request is IAuthenticationFreeRequest<TResponse> authenticationFreeRequest)
			{
				authenticationFreeRequest.AuthenticationFree = new AuthenticationFreeContext
				{
					IpAddress = GetIpAddress()
				};
			}

			return await next();
		}

		#region Behind the Scenes

		private Guid GetUserIdFromClaims()
		{
			string userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);

			if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out Guid userId))
				throw new AuthorizationException();

			return userId;
		}

		private DateTime GetTokenExpiry()
		{
			string expClaim = _httpContextAccessor.HttpContext?.User?.FindFirstValue("exp");

			if (!string.IsNullOrEmpty(expClaim) && long.TryParse(expClaim, out long expUnix))
				return DateTimeOffset.FromUnixTimeSeconds(expUnix).UtcDateTime;

			return DateTime.Now.ToUniversalTimeZone();
		}

		private string GetIpAddress()
		{
			HttpContext httpContext = _httpContextAccessor.HttpContext;

			if (httpContext is null)
				return string.Empty;

			string forwarded = httpContext.Request.Headers["X-Forwarded-For"].FirstOrDefault();

			if (!string.IsNullOrEmpty(forwarded))
				return forwarded.Split(',')[0].Trim();

			return httpContext.Connection.RemoteIpAddress?.ToString() ?? string.Empty;
		}

		#endregion

	}
}
