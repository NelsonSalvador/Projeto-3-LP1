using System;

namespace Game
{
    public class Map
    {
        public int[,] Iswall {get; set;}

        public Map(int rows, int columns, Player player)
        {
            Random rand = new Random();
            Generate(rows, columns, player, rand);
            refreshMap(rows, columns, player);
            
        }
        public void Generate(int rows, int columns, Player player, Random rand)
        {
            Coords victory = new Coords(columns, rand.Next(rows));
            Iswall = new int[rows, columns];
            int numberofwalls = (Math.Min(rows, columns))-1;
            while(numberofwalls != 0)
            {
                int rowsRand = rand.Next(rows);
                int columnsRand = rand.Next(columns);
                if (Iswall[rowsRand,columnsRand] != 1)
                {
                    if (columnsRand == 0 && rowsRand == player.Coords.Y)
                    {
                    }
                    else
                    {
                        Iswall[rowsRand,columnsRand] = 1;
                        numberofwalls--;
                    }
                }
            }
        }

        public void refreshMap(int rows, int columns, Player player)
        {
            int j = 0;
            int i = 0;
            while(i != rows)
            {
                j = 0;
                while (j != columns){
            
                    if (j == player.Coords.X && i == player.Coords.Y)
                    {
                        Console.Write("P");
                        j++;
                    }
                    else if(Iswall[i,j] == 1){
                        Console.Write("#");
                        j++;
                    }
                    else
                    {    
                        Console.Write(".");
                        j++;
                    }
                }
                Console.WriteLine("");
                i++;
            }
        }
    }
}