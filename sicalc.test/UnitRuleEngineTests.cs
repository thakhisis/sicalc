using sicalc.Evaluation;
using Sprache;
using System.ComponentModel;

namespace sicalc.test
{
    public class UnitRuleEngineTests
    {
        [Fact]
        public void UnitRuleEngine_Has_Test()
        {
            var list = UnitList.Build(Unit.Meter, Unit.Second);
            var element = Unit.Second;
            Assert.True(list.Has(element));
        }

        [Fact]
        public void UnitRuleEngine_Has2_Test()
        {
            var list = UnitList.Build(Unit.Meter, Unit.Second, Unit.Second);
            var element = Unit.Second;
            Assert.True(list.Has(element), $"{list} does not contain {element}");
        }


        [Fact]
        public void UnitRuleEngine_HasDerived_Same_Test()
        {
            var list = UnitList.Build(Unit.Kilogram, Unit.Meter, Unit.Second.Inverted().Squared());
            var element = DerivedUnit.Newton;
            Assert.True(list.HasDerived(element), $"{list} does not contain {element}");
        }

        [Fact]
        public void UnitRuleEngine_HasDerived_Test()
        {
            var list = UnitList.Build(Unit.Kilogram.Squared(), Unit.Meter, Unit.Second.Inverted().Squared());
            var element = DerivedUnit.Newton;
            Assert.True(list.HasDerived(element), $"{list} does not contain {element}");
        }

        [Fact]
        public void UnitRuleEngine_Simplify_Test()
        {
            var list = UnitList.Build(Unit.Kilogram, Unit.Meter, Unit.Second.Inverted().Squared());
            var simplified = list.Simplify();
            Assert.True(simplified.Units.Count == 1, $"{simplified} does not have 1 element (should be N)");
            var element = simplified.Units.Single();
            Assert.True("N" == element.Abbreviation, $"{element} is not N");
        }
    }
}
