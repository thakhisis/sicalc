using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using sicalc.Evaluation;

namespace sicalc.Parsing
{
    public abstract record Expr : IElement
    {
        public abstract Result<T> Visit<T>(IVisitor<T> visitor);

        public abstract string Display();
    }

    public abstract record Literal : Expr;

    public record Number(double Value) : Literal
    {
        public override Result<T> Visit<T>(IVisitor<T> visitor)
            => visitor.Visit(this);

        public override string Display() => $"{Value}";
    }

    public record Unit
    {
        public string Value { get; }
        private Unit(string value) => Value = value;

        public static Unit Second = new Unit("s");
        public static Unit Meter = new Unit("m");
        public static Unit Kilogram = new Unit("kg");
    }

    public record Scalar(Number Number, Unit Unit) : Literal
    {
        public override Result<T> Visit<T>(IVisitor<T> visitor)
            => visitor.Visit(this);

        public override string Display() => $"{Number} {Unit}";
    }

    public record Operator(ArithmeticExpressionToken Value)
    {
        public static Operator Plus = new Operator(ArithmeticExpressionToken.Plus);
        public static Operator Minus = new Operator(ArithmeticExpressionToken.Minus);
        public static Operator Times = new Operator(ArithmeticExpressionToken.Times);
        public static Operator Divide = new Operator(ArithmeticExpressionToken.Divide);
        public override string ToString()
        {
            return Value switch
            {
                ArithmeticExpressionToken.Plus => "+",
                ArithmeticExpressionToken.Minus => "-",
                ArithmeticExpressionToken.Times => "*",
                ArithmeticExpressionToken.Divide => "/",
                _ => throw new NotImplementedException()
            };
        }
    }


    public abstract record BinaryExpr(Expr Left, Operator Operator, Expr Right) : Expr
    {

        public override string Display() => $"{Left} {Operator} {Right}";

        public static BinaryExpr Make(ArithmeticExpressionToken Operator, Expr Left, Expr Right)
        {
            return Operator switch
            {
                ArithmeticExpressionToken.Plus => new AddExpr(Left, Right),
                ArithmeticExpressionToken.Minus => new SubtractExpr(Left, Right),
                ArithmeticExpressionToken.Times => new MultiplyExpr(Left, Right),
                ArithmeticExpressionToken.Divide => new DivideExpr(Left, Right),
                _ => throw new NotImplementedException()
            };
        }
    }

    public record AddExpr(Expr Left, Expr Right) : BinaryExpr(Left, Operator.Plus, Right)
    {
        public override Result<T> Visit<T>(IVisitor<T> visitor)
            => visitor.Visit(this);
    }
    public record SubtractExpr(Expr Left, Expr Right) : BinaryExpr(Left, Operator.Minus, Right)
    {
        public override Result<T> Visit<T>(IVisitor<T> visitor)
            => visitor.Visit(this);
    }
    public record MultiplyExpr(Expr Left, Expr Right) : BinaryExpr(Left, Operator.Times, Right)
    {
        public override Result<T> Visit<T>(IVisitor<T> visitor)
            => visitor.Visit(this);
    }
    public record DivideExpr(Expr Left, Expr Right) : BinaryExpr(Left, Operator.Divide, Right)
    {
        public override Result<T> Visit<T>(IVisitor<T> visitor)
            => visitor.Visit(this);
    }
}
