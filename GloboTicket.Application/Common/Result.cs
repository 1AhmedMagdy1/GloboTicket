using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace GloboTicket.Application.Common
{
  
    public class Result<T>
    {
        public T? Data { get; set; }
        public bool  IsSuccess { get; set; }
        public ResultType Code { get; set; }
        public string Message { get; set; } = string.Empty;
        public ErrorResult? Error { get; set; }
    
        public static Result<T> Success(T Data,ResultType code,string? message=null)
            =>new Result<T> { Data = Data,IsSuccess=true,Code=code,Message=message??"" };
    
        public static Result<T> Failure(T? Data,ResultType code,string? message, ErrorResult? errors=null)
            =>new Result<T> { Data = Data,IsSuccess=false,Code=code,Message=message??"",Error=errors };

        public static Result<T> NotFound(string? Message=null, ErrorResult? Error=null) => new Result<T> { IsSuccess = false,Code=ResultType.NotFound,Message=Message??"NotFound",Error=Error };
        public static Result<T> Forbidden(string? Message=null, ErrorResult? Error=null) => new Result<T> { IsSuccess = false,Code=ResultType.Forbidden,Message=Message?? "You Don't have Permission to access this Resource", Error=Error };
        public static Result<T> UnAuthorized(string? Message=null, ErrorResult? Error=null) => new Result<T> { IsSuccess = false,Code=ResultType.Authorization,Message=Message?? "UnAuthorized",Error=Error };
        
        public  OperationResult<T> ToOperationResult()=>new OperationResult<T>(this);
       
    }

    public class ErrorResult
    {
        public string ErrorCode { get; set; } = string.Empty;
        public string ErrorMessage { get; set; } = string.Empty;
        public ErrorResult(string errorCode, string errorMessage)
        {
            ErrorCode = errorCode;
            ErrorMessage = errorMessage;
        }
    }
    public enum ResultType
    {
        OK=200,
        Created=201,
        Accepted=202,
        NoContent=204,
        Validation=400,
        Authorization=401,
        Forbidden=403,
        Conflict=409,
        ServiceUnavailable=503,
        InternalServerError=500,
        NotFound = 404,
    };
}
