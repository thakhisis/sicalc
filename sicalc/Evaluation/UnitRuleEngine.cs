
using sicalc.Evaluation;
using System;
using System.Runtime.CompilerServices;
using System.Text;

namespace sicalc.Evaluation
{
    public enum UnitType
    {
        Mass,
        Length,
        Time,
        Force,
        Temperature,
        ElectricCurrent,
        AmountOfSubstance,
        LuminousIntensity
    }

    public record Unit(string Name, string Abbreviation, UnitType UnitType, int Exponent = 1)
    {
        public static Unit Meter = new Unit("Meter", "m", UnitType.Length, 1);
        public static Unit Second = new Unit("Second", "s", UnitType.Time, 1);
        public static Unit Kilogram = new Unit("Kilogram", "kg", UnitType.Mass, 1);

        public static Unit FromAbbreviation(string abbreviation)
        {
            return abbreviation switch
            {
                "m" => Meter,
                "s" => Second,
                "kg" => Kilogram,
                _ => throw new ArgumentException($"Unknown unit abbreviation: {abbreviation}")
            };
        }

        public Unit Inverted() => this with { Exponent = -this.Exponent };
        public virtual Unit Squared() => this with { Exponent = this.Exponent * 2 };
        public Unit Cubed() => this with { Exponent = this.Exponent * 3 };

        public bool Includes(Unit unit) => this.Abbreviation == unit.Abbreviation && this.Exponent > unit.Exponent;

        public override string ToString()
        {
            if (this.Exponent == 0)
            {
                return "";
            }

            if (this.Exponent == 1)
            {
                return $"{this.Abbreviation}";
            }

            return $"{this.Abbreviation}^{this.Exponent}";
        }
    }

    public sealed record DerivedUnit(string Name, string Abbreviation, UnitType UnitType, UnitList Units) : Unit(Name, Abbreviation, UnitType)
    {
        public static IEnumerable<DerivedUnit> All = new List<DerivedUnit>
        {
            new DerivedUnit("Newton", "N", UnitType.Force, new UnitList(new[] { Unit.Kilogram, Unit.Meter, Unit.Second.Inverted().Squared() }))
        };

        public static DerivedUnit Newton => All.Single(u => u.Abbreviation == "N");

        public override DerivedUnit Squared()
        {
            return base.Squared() as DerivedUnit;
        }

        //public static implicit operator UnitList(DerivedUnit unit)
        public UnitList ToUnitList()
        {
            var ret = new List<Unit>();
            for (var i = 0; i < this.Exponent; i++)
            {
                ret = ret.Concat(this.Units.Units).ToList();
            }
            return new UnitList(ret);
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }

    public static class UnitRuleEngine
    {
        public static bool Has(this UnitList units, Unit unit) => units.Units.Any(u => u == unit) || units.Units.Any(u => u.Includes(unit));
        public static bool HasDerived(this UnitList units, DerivedUnit derivedUnit) => derivedUnit.ToUnitList().Units.All(u => units.Has(u));
        public static UnitList Simplify(this UnitList units)
        {
            var ret = new UnitList(units.Units);

            foreach (var dr in DerivedUnit.All)
            {
                if (units.HasDerived(dr))
                {
                    ret = ret
                        .Subtract(dr.Units)
                        .Add(dr);
                }
            }

            return ret;
        }
    }

    public sealed record UnitList
    {
        public List<Unit> Units { get; init; }
        public UnitList(IEnumerable<Unit> units)
        {
            if (units == null)
            {
                throw new ArgumentNullException(nameof(units));
            }

            var grouped = units.GroupBy(u => u.Abbreviation);
            var newUnits = grouped.Select(g => g.First() with { Exponent = g.Sum(u => u.Exponent) })
                .Where(u => u.Exponent != 0)
                .ToList();

            this.Units = newUnits;
        }

        public static UnitList Build(params Unit[] units) => new UnitList(units);

        public UnitList Add(Unit unit)
        {
            return new UnitList(this.Units.Concat(new[] { unit }));
        }

        public UnitList Add(UnitList unit)
        {
            return new UnitList(this.Units.Concat(unit.Units));
        }

        public UnitList Subtract(Unit unit)
        {
            return new UnitList(this.Units.Concat(new[] { unit.Inverted() }));
        }

        public UnitList Subtract(UnitList unit)
        {
            return new UnitList(this.Units.Concat(unit.Units.Select(u => u.Inverted())));
        }

        public override int GetHashCode()
        {
            int hashCode = 1;
            foreach (var unit in this.Units)
            {
                hashCode = (hashCode * 397) ^ unit.GetHashCode();
            }
            return hashCode;
        }

        public bool Equals(UnitList? other)
        {
            if (other == null)
            {
                return false;
            }

            return this.Units.SequenceEqual(other.Units);
        }

        public override string ToString()
        {
            if (this.Units.Count == 0)
            {
                return "[]";
            }

            return this.Units.Aggregate(new StringBuilder(), (sb, u) =>
            {
                if (sb.Length > 0)
                {
                    sb.Append(" * ");
                }

                sb.Append(u);
                return sb;
            }).ToString();
        }
    }
}
