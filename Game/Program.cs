using System;
using System.Text;
namespace Game
{
    
    class Program
    {
        
        static void Main(string[] args)
        {
            int rows = 0;
            int columns = 0;
            int i = 0;
            Console.OutputEncoding = Encoding.UTF8;
            
            if (args.Length < 2)
            {
                System.Console.WriteLine("Please run Program with with -r and -c arguments");
                System.Console.WriteLine("Exemple : dotnet run .\\Program.cs -- -c 21 -r 47");
                Exit();
            }
            if (args [0] == "-r"){
                rows = Convert.ToInt32(args[1]);
                columns = Convert.ToInt32(args[3]);
            }
            else if (args [0] == "-c")
            {
                columns = Convert.ToInt32(args[1]);
                rows = Convert.ToInt32(args[3]);
            }
            else
            {
                System.Console.WriteLine("Please run Program with with -r and -c arguments");
                System.Console.WriteLine("Exemple : dotnet run .\\Program.cs -- -c 21 -r 47");
                Exit();
            }
            
            while(i != 1)
            {
                Menu menu = new Menu(rows, columns);
            }
            
        }

        static void Exit()
        {
            System.Environment.Exit(0);
        }
    }
}
