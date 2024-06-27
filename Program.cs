using System.Linq.Expressions;

namespace EquationSolver
{
    internal class Program
    {
        static void Main(string[] args)
        {

            //input

            Console.WriteLine("Adj meg egy 0 - ra rendezett egyenletet:\n");

            string line = Console.ReadLine();
            System.Console.Clear();
            Console.Write("Az egyenlet: \n\n" + line + " = 0");
            string[] exp = line.Split(' ');
            List<string> expression;
            List<string> expressionConst = new();

            for (int i = 0; i < exp.Length; i++)
            {
                expressionConst.Add(exp[i]);
            }

            // using Bolzano's Intermediate Value Theorem

            CopyLists(expressionConst, out expression);
            int a, b;

            // 1. step: finding two places where the signs of the function values ​​differ

            (a, b) = PlacesWhereTheSignsDiffer(expression);

            // 2. step: interval halving

            (a, b) = IntervalHalving(expression, a, b);

            if (a == b)
            {
                Console.WriteLine();
                Console.WriteLine("\n\nA megoldás: \n\nx = " + a);
                Console.ReadKey(); 
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("\n\nA megoldás: \n\nx: " + a + " és " + b + " között van");
                Console.ReadKey(); 
            }
        }

        public static void CopyLists(List<string> list, out List<string> list2) 
        {
            list2 = new List<string>();
            for (int i = 0; i < list.Count(); ++i)
            {
                list2.Add(list[i]);
            }
        }

        public static (int a, int b) PlacesWhereTheSignsDiffer(List<string> function)
        {
            int a = FindSignChangePoint(function, positive: false);
            int b = FindSignChangePoint(function, positive: true);
            return (a, b);
        }

        public static int FindSignChangePoint(List<string> function, bool positive)
        {
            int k = 0;
            List<string> expressionConst = new List<string>(function);

            for (int i = positive ? 99999 : -99999; positive ? i >= -999 : i <= 999; i += positive ? -1 : 1)
            {
                CopyLists(expressionConst, out function);
                function = PolishNotation.ToPolishForm(function, false);
                function = PolishNotation.ChangeX(function, i);
                double evalResult = PolishNotation.EvaluatePolishForm(function);
                if ((positive && evalResult > 0) || (!positive && evalResult < 0))
                {
                    k = i;
                    break;
                }
            }
            return k;
        }

        public static (int, int) IntervalHalving(List<string> function, int a, int b)
        {
            List<string> expressionConst = new();

            for (int i = 0; i < function.Count; i++)
            {
                expressionConst.Add(function[i]);
            }

            int z = (a + b) / 2;

            for(int i = 0; i < 5000; ++i)
            {
                CopyLists(expressionConst, out function);
                function = PolishNotation.ToPolishForm(function, false);
                function = PolishNotation.ChangeX(function, z);
                double evalResult = PolishNotation.EvaluatePolishForm(function);
                if (evalResult == 0)
                {
                    return (z, z);
                }
                else if (evalResult > 0)
                {
                    b = z;
                }
                else
                {
                    a = z;
                }
                z = (a + b) / 2;
            }
            return (a, b);
        }
    }
}
