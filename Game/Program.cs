using System;

namespace Game
{
    
    class Program
    {
        static void Main(string[] args)
        {
            int rows = 0;
            int colunes = 0;
            if (args.Length == 0)
            {
                System.Console.WriteLine("Please enter a numeric argument.");
                Exit();
            }
            if (args [1] == "-r"){
                rows = Convert.ToInt32(args[2]);
                colunes = Convert.ToInt32(args[4]);
            }
            else if (args [1] == "-c")
            {
                colunes = Convert.ToInt32(args[2]);
                rows = Convert.ToInt32(args[4]);
            }
            else
            {
                System.Console.WriteLine("Please enter a numeric argument.");
                
                Exit();
            }
            MapGeneration a = new MapGeneration();
            a.Generate(rows,colunes);
        }

        static void Exit()
        {
            System.Environment.Exit(0);
        }
    }
}
