namespace API.Errors
{
    public class ApiResponse
    {
        public ApiResponse(int statusCode, string? message = null)
        {
            StatusCode = statusCode;
            Message = message ?? GetDefaultMessaegStatusCode(statusCode);
        }

        private string GetDefaultMessaegStatusCode(int statusCode)
        {
            return statusCode switch
            {
                400 => "A bad request",
                401 => "tou are not authiorized",
                404 => "not found",
                500 => "something went wrong",
                _ => null
            };
        }

        public int StatusCode { get; set; }
        public string Message { get; set; }
    }
}
