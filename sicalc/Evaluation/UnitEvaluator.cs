using sicalc.Parsing;

namespace sicalc.Evaluation
{
    public record UnitResult
    {
        public UnitList Units { get; }

        public UnitResult(UnitList units)
        {
            Units = units.Simplify();
        }
    }

    public class UnitEvaluator : IVisitor<UnitResult>
    {
        public Result<UnitResult> Visit(Scalar scalar)
        {
            return new UnitResult(new UnitList([Unit.FromAbbreviation(scalar.Unit.Value)]));
        }

        public Result<UnitResult> Visit(Number entity)
        {
            return new UnitResult(new UnitList([]));
        }

        public Result<UnitResult> Visit(AddExpr expr)
        {
            var leftResult = expr.Left.Visit(this);
            var rightResult = expr.Right.Visit(this);

            if (leftResult != rightResult)
            {
                throw new InvalidOperationException($"Cannot add units {leftResult.Value} and {rightResult.Value}");
            }

            return leftResult.Value;
        }

        public Result<UnitResult> Visit(SubtractExpr expr)
        {
            var leftResult = expr.Left.Visit(this);
            var rightResult = expr.Right.Visit(this);

            if (leftResult.Value != rightResult.Value)
            {
                throw new InvalidOperationException($"Cannot subtract units {leftResult.Value} and {rightResult.Value}");
            }

            return leftResult.Value;
        }

        public Result<UnitResult> Visit(MultiplyExpr expr)
        {
            var leftResult = expr.Left.Visit(this);
            var rightResult = expr.Right.Visit(this);

            return new UnitResult(leftResult.Value.Units.Add(rightResult.Value.Units));
        }

        public Result<UnitResult> Visit(DivideExpr expr)
        {
            var leftResult = expr.Left.Visit(this);
            var rightResult = expr.Right.Visit(this);

            return new UnitResult(leftResult.Value.Units.Subtract(rightResult.Value.Units));
        }
    }
}
