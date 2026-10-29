using FluentValidation.Results;
using GloboTicket.Application.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace GloboTicket.Application.ExtensionMethods
{
    public static class ValidationFailureExt
    {
        public static ErrorResult ToErrorResult(this ValidationFailure validation)
        {
            
            var Error= new ErrorResult(validation.ErrorCode, validation.ErrorMessage);

            
            return Error;
        }
    }
}
