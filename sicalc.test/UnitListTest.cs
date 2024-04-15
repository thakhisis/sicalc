using sicalc.Evaluation;
using Sprache;
using System.ComponentModel;

namespace sicalc.test
{
    public class UnitListTest
    {
        [Fact]
        public void Unit_Equals_Test()
        {
            var unit1 = Unit.Meter;
            var unit2 = Unit.Meter;
            Assert.Equal(unit1, unit2);
        }
        [Fact]
        public void DerivedUnit_Equals_Test()
        {
            var unit1 = DerivedUnit.Newton;
            var unit2 = DerivedUnit.Newton;
            Assert.Equal(unit1, unit2);
        }

        [Fact]
        public void UnitWithExponentDefault_Equals_Test()
        {
            var unitWithExponent1 = Unit.Meter;
            var unitWithExponent2 = Unit.Meter;
            Assert.Equal(unitWithExponent1, unitWithExponent2);
        }
        
        [Fact]
        public void UnitWithExponentCustom_Equals_Test()
        {
            var unitWithExponent1 = Unit.Meter;
            var unitWithExponent2 = new Unit("Meter", "m", UnitType.Length, 1);
            Assert.Equal(unitWithExponent1, unitWithExponent2);
        }
        
        [Fact]
        public void UnitWithExponentCustom_NotEquals_Test()
        {
            var unitWithExponent1 = Unit.Meter;
            var unitWithExponent2 = Unit.Meter.Squared();
            Assert.NotEqual(unitWithExponent1, unitWithExponent2);
        }

        [Fact]
        public void UnitList_Equals_Test()
        {
            var unitList1 = new UnitList(new[] { Unit.Meter, Unit.Second });
            var unitList2 = new UnitList(new[] { Unit.Meter, Unit.Second });
            Assert.Equal(unitList1, unitList2);
        }

        [Fact]
        public void UnitList_Derived_Equals_Test()
        {
            var devUnit = DerivedUnit.Newton.ToUnitList();
            var list = UnitList.Build(Unit.Kilogram, Unit.Meter, Unit.Second.Inverted().Squared());

            Assert.Equal(list, devUnit);
        }

        [Fact]
        public void UnitList_Derived_Sqaured_Equals_Test()
        {
            var devUnit = DerivedUnit.Newton.Squared().ToUnitList();
            var list = UnitList.Build(Unit.Kilogram.Squared(), Unit.Meter.Squared(), Unit.Second.Inverted().Squared().Squared());

            Assert.Equal(list, devUnit);
        }

        [Fact]
        public void UnitList_Add_Test()
        {
            var unitList1 = new UnitList([])
                .Add(Unit.Meter)
                .Add(Unit.Second);

            var unitList2 = new UnitList(new[] { Unit.Meter, Unit.Second });

            Assert.Equal(unitList1, unitList2);
        }

        [Fact]
        public void UnitList_Add_Collapse_Test()
        {
            var unitList1 = new UnitList([])
                .Add(Unit.Meter)
                .Add(Unit.Meter);

            var unitList2 = new UnitList(new[] { new Unit("Meter", "m", UnitType.Length, 2) });

            Assert.Equal(unitList1, unitList2);
        }

        [Fact]
        public void UnitList_Subtract_Collapse_Test()
        {
            var unitList1 = new UnitList([])
                .Add(Unit.Meter)
                .Subtract(Unit.Meter);

            var unitList2 = new UnitList([]);

            Assert.Equal(unitList1, unitList2);
        }

        [Fact]
        public void UnitList_Immutable_Test()
        {
            var list1 = UnitList.Build(Unit.Meter);
            var list2 = list1.Add(Unit.Second);
            var list3 = list2.Subtract(Unit.Meter);
            Assert.Equal(list1, UnitList.Build(Unit.Meter));
            Assert.Equal(list2, UnitList.Build(Unit.Meter, Unit.Second));
            Assert.Equal(list3, UnitList.Build(Unit.Second));
        }

        [Fact]
        public void UnitList_Count_With_Derived_Test()
        {
            var list1 = UnitList.Build(DerivedUnit.Newton);
            Assert.Single(list1.Units);
        }

        [Fact]
        public void UnitList_Count_With_Derived_And_Normal_Test()
        {
            var list1 = UnitList.Build(DerivedUnit.Newton);
            var list2 = list1.Add(Unit.Meter);
            Assert.Single(list1.Units);
            Assert.Equal(2, list2.Units.Count());
        }
    }
}
