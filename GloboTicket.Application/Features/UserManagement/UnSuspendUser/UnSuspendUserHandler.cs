using GloboTicket.Application.Common;
using GloboTicket.Application.Contracts.Persistence;
using GloboTicket.Application.Contracts.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GloboTicket.Application.Features.UserManagement.UnSuspendUser
{
    public class UnSuspendUserHandler : IRequestHandler<UnSuspendUserCommand, Result<bool>>
    {
        private readonly IUserRepository userRepository;
        private readonly ICurrentUserService currentUserService;
        private readonly IUnitOfWork unitOfWork;
        public UnSuspendUserHandler(IUserRepository userRepository, ICurrentUserService currentUserService, IUnitOfWork unitOfWork)
        {
            this.userRepository = userRepository;
            this.currentUserService = currentUserService;
            this.unitOfWork = unitOfWork;
        }


        public async Task<Result<bool>> Handle(UnSuspendUserCommand request, CancellationToken cancellationToken)
        {
            if(!await userRepository.IsuserExistsAsync(request.UserID,cancellationToken))
                return  Result<bool>.NotFound("User is Not Found", new ErrorResult("User.NotFound", "User is Not Found"));

            var UnSuspendResult = await userRepository.UnSuspendUserAsync(request.UserID, currentUserService.getCurrentUserId()!, cancellationToken);
            if (UnSuspendResult.IsSuccess)
               await unitOfWork.SaveChangesAsync();
            return UnSuspendResult;
        }
    }
}
