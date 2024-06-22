namespace EquationSolver
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // for testing

            /*Console.Write("Adj meg egy függvényt: f(x) = ");
            string line = Console.ReadLine();
            Console.Write("Milyen értéket akarsz behelyettesíteni: ");
            double value = Convert.ToDouble(Console.ReadLine());
            string [] exp = line.Split(' ');
            List<string> expression = new List<string>();
            for(int i = 0; i < exp.Length; i++) 
            {
                expression.Add(exp[i]);
            }
            expression = PolishNotation.ToPolishForm(expression, false);
            expression = PolishNotation.ChangeX(expression, value);
            Console.WriteLine();
            Console.WriteLine(PolishNotation.EvaluatePolishForm(expression));*/

            // brute force solving


            //input

            Console.WriteLine("Adj meg egy 0 - ra rendezett egyenletet:\n");
            string line = Console.ReadLine();
            System.Console.Clear();
            Console.Write("Az egyenlet: \n\n" + line + " = 0");
            string[] exp = line.Split(' ');
            List<string> expression;
            List<string> expressionConst = new List<string>();

            for (int i = 0; i < exp.Length; i++)
            {
                expressionConst.Add(exp[i]);
            }

            // brute force solution, not using Bolzano's theorem

            double x = -69;

            for (int i = -999999; i < 999999; i++)
            {
                CopyLists(expressionConst, out expression);
                expression = PolishNotation.ToPolishForm(expression, false);
                expression = PolishNotation.ChangeX(expression, i);
                if (PolishNotation.EvaluatePolishForm(expression) == 0)
                {
                    x = i;
                    break;
                }
            }


            Console.WriteLine();
            Console.WriteLine("\n\nA megoldás: \n\nx = " + x);
        }
    
        public static void CopyLists(List<string> list, out List<string> list2) 
        {
            list2 = new List<string>();
            for (int i = 0; i < list.Count(); ++i)
            {
                list2.Add(list[i]);
            }
        }
    }
}
