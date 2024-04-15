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
    public record ScalarResult(NumericResult Number, UnitResult Unit);

    public class ScalarEvaluator : IVisitor<ScalarResult>
    {
        private NumericEvaluator NumericEvaluator { get; }
        private UnitEvaluator UnitEvaluator { get; }

        public ScalarEvaluator(NumericEvaluator numericEvaluator, UnitEvaluator unitEvaluator)
        {
            this.NumericEvaluator = numericEvaluator;
            this.UnitEvaluator = unitEvaluator;
        }

        public Result<ScalarResult> Visit(Scalar entity)
        {
            var numeric = entity.Visit(NumericEvaluator);
            var unit = entity.Visit(UnitEvaluator);

            return new ScalarResult(numeric.Value, unit.Value);
        }

        public Result<ScalarResult> Visit(Number entity)
        {
            var numeric = entity.Visit(NumericEvaluator);
            var unit = entity.Visit(UnitEvaluator);

            return new ScalarResult(numeric.Value, unit.Value);
        }

        public Result<ScalarResult> Visit(AddExpr entity)
        {
            var numeric = entity.Visit(NumericEvaluator);
            var unit = entity.Visit(UnitEvaluator);

            return new ScalarResult(numeric.Value, unit.Value);
        }

        public Result<ScalarResult> Visit(SubtractExpr entity)
        {
            var numeric = entity.Visit(NumericEvaluator);
            var unit = entity.Visit(UnitEvaluator);

            return new ScalarResult(numeric.Value, unit.Value);
        }

        public Result<ScalarResult> Visit(MultiplyExpr entity)
        {
            var numeric = entity.Visit(NumericEvaluator);
            var unit = entity.Visit(UnitEvaluator);

            return new ScalarResult(numeric.Value, unit.Value);
        }

        public Result<ScalarResult> Visit(DivideExpr entity)
        {
            var numeric = entity.Visit(NumericEvaluator);
            var unit = entity.Visit(UnitEvaluator);

            return new ScalarResult(numeric.Value, unit.Value);
        }
    }
}
