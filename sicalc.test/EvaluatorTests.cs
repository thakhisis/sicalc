using sicalc.Evaluation;
using sicalc.Parsing;
using Sprache;
using System.ComponentModel;

namespace sicalc.test
{
    public class EvaluatorTests
    {
        [Fact]
        public void VisitNumber_Test()
        {
            var number = new Number(2);
            var evaluator = new NumericEvaluator();
            var result = number.Visit(evaluator);
            Assert.Equal(new NumericResult(2), result);
        }

        [Fact]
        public void VisitScalar_Numeric_Test()
        {
            var scalar = new Scalar(new Number(2), Parsing.Unit.Meter);
            var numericEvaluator = new NumericEvaluator();
            var numericResult = scalar.Visit(numericEvaluator);
            Assert.Equal(new NumericResult(2), numericResult);
        }

        [Fact]
        public void VisitScalar_Unit_Test()
        {
            var scalar = new Scalar(new Number(2), Parsing.Unit.Meter);
            var unitEvaluator = new UnitEvaluator();
            var unitResult = scalar.Visit(unitEvaluator);
            Assert.Equal(new UnitResult(new UnitList(new[] { Evaluation.Unit.Meter })), unitResult);
        }

        [InlineData("2+2", 4)]
        [InlineData("1+5", 6)]
        [InlineData("2-1", 1)]
        [InlineData("2*3", 6)]
        [Theory]
        public void VisitSimpleExpression_Test(string input, double value)
        {
            var expr = Parser.Parse(input);
            var result = expr.Visit(new NumericEvaluator());
            Assert.Equal(new NumericResult(value), result);
        }

        [Fact]
        public void VisitScalarExpression_Test()
        {
            var input = "2m+3m";
            var expr = Parser.Parse(input);
            var result = expr.Visit(new UnitEvaluator());
            Assert.Equal(new UnitResult(new UnitList([Evaluation.Unit.Meter])), result);
        }
    }
}
