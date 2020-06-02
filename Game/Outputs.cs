using System;

namespace Game
{
    class Outputs
    {
        public static void pritMap (string[,] map, int rows, int colums)
        {
            for(int x = 0; x < rows; x++)
            {
                for(int y = 0; y < colums; y++)
                {
                    Console.Write(map[x,y]);
                }
                Console.WriteLine("");
            }
        }

    }
}