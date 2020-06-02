using System;
using System.Collections.Generic;

namespace Game
{
    public class Map
    {
        public Dictionary<Coords , Objects> layout {get; set;}
        public Map(int rows, int columns, Player player)
        {
            Random rand = new Random();
            
            layout = new Dictionary<Coords, Objects>();

            Generate(rows, columns, player, rand);
            refreshMap(rows, columns, player);
        }
        public void Generate(int rows, int columns, Player player, Random rand)
        {
            int numberofwalls = (Math.Min(rows, columns))-1;

            Coords spawn = new Coords(rand.Next(rows), 0);
            layout[spawn] = Objects.Player;

            Coords Victory = new Coords(rand.Next(rows), columns);
            layout[Victory] = Objects.Win;

            int rowsRand = rand.Next(rows);
            int columnsRand = rand.Next(columns);
            for (int i = 0; i < columns; i++)
            {
                for ( int j = 0; j < rows; j++)
                {
                    Coords none = new Coords(i, j);
                    layout[none] = Objects.None;
                }
            }
            

            
        }

        public void refreshMap(int rows, int columns, Player player)
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