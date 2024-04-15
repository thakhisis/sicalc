using Sprache;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("sicalc.test")]
namespace sicalc.Parsing
{
    public enum ArithmeticExpressionToken { None, Number, Plus, Minus, Times, Divide, LParen, RParen }

    internal static class SIUnitsGrammer
    {
        private static readonly Parser<ArithmeticExpressionToken> Plus =
            Parse.Char('+').Token().Return(ArithmeticExpressionToken.Plus);

        private static readonly Parser<ArithmeticExpressionToken> Minus =
            Parse.Char('-').Token().Return(ArithmeticExpressionToken.Minus);

        private static readonly Parser<ArithmeticExpressionToken> Times =
            Parse.Char('*').Token().Return(ArithmeticExpressionToken.Times);

        private static readonly Parser<ArithmeticExpressionToken> Divide =
            Parse.Char('/').Token().Return(ArithmeticExpressionToken.Divide);

        public static readonly Parser<Number> Number =
            Parse.DecimalInvariant
                .Select(s => double.Parse(s, CultureInfo.InvariantCulture))
                .Select(v => new Number(v));

        public static readonly Parser<Unit> Unit =
                Parse.Char('s').Select(_ => Parsing.Unit.Second)
            .Or(Parse.Char('m').Select(_ => Parsing.Unit.Meter))
            .Or(Parse.String("kg").Select(_ => Parsing.Unit.Kilogram));

        public static readonly Parser<Expr> Scalar =
            from number in Number
            from unit in Unit.Token()
            select new Scalar(number, unit);

        public static readonly Parser<Expr> Literal =
                Scalar
            .Or(Number);

        public static readonly Parser<Expr> MultiplyOrDivide =
            Parse.ChainOperator(Times.Or(Divide), Literal, BinaryExpr.Make);

        public static readonly Parser<Expr> AddOrSubtract =
            Parse.ChainOperator(Plus.Or(Minus), MultiplyOrDivide, BinaryExpr.Make);

        public static readonly Parser<Expr> Expr =
            AddOrSubtract.End();
    }

    public class Parser
    {
        public static Expr Parse(string expr) => SIUnitsGrammer.Expr.Parse(expr);
    }
}
