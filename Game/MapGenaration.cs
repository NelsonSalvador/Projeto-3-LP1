using System;

namespace Game
{
    public class MapGeneration
    {
        public void Generate(int rows, int colunes)
        {
            int i = 0;
            int j = 0;
            while(i != rows)
            {
                j = 0;
                while (j != colunes){
                    Console.Write("#");
                    j++;
                }
                Console.WriteLine("");
                i++;
            }
        }
        
        
        
    }
}