using System;

namespace Game
{
    public class Game
    {
        public Player Player {get; set;}
        public Map Map {get; set;}


        public Game(int rows, int columns)
        {
            Player = new Player(rows, columns);
            Map map = new Map(rows, columns, Player);

            StartGame(rows,columns);      
        }
        public void StartGame(int rows, int columns, string[,] map)
        {
            while (!Player.IsDead())
            {
                GetInput();
                Console.WriteLine(Player.Coords.X + " , " + Player.Coords.Y);
                Outputs.pritMap(map, rows, columns);

                GetInput();
                Console.WriteLine(Player.Coords.X + " , " + Player.Coords.Y);
                Outputs.pritMap(map, rows, columns);
            }
        }
        public void GetInput()
        {
            string input = Console.ReadLine();
            switch (input)
            {
                case "w" :
                    Player.Coords = new Coords(Player.Coords.X, Player.Coords.Y-1);
                    break;

                case "a" :
                    Player.Coords = new Coords(Player.Coords.X-1, Player.Coords.Y);
                    break;

                case "s" :
                    Player.Coords = new Coords(Player.Coords.X, Player.Coords.Y+1);
                    break;

                case "d" :
                    Player.Coords = new Coords(Player.Coords.X+1, Player.Coords.Y);
                    break;
            }
        }
        public void CheckWin()
        {

        }
    }
}