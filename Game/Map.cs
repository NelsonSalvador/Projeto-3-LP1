using System;

namespace Game 
{
    public class Map
    {
        
        public Map(int rows, int columns, Player player)
        {
            //Random number generation
            Random rand = new Random();

            // Variable that guards the map
            string[,] map = new string[rows, columns];

            // Variable to run trough rows
            int i = 0;

            // Variable to run trough columns
            int j = 0;

            Coords victory = new Coords(columns, rand.Next(rows));

            //Variable to place walls 
            int[,] iswall = new int[rows, columns];

            //Variable to place enemys
            int[,] isenemy = new int[rows, columns];

            //Calculate number os walls to be placed
            int numberofwalls = (Math.Min(rows, columns))-1;

            //While the number of walls that need to be placed don't reach 0 run this while
            while(numberofwalls != 0)
            {
                //Get random row to place wall
                int rowsRand = rand.Next(rows);

                //Get random column to place wall
                int columnsRand = rand.Next(columns);

                //Check if that position already has a wall
                if (iswall[rowsRand,columnsRand] != 1)
                {
                    //Check if the player is in that position
                    if (columnsRand == 0 && rowsRand == player.Coords.Y)
                    {
                    }
                    else
                    {
                        //Place wall
                        iswall[rowsRand,columnsRand] = 1;

                        //Decrease the number of walls that need to be placed
                        numberofwalls--;
                    }
                }
            }

            //Run trough the number of rows
            while(i != rows)
            {
                // Reseting the variable to run trough the number of columns
                j = 0;
                //Run trough the number of columns
                while (j != columns){
                    
                    //If it's the position of the player add "P" to the map
                    if (j == player.Coords.X && i == player.Coords.Y)
                    {
                        map[i, j] = "P";
                        j++;
                    }
                    //If it's the position of a wall add "#" to the map
                    else if(iswall[i,j] == 1){
                        map[i, j] = "#";
                        j++;
                    }
                    //Else add "." to the map
                    else
                    {    
                        map[i, j] = ".";
                        j++;
                    }
                }
                i++;
            }

            return map; 
        }
    }
}