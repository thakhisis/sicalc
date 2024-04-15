// See https://aka.ms/new-console-template for more information
using sicalc;
using sicalc.Evaluation;
using sicalc.Parsing;
using System.Linq.Expressions;

var expression = "2kg * 2m / 3s / 1s";

Console.WriteLine($"Input: \"{expression}\"");

var expr = Parser.Parse(expression);

var parsedResult = expr.Visit(new PrinterEvaluator());

Console.Write("Parsed: ");
ResultPrinter.Print(parsedResult);

var evaluator = new ScalarEvaluator(
    new NumericEvaluator(),
    new UnitEvaluator()
);

var result = expr.Visit(evaluator);

Console.Write("Result: ");
ResultPrinter.Print(result);

