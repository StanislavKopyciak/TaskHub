namespace TaskHub.Application.Common
{
    public class Results<T>
    {
        public bool Success { get; set; }
        public string? Error { get; set; }
        public T? Value { get; set; }

        public static Results<T> Ok(T value) => new Results<T> { Success = true, Value = value };
        public static Results<T> Fail(string error) => new Results<T> { Success = false, Error = error };
    }

}
