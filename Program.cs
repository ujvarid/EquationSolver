using System.Linq.Expressions;



namespace EquationSolver
{
    internal class Program
    {
        private const int IVMAX = 99999;
        private const int IVMIN = -99999;

        static void Main(string[] args)
        {

            //input

            Console.WriteLine("Enter an equation set to zero:\n");

            string line = Console.ReadLine();
            System.Console.Clear();
            Console.Write($"The equation: \n\n{line} = 0");
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
                if (a == 0 && Evaluating(expressionConst, expression, 0) != 0)
                {
                    Console.WriteLine($"\n\n\nSorry, I could not find a solution given the interval [{IVMIN}, {IVMAX}]");
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("\n\nThe solution: \n\nx = " + a);
                    Console.ReadKey();
                }
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine($"\n\nThe solution: \n\nx is in the interval ({a}, {b})");
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

            for (int i = positive ? IVMAX : IVMIN; positive ? i >= IVMIN : i <= IVMAX; i += positive ? -1 : 1)
            {
                double evalResult = Evaluating(expressionConst, function, i);
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
                double evalResult = Evaluating(expressionConst, function, z);
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

        public static double Evaluating(List<string> expressionConst, List<string> function, int a)
        {
            CopyLists(expressionConst, out function);
            function = PolishNotation.ToPolishForm(function, false);
            function = PolishNotation.ChangeX(function, a);
            return PolishNotation.EvaluatePolishForm(function);
        }
    }
}
