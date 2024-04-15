using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sicalc.Evaluation
{
    public static class ResultPrinter
    {
        public static void Print(Result<ScalarResult> result)
        {
            Console.WriteLine($"{result.Value.Number.Value} {result.Value.Unit.Units}");
        }

        public static void Print(Result<NumericResult> result)
        {
            Console.WriteLine($"{result.Value.Value}");
        }

        public static void Print(Result<UnitResult> result)
        {
            Console.WriteLine($"{result.Value}");
        }

        public static void Print(Result<string> result)
        {
            Console.WriteLine($"{result.Value}");
        }
    }
}
