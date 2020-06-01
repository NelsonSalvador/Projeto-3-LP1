using System;

namespace Game
{
    public class MapGeneration
    {
        public void Generate(int rows, int columns, Player player)
        {
            int i = 0;
            int j = 0;
            player.HP = (rows * columns) / 4;
            while(i != rows)
            {
                j = 0;
                while (j != columns){
                    if (j == player.posX && i == player.posY) // Instead of rows/2 put:  if i == [randInt]
                    {
                        Console.Write("*");
                        j++;
                    }
                    else
                    {    
                    Console.Write("#");
                    j++;
                    }
                }
                Console.WriteLine("");
                i++;
            }
        }
    }
}