using System;
using System.Collections.Generic;
using System.Text;

namespace GloboTicket.Application.Common
{
    public class OperationResult<T>
    {
        public bool IsSuccess { get; set; }
        public int StatusCode { get; set; }
        public T? Data { get; set; }
        public string Message { get; set; }
        public string? ErrorCode { get; set; }
        public OperationResult(Result<T> result)
        {
            
            this.IsSuccess = result.IsSuccess;
            this.Data = result.Data;
            this.Message = result.Message;
            this.ErrorCode = result?.Errors?.FirstOrDefault()?.ErrorCode??"Invalid Request";
            this.StatusCode = (int)result.Code;
        }

    }
}
