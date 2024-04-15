namespace sicalc.Evaluation
{
    public record Result<T>(T Value)
    {
        public static implicit operator Result<T>(T result)
        {
            return new Result<T>(result);
        }
    }
}
