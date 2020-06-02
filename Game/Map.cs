using System;

namespace Game
{
    public class Map
    {
        public Map(int rows, int columns, Player player)
        {
            Random rand = new Random();
            Generate(rows, columns, player, rand);
        }
        public void Generate(int rows, int columns, Player player, Random rand)
        {
            int i = 0;
            int j = 0;

            int[,] iswall = new int[rows, columns];
            int numberofwalls = (Math.Min(rows, columns))-1;
            while(numberofwalls != 0)
            {
                int rowsRand = rand.Next(rows);
                int columnsRand = rand.Next(columns);
                if (iswall[rowsRand,columnsRand] != 1)
                {
                    if (columnsRand == 0 && rowsRand == player.PosY)
                    {
                    }
                    else
                    {
                        iswall[rowsRand,columnsRand] = 1;
                        numberofwalls--;
                    }
                }
            }
            while(i != rows)
            {
                j = 0;
                while (j != columns){
            
                    if (j == player.PosX && i == player.PosY)
                    {
                        Console.Write("P");
                        j++;
                    }
                    else if(iswall[i,j] == 1){
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