using sicalc.Parsing;
using System.ComponentModel;
using Sprache;

namespace sicalc.test
{
    public class ParserTests
    {
        [Fact]
        public void ParseNumber_Test()
        {
            var expression = "2";
            var result = SIUnitsGrammer.Number.Parse(expression);
            Assert.Equal(new Number(2), result);
        }

        [Fact]
        public void ParseUnit_Test()
        {
            var expression = "m";
            var result = SIUnitsGrammer.Unit.Parse(expression);
            Assert.Equal(Unit.Meter, result);
        }


        [Fact]
        public void ParseScalar_Test()
        {
            var expression = "2m";
            var result = SIUnitsGrammer.Scalar.Parse(expression);
            Assert.Equal(new Scalar(new Number(2), Unit.Meter), result);
        }
        

        [Fact]
        public void ParseScalar_WhiteSpace_Test()
        {
            var expression = "2 m";
            var result = SIUnitsGrammer.Scalar.Parse(expression);
            Assert.Equal(new Scalar(new Number(2), Unit.Meter), result);
        }

        [Fact]
        public void ParseNumericExpression_Test()
        {
            var expression = "2+2";
            var result = SIUnitsGrammer.AddOrSubtract.Parse(expression);
            Assert.Equal(new AddExpr(new Number(2), new Number(2)), result);
        }

        [Fact]
        public void ParseNumericExpression_WhiteSpace_Test()
        {
            var expression = "2 +  2";
            var result = SIUnitsGrammer.AddOrSubtract.Parse(expression);
            Assert.Equal(new AddExpr(new Number(2), new Number(2)), result);
        }

        [Fact]
        public void ParseNumericMultiplyScalarExpression_Test()
        {
            var expression = "2*2m";
            var result = Parser.Parse(expression);
            Assert.Equal(new MultiplyExpr(new Number(2), new Scalar(new Number(2), Unit.Meter)), result);
        }

        [Fact]
        public void ParseNumericUnitExpression_Test()
        {
            var expression = "2m+2s";
            var result = Parser.Parse(expression);
            Assert.Equal(new AddExpr(new Scalar(new Number(2), Unit.Meter), new Scalar(new Number(2), Unit.Second)), result);
        }

        [Fact]
        public void ParseNumericUnitExpression_Triple_Test()
        {
            var expression = "2m+2s+5m";
            var result = Parser.Parse(expression);
            Assert.Equal(new AddExpr(new AddExpr(new Scalar(new Number(2), Unit.Meter), new Scalar(new Number(2), Unit.Second)), new Scalar(new Number(5), Unit.Meter)), result);
        }

        [Fact]
        public void ParseExpression_NumericAndUnit_ParseException_Test()
        {
            var expression = "2+m";
            var result = () => Parser.Parse(expression);
            Assert.Throws<ParseException>(result);
        }
    }
}
