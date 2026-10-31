using GloboTicket.Application.Common;
using GloboTicket.Application.Contracts.Persistence;
using GloboTicket.Application.Contracts.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GloboTicket.Application.Features.UserManagement.UpdateUser
{
    public class UpdateUserHandler : IRequestHandler<UpdateUserCommand, Result<bool>>
    {
        private readonly IUserRepository userRepository;

        public UpdateUserHandler(IUserRepository userRepository, ICurrentUserService currentUserService, IUnitOfWork unitOfWork)
        {
            this.userRepository = userRepository;
            this.currentUserService = currentUserService;
            this.unitOfWork = unitOfWork;
        }

        private readonly ICurrentUserService currentUserService;
        private readonly IUnitOfWork unitOfWork;
        public async Task<Result<bool>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            if(!await userRepository.IsuserExistsAsync(request.UserID,cancellationToken))
                return Result<bool>.NotFound("User is Not Found", new ErrorResult("User.NotFound", "User is Not Found"));

            if (request.UserID != currentUserService.getCurrentUserId())
                return Result<bool>.Forbidden();
            var UpdateResult = await userRepository.UpdateUserAsync(request,cancellationToken);
            if (UpdateResult.IsSuccess)
                await unitOfWork.SaveChangesAsync();
            return UpdateResult;
        }
    }
}
