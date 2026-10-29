using FluentValidation;
using GloboTicket.Application.Common;
using GloboTicket.Application.Contracts.Identity;
using GloboTicket.Application.Contracts.Persistence;
using GloboTicket.Application.Contracts.Services;
using GloboTicket.Application.DTOS.UserManagement;
using GloboTicket.Application.ExtensionMethods;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GloboTicket.Application.Features.UserManagement.GetUserByID
{
    public class GetUserByIDHandler : IRequestHandler<GetUserByIDQuery, Result<UserInfo>>
    {
        private readonly IUserRepository userRepository;
        private readonly ICurrentUserService currentUserService;
        private readonly IValidator<GetUserByIDQuery> validator;

        public GetUserByIDHandler(IUserRepository userRepository, IValidator<GetUserByIDQuery> validator, ICurrentUserService currentUserService)
        {
            this.userRepository = userRepository;
            this.validator = validator;
            this.currentUserService = currentUserService;
        }

        public async Task<Result<UserInfo>> Handle(GetUserByIDQuery request, CancellationToken cancellationToken)
        {
            var InputValidationResult = await validator.ValidateAsync(request);
            if (!InputValidationResult.IsValid)
            {

                var error = InputValidationResult.Errors.First().ToErrorResult();
             return   Result<UserInfo>.Failure(null, ResultType.Validation,error.ErrorMessage ,error);
            }
            var ActorID = currentUserService.getCurrentUserId();
            if(ActorID is null)
            {
                var error = new ErrorResult("user.Unauthorized", "User is not authorized");
                return Result<UserInfo>.UnAuthorized("User is not authorized",error);
            }
            var HasRole = await userRepository.HasRole(ActorID, SystemRolesEnum.SystemAdmin.ToString(), cancellationToken);
            if (ActorID != request.ID &&!HasRole.IsSuccess){
                var error = new ErrorResult("user.Forbidden", "You Don't have Permission to access this Resource");
                return Result<UserInfo>.Forbidden(null,error);
            }
               
            

            var user =await userRepository.GetUserById(request.ID,cancellationToken);

            if (!user.IsSuccess)
                return Result<UserInfo>.NotFound("User Not Found");

            return user;
                
            }
    }
}
