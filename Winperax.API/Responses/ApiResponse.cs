using System.Net;

namespace Winperax.API.Responses
{
    public class ApiResponse<T>
    {
        public bool IsSuccess { get; set; }
        public T? Data { get; set; }
        public string[]? Errors { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; } = string.Empty;

        public static ApiResponse<T> Success(
            T data,
            string message = "Operation successful",
            int statusCode = (int)HttpStatusCode.OK
        )
        {
            return new ApiResponse<T>
            {
                IsSuccess = true,
                Data = data,
                StatusCode = statusCode,
                Message = message,
            };
        }

        public static ApiResponse<T> Failure(
            string[] errors,
            int statusCode = (int)HttpStatusCode.BadRequest,
            string message = "Operation failed"
        )
        {
            return new ApiResponse<T>
            {
                IsSuccess = false,
                Errors = errors,
                StatusCode = statusCode,
                Message = message,
            };
        }

        public static ApiResponse<T> Failure(
            string error,
            int statusCode = (int)HttpStatusCode.BadRequest,
            string message = "Operation failed"
        )
        {
            return new ApiResponse<T>
            {
                IsSuccess = false,
                Errors = new[] { error },
                StatusCode = statusCode,
                Message = message,
            };
        }
    }

    public class ApiResponse
    {
        public bool IsSuccess { get; set; }
        public string[]? Errors { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; } = string.Empty;

        public static ApiResponse Success(
            string message = "Operation successful",
            int statusCode = (int)HttpStatusCode.OK
        )
        {
            return new ApiResponse
            {
                IsSuccess = true,
                StatusCode = statusCode,
                Message = message,
            };
        }

        public static ApiResponse Failure(
            string[] errors,
            int statusCode = (int)HttpStatusCode.BadRequest,
            string message = "Operation failed"
        )
        {
            return new ApiResponse
            {
                IsSuccess = false,
                Errors = errors,
                StatusCode = statusCode,
                Message = message,
            };
        }

        public static ApiResponse Failure(
            string error,
            int statusCode = (int)HttpStatusCode.BadRequest,
            string message = "Operation failed"
        )
        {
            return new ApiResponse
            {
                IsSuccess = false,
                Errors = new[] { error },
                StatusCode = statusCode,
                Message = message,
            };
        }
    }
}
