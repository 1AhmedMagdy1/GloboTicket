using FluentValidation;
using GloboTicket.Application.Common;
using GloboTicket.Application.Contracts.Identity;
using GloboTicket.Application.Contracts.Persistence;
using GloboTicket.Application.Contracts.Services;
using GloboTicket.Application.ExtensionMethods;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace GloboTicket.Application.Features.UserManagement.SuspendUser
{
    internal class SuspendUserHandler : IRequestHandler<SuspenionUserCommand, Result<bool>>
    {
        private readonly IUserRepository userRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly ILogger<SuspendUserHandler> logger;
        private readonly ICurrentUserService currentUserService;
        private readonly IValidator<SuspenionUserCommand> validator;
        public SuspendUserHandler(IUserRepository userRepository, IUnitOfWork unitOfWork, ILogger<SuspendUserHandler> logger, ICurrentUserService currentUserService, IValidator<SuspenionUserCommand> validator)
        {
            this.userRepository = userRepository;
            this.unitOfWork = unitOfWork;
            this.logger = logger;
            this.currentUserService = currentUserService;
            this.validator = validator;
        }

        public async Task<Result<bool>> Handle(SuspenionUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                //validate Request
              var ValidationResult=await  validator.ValidateAsync(request);
                if (!ValidationResult.IsValid)
                {
                    var error= ValidationResult.Errors.First().ToErrorResult();
                    return Result<bool>.Failure(false, ResultType.Validation, error.ErrorMessage, error);

                }
                //validate user exists
                if (!await userRepository.IsuserExistsAsync(request.UserID, cancellationToken))
                    return Result<bool>.NotFound("User is Not Found", new ErrorResult("User.NotFound", "User is Not Found"));

                //var IsSystemAdminUser = await userRepository.HasRoleAsync(SystemRolesEnum.SystemAdmin.ToString(), cancellationToken);
                //if (!IsSystemAdminUser.IsSuccess)
                //    return IsSystemAdminUser;

                var result = await userRepository.SuspendUserAsync(request.UserID, request.Reason,currentUserService.getCurrentUserId()!, cancellationToken);
                if (result.IsSuccess)
                {
                    await unitOfWork.SaveChangesAsync();
                }

                return result;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return Result<bool>.Failure(false, ResultType.InternalServerError, "Un Expected Exception,Please Try Again");
            }
        }
    }
}
