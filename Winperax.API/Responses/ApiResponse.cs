namespace Winperax.API.Responses
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public T? Data { get; set; }
        public string[] Errors { get; set; } = Array.Empty<string>();
        public string Message { get; set; } = string.Empty;

        public static ApiResponse<T> SuccessResult(T data, string message = "Operation successful")
        {
            return new ApiResponse<T>
            {
                Success = true,
                Data = data,
                Message = message
            };
        }

        public static ApiResponse<T> FailureResult(string[] errors, string message = "Operation failed")
        {
            return new ApiResponse<T>
            {
                Success = false,
                Errors = errors,
                Message = message
            };
        }

        public static ApiResponse<T> FailureResult(string error, string message = "Operation failed")
        {
            return FailureResult(new[] { error }, message);
        }
    }

    public class ApiResponse
    {
        public bool Success { get; set; }
        public string[] Errors { get; set; } = Array.Empty<string>();
        public string Message { get; set; } = string.Empty;

        public static ApiResponse SuccessResult(string message = "Operation successful")
        {
            return new ApiResponse
            {
                Success = true,
                Message = message
            };
        }

        public static ApiResponse FailureResult(string[] errors, string message = "Operation failed")
        {
            return new ApiResponse
            {
                Success = false,
                Errors = errors,
                Message = message
            };
        }

        public static ApiResponse FailureResult(string error, string message = "Operation failed")
        {
            return FailureResult(new[] { error }, message);
        }
    }
}
