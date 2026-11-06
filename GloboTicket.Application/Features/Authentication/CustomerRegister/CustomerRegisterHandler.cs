using FluentValidation;
using GloboTicket.Application.Common;
using GloboTicket.Application.Contracts.Persistence;
using GloboTicket.Application.Contracts.Services;
using GloboTicket.Application.ExtensionMethods;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GloboTicket.Application.Features.Authentication.CustomerRegister
{
    public class CustomerRegisterHandler : IRequestHandler<CustomerRegisterCommand, Result<bool>>
    {
        private readonly IAuthService _authService;
        private readonly IValidator<CustomerRegisterCommand> _validator;
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        public CustomerRegisterHandler(IAuthService authService, IValidator<CustomerRegisterCommand> validator, IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _authService = authService;
            _validator = validator;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<bool>> Handle(CustomerRegisterCommand request, CancellationToken cancellationToken)
        {
            var validationResult = _validator.Validate(request);
            if (!validationResult.IsValid)
            {
                var error = validationResult.Errors.First().ToErrorResult();
                return Result<bool>.Failure(false, ResultType.Validation, error.ErrorMessage, error);
            }

            if (await _userRepository.ExistsByEmailAsync(request.Email, cancellationToken))
            {
                return Result<bool>.Failure(false, ResultType.Conflict, "User with this email already exists");
            }
           


               
                var result = await _authService.RegisterCustomerAsync(request, cancellationToken);
             
                

                return result;
            }
           
        }
    }

