namespace sicalc.Evaluation
{
    public interface IElement
    {
        public Result<T> Visit<T>(IVisitor<T> visitor);
        public string Display();
    }
}
