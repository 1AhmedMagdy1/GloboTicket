using FluentValidation;
using GloboTicket.Application.Common;
using GloboTicket.Application.Contracts.Identity;
using GloboTicket.Application.Contracts.Persistence;
using GloboTicket.Application.Contracts.Services;
using GloboTicket.Application.DTOS.UserManagement;

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
        public GetUsersHandler(IUserRepository userRepository, ICurrentUserService currentUserService)
        {
            _userRepository = userRepository;
            this.currentUserService = currentUserService;
        }

        public async Task<Result<Pagination<UserInfo>>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
          var validationResult=await  GetUsersQueryvalidator.ValidateAsync(request,cancellationToken);
            if (!validationResult.IsValid)
            {
               var errors= validationResult.Errors.Select(e => new ErrorResult
                {
                    ErrorCode = e.ErrorCode,
                    ErrorMessage = e.ErrorMessage
                }).First();
                return Result<Pagination<UserInfo>>.Failure(null, ResultType.Validation, "Validation failed", new List<ErrorResult> { errors });
            }
            var currentUserId = currentUserService.getCurrentUserId();
            if (currentUserId is null)
            {
                var errors = new ErrorResult[]
                {
                    new ErrorResult
                    {
                     ErrorCode = "user.Unauthorized",
                    ErrorMessage = "User is not authorized"
                    }  
                };
                var result=Result<Pagination<UserInfo>>.Failure(null,ResultType.Authorization, "User is not authorized", errors);
                return result;
            
            }
            if(!await _userRepository.HasRole(currentUserId,SystemRolesEnum.SystemAdmin.ToString(),cancellationToken))
            {
                var errors = new ErrorResult[]
                {
                    new ErrorResult
                    {
                     ErrorCode = "user.Forbidden",
                    ErrorMessage = "User is not authorized"
                    }  
                };
                var result=Result<Pagination<UserInfo>>.Failure(null,ResultType.Forbidden, "User is not authorized", errors);
                return result;
            }


            var Users=await _userRepository.GetUsersAsync(request, currentUserId, cancellationToken);

            return Result<Pagination<UserInfo>>.Success(Users, ResultType.OK, "Users retrieved successfully");
        }
    }
}
