using sicalc.Parsing;
using Sprache;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace sicalc.Evaluation
{
    public record NumericResult(double Value);

    public class NumericEvaluator : IVisitor<NumericResult>
    {
        public Result<NumericResult> Visit(Scalar scalar)
        {
            return new NumericResult(scalar.Number.Value);
        }

        public Result<NumericResult> Visit(Number entity)
        {
            return new NumericResult(entity.Value);
        }

        //public Result Visit1(BinaryExpr entity)
        //{
        //    var leftResult = entity.Left.Visit(this);
        //    var rightResult = entity.Right.Visit(this);

        //    var result = entity.Operator.Value switch
        //    {
        //        ArithmeticExpressionToken.Plus => left + right,
        //        ArithmeticExpressionToken.Minus => left - right,
        //        ArithmeticExpressionToken.Times => left * right,
        //        ArithmeticExpressionToken.Divide => left / right,
        //        _ => throw new NotImplementedException()
        //    };

        //    return new NumericResult(result);
        //}

        public Result<NumericResult> Visit(AddExpr expr)
        {
            var leftResult = expr.Left.Visit(this);
            var rightResult = expr.Right.Visit(this);

            return new NumericResult(leftResult.Value.Value + rightResult.Value.Value);
        }

        public Result<NumericResult> Visit(SubtractExpr expr)
        {
            var leftResult = expr.Left.Visit(this);
            var rightResult = expr.Right.Visit(this);

            return new NumericResult(leftResult.Value.Value - rightResult.Value.Value);
        }

        public Result<NumericResult> Visit(MultiplyExpr expr)
        {
            var leftResult = expr.Left.Visit(this);
            var rightResult = expr.Right.Visit(this);

            return new NumericResult(leftResult.Value.Value * rightResult.Value.Value);
        }

        public Result<NumericResult> Visit(DivideExpr expr)
        {
            var leftResult = expr.Left.Visit(this);
            var rightResult = expr.Right.Visit(this);

            if (rightResult.Value.Value == 0)
                throw new DivideByZeroException($"{rightResult}");

            return new NumericResult(leftResult.Value.Value / rightResult.Value.Value);
        }
    }
}
