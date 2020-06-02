using System;

namespace Game
{
    public class Game
    {
        public Player Player {get; set;}
        public Map Map {get; set;}
        public bool Win {get; set;}
        public Game(int rows, int columns)
        {
            Player = new Player(rows, columns);
            Map = new Map(rows, columns, Player);
            Win = false;
            StartGame(rows, columns, Player);       
        }
        public void StartGame(int rows, int columns, Player player)
        {
            while (!Player.IsDead() && !Win)
            {
                //player move
                GetInput(rows, columns);
                if (Win)
                    break;
                Console.WriteLine(Player.Coords.X + " , " + Player.Coords.Y);
                //refresh
                Map.refreshMap(rows, columns);
                //player move
                GetInput(rows, columns);
                Console.WriteLine(Player.Coords.X + " , " + Player.Coords.Y);
                //refresh
                Map.refreshMap(rows, columns);
                //newturn
            }
            if (Win)
            {
                Player = new Player(rows, columns);
                Map = new Map(rows, columns, Player);
                Win = false;
                StartGame(rows, columns, Player);
            }
        }
        public void GetInput(int rows, int columns)
        {
            Objects none = Objects.None; 
            string input = Console.ReadLine();
            switch (input)
            {
                case "w" :
                    if (checkMove(new Coords(Player.Coords.X-1, Player.Coords.Y), rows, columns))
                    {
                        none = Map.layout[new Coords(Player.Coords.X-1, Player.Coords.Y)];
                        Map.layout[Player.Coords] = Objects.None;
                        Player.Coords = new Coords(Player.Coords.X-1, Player.Coords.Y);
                        Map.layout[Player.Coords] = Objects.Player;
                    }
                    break;

                case "a" :
                    if (checkMove(new Coords(Player.Coords.X, Player.Coords.Y-1), rows, columns))
                    {
                        none = Map.layout[new Coords(Player.Coords.X, Player.Coords.Y-1)];
                        Map.layout[Player.Coords] = Objects.None;
                        Player.Coords = new Coords(Player.Coords.X, Player.Coords.Y-1);
                        Map.layout[Player.Coords] = Objects.Player;
                    }
                    break;

                case "s" :
                    if (checkMove(new Coords(Player.Coords.X+1, Player.Coords.Y), rows, columns))
                    {
                        none = Map.layout[new Coords(Player.Coords.X+1, Player.Coords.Y)];
                        Map.layout[Player.Coords] = Objects.None;
                        Player.Coords = new Coords(Player.Coords.X+1, Player.Coords.Y);
                        Map.layout[Player.Coords] = Objects.Player;
                    }
                    break;

                case "d" :
                    if (checkMove(new Coords(Player.Coords.X, Player.Coords.Y+1), rows, columns))
                    {
                        none = Map.layout[new Coords(Player.Coords.X, Player.Coords.Y+1)];
                        Map.layout[Player.Coords] = Objects.None;
                        Player.Coords = new Coords(Player.Coords.X, Player.Coords.Y+1);
                        Map.layout[Player.Coords] = Objects.Player;
                    }
                    break;
            }
            if (none == Objects.Victory)
            {
                Win = true;
            }
        }
        public bool checkMove(Coords coords, int rows, int columns)
        {
            if (!Map.layout.ContainsKey(coords))
            {
                return false;
            }
            if (Map.layout[coords] == Objects.None)
            {
                return true;
            }
            if (Map.layout[coords] == Objects.Victory)
            {
                return true;
            }
            return false;
        }
        

    }
}