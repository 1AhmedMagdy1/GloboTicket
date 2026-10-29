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
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GloboTicket.Application.Features.UserManagement.GetUsers
{
    public class GetUsersHandler : IRequestHandler<GetUsersQuery, Result<Pagination<UserInfo>>>
    {
        private readonly IUserRepository _userRepository;
        private readonly ICurrentUserService currentUserService;
        private readonly IValidator<GetUsersQuery> GetUsersQueryvalidator;
        public GetUsersHandler(IUserRepository userRepository, ICurrentUserService currentUserService, IValidator<GetUsersQuery> getUsersQueryvalidator)
        {
            _userRepository = userRepository;
            this.currentUserService = currentUserService;
            GetUsersQueryvalidator = getUsersQueryvalidator;
        }

        public async Task<Result<Pagination<UserInfo>>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
          var validationResult=await  GetUsersQueryvalidator.ValidateAsync(request,cancellationToken);

            if (!validationResult.IsValid)
            {
                var error = validationResult.Errors.First().ToErrorResult();
                return Result<Pagination<UserInfo>>.Failure(null, ResultType.Validation, error.ErrorMessage,error);
            }

            var currentUserId = currentUserService.getCurrentUserId();

            if (currentUserId is null)
            {
                var error = new ErrorResult("user.Unauthorized", "User is not authorized");
                   
               
                var result=Result<Pagination<UserInfo>>.Failure(null,ResultType.Authorization, "User is not authorized", error);
                return result;
            
            }
            var HasRole = await _userRepository.HasRole(currentUserId, SystemRolesEnum.SystemAdmin.ToString(), cancellationToken);
                if(!HasRole.IsSuccess)
               return Result<Pagination<UserInfo>>.Forbidden();











            var Users=await _userRepository.GetUsersAsync(request, cancellationToken);

            return Result<Pagination<UserInfo>>.Success(Users.Data!, ResultType.OK, "Users retrieved successfully");
        }
    }
}
