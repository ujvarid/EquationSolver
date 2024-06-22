namespace EquationSolver
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Adj meg egy függvényt: f(x) = ");
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
            Console.WriteLine(PolishNotation.EvaluatePolishForm(expression));
        }
    }
}
