using System;

namespace Game
{
    
    class Program
    {
        static void Main(string[] args)
        {
            int rows = 0;
            int colunes = 0;
            if (args.Length < 2)
            {
                System.Console.WriteLine("Please run Program with with -r and -c arguments");
                System.Console.WriteLine("Exemple : dotnet run .\\Program.cs -- -c 21 -r 47");
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
                System.Console.WriteLine("Please run Program with with -r and -c arguments");
                System.Console.WriteLine("Exemple : dotnet run .\\Program.cs -- -c 21 -r 47");
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
