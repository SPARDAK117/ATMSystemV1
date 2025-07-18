using API.ATM.Domain;

namespace API.ATM.Application
{
    public static class ApiEnvelope
    {
        public static ApiResponse<object> Ok(object? d = null)
            => ApiResponse<object>.Ok(d!);

        public static ApiResponse<object> Fail(int code, string message, object? details = null)
            => ApiResponse<object>.Fail(code, message, details);
    }
}
