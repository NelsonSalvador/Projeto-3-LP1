using System;
namespace Game
{
    /// <summary>
    /// Handles menu inputs and outputs
    /// </summary>
    public class Menu
    {
        /// <summary>
        /// Property that contains instance of game
        /// </summary>
        public Game game;
        /// <summary>
        /// Property that contains instance of highscores
        /// </summary>
        public HighScore highScore;
        /// <summary>
        /// Create menu
        /// </summary>
        /// <param name="rows"></param>
        /// <param name="columns"></param>
        public Menu(int rows, int columns)
        {
            UseMenu(rows, columns);
        }
        /// <summary>
        /// Print menu and handle inputs for menu
        /// </summary>
        /// <param name="rows"></param>
        /// <param name="columns"></param>
        public void UseMenu(int rows, int columns)
        {   
            //Variables to handle the input
            bool isValidInput = false;
            string inputStr;

            //Main menu
            Console.Clear();
            Console.WriteLine("1. New game");
            Console.WriteLine("2. High scores");
            Console.WriteLine("3. Instructions");
            Console.WriteLine("4. Credits");
            Console.WriteLine("5. Quit");

            //While the player does not give a valid input
            while(!isValidInput)
            {
                //Ask for input
                inputStr = Console.ReadLine();
                ConsoleKeyInfo input;

                //Validate and choose input of player
                switch (inputStr)
                {
                    case "1" :
                        game = new Game(rows,columns);
                        isValidInput = true;
                        break;

                    case "2" :

                        Console.Clear();
                        highScore = new HighScore(rows, columns, 0, 1);
                        input = Console.ReadKey();
                        UseMenu(rows, columns); 
                        break;

                    case "3" :

                        Console.Clear();
                        Console.Write("The rules are:\n");
                        Console.Write("You can move using 'WASD' or the " + 
                        "directional keys(▲, ►, ▼, ◄). ");
                        Console.Write("Each time you move, you lose 1 hp.\n");
                        Console.Write("There are walls(█) you must dodge so " + 
                        "you can go to your objective\n");

                        Console.Write("Enemies move towards you, they move " + 
                        "after you moved twice. ");

                        Console.Write("Minions(☼) deal 5 damage, " + 
                        "and bosses(☺) deal 10 damage.\n");

                        Console.Write("There are also powerups that heal you," +
                        " small ones(♠) heal you by 4 hp, ");

                        Console.Write("medium ones(♣) heal you by 8 hp and " +
                        "big ones(♥) heal you by 16 hp. \n");

                        Console.Write("To pass each level you must reach" + 
                        " de exit(֍).\n");

                        Console.Write("Your objective is simple go as far" + 
                        " as you can! By the way this is you (☻)\n");

                        input = Console.ReadKey();
                        UseMenu(rows, columns);
                        break;

                    case "4" :

                        Console.Clear();
                        Console.Write("Game done by:\n");
                        Console.Write("Pedro Coutinho 21905323\n");
                        Console.Write("Nelson Salvador 21904295\n");
                        Console.Write("Miguel Martinho 21901530\n"); 
                        input = Console.ReadKey();
                        UseMenu(rows, columns);
                        break;
                        

                    case "5" :
                        System.Environment.Exit(0);
                        isValidInput = true;
                        break;
                    default:
                        Console.WriteLine("Invalid Input");
                        break;
                }

                inputStr = "";
            }

        }
    }
}