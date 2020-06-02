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
                Map.refreshMap(rows, columns);
                //player move
                GetInput();
                Console.WriteLine(Player.Coords.X + " , " + Player.Coords.Y);
                //refresh
                Map.refreshMap(rows, columns);
                //newturn
                
            }
        }
        public void GetInput()
        {
            string input = Console.ReadLine();
            switch (input)
            {
                case "w" :
                    if (checkMove(new Coords(Player.Coords.X-1, Player.Coords.Y)))
                    {
                        Map.layout[Player.Coords] = Objects.None;
                        Player.Coords = new Coords(Player.Coords.X-1, Player.Coords.Y);
                        Map.layout[Player.Coords] = Objects.Player;
                    }
                    break;

                case "a" :
                    if (checkMove(new Coords(Player.Coords.X, Player.Coords.Y-1)))
                    {
                        Map.layout[Player.Coords] = Objects.None;
                        Player.Coords = new Coords(Player.Coords.X, Player.Coords.Y-1);
                        Map.layout[Player.Coords] = Objects.Player;
                    }
                    break;

                case "s" :
                    if (checkMove(new Coords(Player.Coords.X+1, Player.Coords.Y)))
                    {
                        Map.layout[Player.Coords] = Objects.None;
                        Player.Coords = new Coords(Player.Coords.X+1, Player.Coords.Y);
                        Map.layout[Player.Coords] = Objects.Player;
                    }
                    break;

                case "d" :
                    if (checkMove(new Coords(Player.Coords.X, Player.Coords.Y+1)))
                    {
                        Map.layout[Player.Coords] = Objects.None;
                        Player.Coords = new Coords(Player.Coords.X, Player.Coords.Y+1);
                        Map.layout[Player.Coords] = Objects.Player;
                    }
                    break;
            }
        }
        public bool checkMove(Coords coords)
        {
            if (!Map.layout.ContainsKey(coords))
            {
                return false;
            }
            if (Map.layout[coords] == Objects.None)
            {
                return true;
            }
            return false;
        }

        public void CheckWin()
        {

        }
    }
}