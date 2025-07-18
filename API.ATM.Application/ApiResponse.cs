namespace API.ATM.Domain
{
    public sealed record ApiResponse<T>(
        bool Success,
        int? Code = null,
        string? Message = null,
        object? Details = null,
        T? Data = default
    )
    {
        public static ApiResponse<T> Ok(T data) => new(true, Data: data);
        public static ApiResponse<T> Fail(int code, string message, object? details = null)
            => new(false, code, message, details);
    }
}
