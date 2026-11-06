using FluentValidation;
using GloboTicket.Application.Common;
using GloboTicket.Application.Contracts.Persistence;
using GloboTicket.Application.Contracts.Services;
using GloboTicket.Application.DTOS.Authentitcation.Login;
using GloboTicket.Application.DTOS.UserManagement;
using GloboTicket.Application.ExtensionMethods;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GloboTicket.Application.Features.Authentication.Login
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, Result<LogincommandResult>>
    {
        private readonly IAuthService authService;
        private readonly IUserRepository userRepository;
        private readonly IValidator<LoginCommand> validator;

        public LoginCommandHandler( IValidator<LoginCommand> validator, IUserRepository userRepository, IAuthService authService)
        {
          
            this.validator = validator;
            this.userRepository = userRepository;
            this.authService = authService;
        }

        public async Task<Result<LogincommandResult>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var ValidationResult = validator.Validate(request);
            if(ValidationResult.IsValid==false)
            {
                var error = ValidationResult.Errors.First().ToErrorResult();
                return Result<LogincommandResult>.Failure(null, ResultType.Validation, ValidationResult.Errors.First().ErrorMessage, error);
            }
            if(await authService.ValidateLoginCredentials(request.Email, request.Password, cancellationToken) == false)
            {
                return Result<LogincommandResult>.UnAuthorized("Invalid email or password.");
            }
            var user =await userRepository.GetUserByEmailAsync(request.Email, cancellationToken);
            if(user.IsSuccess == false)
            {
                return Result<LogincommandResult>.Failure(null, ResultType.NotFound, "User not found");
            }
            else if (user.Data!.IsSuspended)
            {
                return Result<LogincommandResult>.Forbidden("User is suspended");
            }
            else if (user.Data!.IsEmailConfirmed == false)
            {
                //send email confirmation again
                return Result<LogincommandResult>.UnAuthorized("Please Confirm your email first");
            }

            var Accesstoken =await authService.GenerateJwtToken(user.Data, cancellationToken);
            var RefreshTokenResult = await authService.AddRefreshTokenAsync(user.Data.ID,cancellationToken);
            var userRoles = user.Data.Roles.Select(r => r.ToString()).ToList();
            var Result = new LogincommandResult
            {
                AccessToken = Accesstoken,
                Roles = userRoles,
                Uid = user.Data.ID,
                RefreshToken=RefreshTokenResult.Item1,
                RefreshTokenExpirationDate = RefreshTokenResult.Item2

            };
            return Result<LogincommandResult>.Success(Result,ResultType.OK,"User Logged in SuccessFully");
        }
    }
}
