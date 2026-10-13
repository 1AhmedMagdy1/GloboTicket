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
        public IEnumerable<ErrorResult>? Errors { get; set; }
    
        public static Result<T> Success(T Data,ResultType code,string? message)
            =>new Result<T> { Data = Data,IsSuccess=true,Code=code,Message=message??"" };
    
        public static Result<T> Failure(T Data,ResultType code,string? message,IEnumerable<ErrorResult> errors)
            =>new Result<T> { Data = Data,IsSuccess=false,Code=code,Message=message??"",Errors=errors };
    
        public static OperationResult<T> ToOperationResult(Result<T> Result)=>new OperationResult<T>(Result);
        
    }

    public class ErrorResult
    {
        public string ErrorCode { get; set; } = string.Empty;
        public string ErrorMessage { get; set; } = string.Empty;
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
        InternalServerError=500
    };
}
