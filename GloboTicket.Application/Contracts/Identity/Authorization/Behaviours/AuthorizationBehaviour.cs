using GloboTicket.Application.Common;
using GloboTicket.Application.Contracts.Persistence;
using GloboTicket.Application.Contracts.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GloboTicket.Application.Contracts.Identity.Authorization.Behaviours
{
    public class AuthorizationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest: notnull
        where TResponse:IResultResponse,new()
    {
        private readonly ICurrentUserService currentUserService;
        private readonly IUserRepository userRepository;
        public AuthorizationBehaviour(ICurrentUserService currentUserService, IUserRepository userRepository)
        {
            this.currentUserService = currentUserService;
            this.userRepository = userRepository;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
           if(request is not IRequireAuthorization secured)
            {
                return await next(cancellationToken);
            }

            var CurrentActor = currentUserService.getCurrentUserId();
            if (string.IsNullOrWhiteSpace(CurrentActor))
            {
                return Deny(ResultType.Authorization, "User is not Authorized", new ErrorResult("User.Authorization", "User is not Authorized"));
            }
            var HasAccess=await userRepository.HasAnyRole(CurrentActor, secured.RequiredRoles, cancellationToken);
            if (!HasAccess.IsSuccess)
                return Deny(ResultType.Forbidden, "You don't have Access To this Resource", new ErrorResult("User.Forbidden", "You don't have Access To this Resource"));

            return await next(cancellationToken);
        }

        public static TResponse Deny(ResultType resultType,string message,ErrorResult error)
        {
            return new TResponse
            {
                Code = resultType,
                Error = error,
                IsSuccess = false,
                Message=message

            };
        }
    }
}
