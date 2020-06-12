using System;
using System.Text;
namespace Game
{
    /// <summary>
    /// Holds Main, there for start of the program
    /// </summary>
    class Program
    {
        /// <summary>
        /// Start of the program
        /// </summary>
        /// <param name="args">
        /// Program arguments are as followed :
        /// <term><c>-r </c></term>
        /// <description>Defines the number of rows the game
        /// table will have </description>
        /// <term><c>-c </c></term>
        /// <description>Defines the number os columns the game
        /// table will have </description>
        /// </param>
        static void Main(string[] args)
        {
            //Create variables 
            int rows = 0;
            int columns = 0;
            Console.OutputEncoding = Encoding.UTF8;
            
            //Check if there are enough arguments
            if (args.Length < 2)
            {
                System.Console.WriteLine("Please run Program with with" + 
                " -r and -c arguments");

                System.Console.WriteLine("Example : dotnet run -- -r 21 -c 47");
                System.Environment.Exit(0);
            }
            //if the first argument is -r define rows first and columns next
            if (args [0] == "-r" && args[2] == "-c"){
                rows = Convert.ToInt32(args[1]);
                columns = Convert.ToInt32(args[3]);
            }
            //if the first argument is -c define columns first and rows next
            else if (args [0] == "-c" && args [2] == "-r")
            {
                columns = Convert.ToInt32(args[1]);
                rows = Convert.ToInt32(args[3]);
            }
            //if arguments are none of the above print example 
            else
            {
                System.Console.WriteLine("Please run Program with with" + 
                " -r and -c arguments");
                System.Console.WriteLine("Example : dotnet run -- -c 21 -r 47");
                System.Environment.Exit(0);
            }
            
            //infinite loop for game
            while(true)
            {
                Menu menu = new Menu(rows, columns);
            }
            
        }
    }
}
