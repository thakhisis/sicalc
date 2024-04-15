using sicalc.Parsing;
using Sprache;

namespace sicalc.Evaluation
{
    public class PrinterEvaluator : IVisitor<string>
    {
        public PrinterEvaluator() {
        }

        public Result<string> Visit(Scalar entity)
        {
            return $"{entity.Number.Value} {entity.Unit.Value}";
        }

        public Result<string> Visit(Number entity)
        {
            return $"{entity.Value}";
        }

        public Result<string> Visit(AddExpr entity)
        {
            var left = entity.Left.Visit(this);
            var right = entity.Right.Visit(this);

            return $"{left.Value} + {right.Value}";
        }

        public Result<string> Visit(SubtractExpr entity)
        {
            var left = entity.Left.Visit(this);
            var right = entity.Right.Visit(this);

            return $"{left.Value} - {right.Value}";
        }

        public Result<string> Visit(MultiplyExpr entity)
        {
            var left = entity.Left.Visit(this);
            var right = entity.Right.Visit(this);

            return $"{left.Value} * {right.Value}";
        }

        public Result<string> Visit(DivideExpr entity)
        {
            var left = entity.Left.Visit(this);
            var right = entity.Right.Visit(this);

            return $"{left.Value} / {right.Value}";
        }   
    }
}
