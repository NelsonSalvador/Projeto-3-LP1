using System;

namespace Game
{
    public class MapGeneration
    {
        
        public void Generate(int rows, int columns, Player player)
        {
            var rand = new Random();
            int i = 0;
            int j = 0;

            int[,] iswall = new int[rows, columns];
            int numberofwalls = (Math.Min(rows, columns))-1;
            player.HP = (rows * columns) / 4;
            player.posY = rand.Next(rows); 
            
            while(numberofwalls != 0)
            {
                int rowsRand = rand.Next(rows);
                int columnsRand = rand.Next(columns);
                if (iswall[rowsRand,columnsRand] != 1)
                {
                    if (columnsRand == 0 && rowsRand == player.posY)
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
            
                    if (j == player.posX && i == player.posY)
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