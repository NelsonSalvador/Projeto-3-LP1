using System;
namespace Game
{
    public class Menu
    {
        public Game game;
        public HighScore highScore;
        public Menu(int rows, int columns)
        {
            UseMenu(rows, columns);
        }
        public void UseMenu(int rows, int columns)
        {
            string inputStr;

            Console.WriteLine("1. New game");
            Console.WriteLine("2. High scores");
            Console.WriteLine("3. Instructions");
            Console.WriteLine("4. Credits");
            Console.WriteLine("5. Quit");

            inputStr = Console.ReadLine();

            switch (inputStr)
            {
                case "1" :
                    game = new Game(rows,columns);
                    break;

                case "2" :
                    highScore = new HighScore(rows, columns, 0, 1);
                    //Console.Write("2"); 
                    break;

                case "3" :
                    Console.Write("3"); 
                    break;

                case "4" :
                    Console.Write("Game done by:\n");
                    Console.Write("Pedro Coutinho 21905323\n");
                    Console.Write("Nelson Salvador 21904295\n");
                    Console.Write("Miguel Martinho 21901530\n"); 
                    break;

                case "5" :
                    System.Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid Input");
                    break; 
            }
        }
    }
}