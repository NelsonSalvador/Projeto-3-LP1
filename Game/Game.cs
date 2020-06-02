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
            Map = new Map(rows, columns, Player);
            StartGame(rows, columns, Player);       
        }
        public void StartGame(int rows, int columns, Player player)
        {
            while (!Player.IsDead())
            {
                //player move
                GetInput();
                Console.WriteLine(Player.Coords.X + " , " + Player.Coords.Y);
                //refresh
                Map.refreshMap(rows, columns, Player);
                //player move
                GetInput();
                Console.WriteLine(Player.Coords.X + " , " + Player.Coords.Y);
                //refresh
                Map.refreshMap(rows, columns, Player);
                //newturn 

            }
        }
        public void GetInput()
        {
            string input = Console.ReadLine();
            bool canMove;
            switch (input)
            {
                case "w" :
                    
                    canMove = checkMove(Player.Coords.X, Player.Coords.Y-1);
                    if (canMove == true)
                        Player.Coords = new Coords(Player.Coords.X, Player.Coords.Y-1);
                    break;

                case "a" :
                    canMove = checkMove(Player.Coords.X-1, Player.Coords.Y);
                    if (canMove == true)
                        Player.Coords = new Coords(Player.Coords.X-1, Player.Coords.Y);
                    break;

                case "s" :
                    canMove = checkMove(Player.Coords.X, Player.Coords.Y+1);
                    if (canMove == true)
                        Player.Coords = new Coords(Player.Coords.X, Player.Coords.Y+1);
                    break;

                case "d" :
                    canMove = checkMove(Player.Coords.X+1, Player.Coords.Y);
                    if (canMove == true)
                        Player.Coords = new Coords(Player.Coords.X+1, Player.Coords.Y);
                    break;
            }
        }

        public bool checkMove(int PlayerX, int PlayerY)
        {
            if (Map.Iswall[PlayerY, PlayerX] == 1)
            {
                return false;
            }

            return true;
        }

        public void CheckWin()
        {

        }
    }
}