using sicalc.Parsing;

namespace sicalc.Evaluation
{
    public interface IVisitor<T>
    {
        public Result<T> Visit(Scalar entity);
        public Result<T> Visit(Number entity);
        public Result<T> Visit(AddExpr entity);
        public Result<T> Visit(SubtractExpr entity);
        public Result<T> Visit(MultiplyExpr entity);
        public Result<T> Visit(DivideExpr entity);
    }
}
