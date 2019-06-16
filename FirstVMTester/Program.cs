using System;
using System.IO;
using System.Diagnostics;

using FirstVM.Lexis;
using FirstVM.Parser;
using FirstVM.AST;
namespace FirstVMTester
{
    class Program
    {
        static void Main(string[] args)
        {
            string code = File.ReadAllText("code.links");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("[SourceCode]");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(code);
            Console.WriteLine();


            LexicalAnalyzer analyzer = new LexicalAnalyzer();
            Stopwatch swAnalyzer = Stopwatch.StartNew();
            Token[] tokens = analyzer.Analyze(code);
            swAnalyzer.Stop();

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("[Tokens]");
            Console.ForegroundColor = ConsoleColor.Magenta;
            PrintTokens(tokens);
            Console.WriteLine();

            
            TokenParser parser = new TokenParser();
            Stopwatch swParsing = Stopwatch.StartNew();
            IStatement statement = parser.Parse(tokens);
            swParsing.Stop();

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("[Statements]");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(statement.ToString());
            Console.WriteLine();


            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("[Execute]");
            Console.ForegroundColor = ConsoleColor.Gray;
            Stopwatch swExecute = Stopwatch.StartNew();
            statement.Execute();
            swExecute.Stop();
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("[EndExecute]");
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("[TimeStats]");
            Console.WriteLine("AnalyzeTime: " + swAnalyzer.Elapsed.ToString());
            Console.WriteLine("ParsingTime: " + swParsing.Elapsed.ToString());
            Console.WriteLine("ExecuteTime: " + swExecute.Elapsed.ToString());


            Console.ReadKey(true); Console.ReadKey(true); Console.ReadKey(true);
        }

        static void PrintTokens(Token[] tokens)
        {
            for (int i = 0; i < tokens.Length; i++)
                Console.WriteLine(tokens[i].ToString());
        }
        static void PrintExpression(IExpression exp)
        {
            Console.WriteLine(exp.ToString() + " = " + exp.Eval().ToString());
        }
    }
}