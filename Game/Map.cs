using System;
using System.Collections.Generic;

namespace Game
{
    public class Map
    {
        public Dictionary<Coords , Objects> layout {get; set;}
        public Random Random {get; set;}
        public Player Player {get; set;}
        public Map(int rows, int columns, Player player)
        {
            Player = player;
            Random = new Random();
            layout = new Dictionary<Coords, Objects>();

            Generate(rows, columns, player);
        }
        public void Generate(int rows, int columns, Player player)
        {
            for (int i = 0; i < rows; i++)
            {
                for ( int j = 0; j < columns; j++)
                {
                    layout.Add(new Coords( i, j), Objects.None);
                }
            }
            Coords spawn = new Coords(Random.Next(rows), 0);
            layout[spawn] = Objects.Player;
            player.Coords = spawn;

            Coords Victory = new Coords(Random.Next(rows), columns-1);
            layout[Victory] = Objects.Win;

            int numberofwalls = (Math.Min(rows, columns))-1;
            while (numberofwalls != 0)
            {
                Coords wall = new Coords(Random.Next(rows), Random.Next(columns));
                Console.WriteLine(wall);
                if (layout[wall] == Objects.None)
                {
                    layout[wall] = Objects.Wall;
                    numberofwalls--;
                }
            }
            refreshMap(rows, columns);
        }

        public void refreshMap(int rows, int columns)
        {
            foreach(KeyValuePair<Coords, Objects> tile in layout)
            {
                switch(tile.Value)
                {
                    case Objects.None:
                        Console.Write(".");
                        break;
                    case Objects.Player:
                        Console.Write("P");
                        break;
                    case Objects.Wall:
                        Console.Write("#");
                        break;
                    case Objects.Win:
                        Console.Write("O");
                        break;
                }

                if (tile.Key.Y == (columns-1))
                {
                    Console.WriteLine("");
                }
            }
        }
    }
}