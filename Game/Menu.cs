using System;
namespace Game
{
    public class Menu
    {
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
                    Game game = new Game(rows,columns);
                    break;

                case "2" :
                    Console.Write("2"); 
                    break;

                case "3" :
                    Console.Write("3"); 
                    break;

                case "4" :
                    Console.Write("4"); 
                    break;

                case "5" :
                    Console.Write("5"); 
                    break;    
            }
        }
    }
}