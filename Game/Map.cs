using System;
using System.Collections.Generic;

namespace Game
{
    /// <summary>
    /// Handles map genaration and refresh
    /// </summary>
    public class Map
    {
        /// <summary>
        /// Property that contains all the objects for the map
        /// </summary>
        /// <value></value>
        public Dictionary<Coords , Objects> layout {get; set;} 

        /// <summary>
        /// Property that contains a Random instance to use in map genaration
        /// </summary>
        /// <value></value>
        public Random Random {get; set;}
        
        /// <summary>
        /// Property that contains instance of player
        /// </summary>
        /// <value></value>
        public Player Player {get; set;}

        /// <summary>
        /// Property that contains a enemy list to hold all enemies
        /// </summary>
        /// <value></value>
        public List<Enemy> EnemyList {get; set;}

        /// <summary>
        /// Creates a new instance of map
        /// </summary>
        /// <param name="rows"></param>
        /// <param name="columns"></param>
        /// <param name="player"></param>
        /// <param name="level"></param>
        public Map(int rows, int columns, Player player, int level)
        {
            Player = player;
            Random = new Random();
            layout = new Dictionary<Coords, Objects>();
            EnemyList = new List<Enemy>();

            Generate(rows, columns, player, level);
        }
        /// <summary>
        /// Generates new map and instanciates objects
        /// </summary>
        /// <param name="rows"></param>
        /// <param name="columns"></param>
        /// <param name="player"></param>
        /// <param name="level"></param>
        public void Generate(int rows, int columns, Player player, int level)
        {
            //Fill the map with available tiles 
            for (int i = 0; i < rows; i++)
            {
                for ( int j = 0; j < columns; j++)
                {
                    layout.Add(new Coords( i, j), Objects.None);
                }
            }

            //Define Random spawn of the player
            Coords spawn = new Coords(Random.Next(rows), 0);
            layout[spawn] = Objects.Player;
            player.Coords = spawn;

            //Define Random spawn of the exit
            Coords Victory = new Coords(Random.Next(rows), columns-1);
            layout[Victory] = Objects.Victory;

            //Calculates de number os walls
            int numberofwalls = (Math.Min(rows, columns))-1;

            //Defines the placement of walls
            while (numberofwalls != 0)
            {
                Coords wall = new Coords(Random.Next(rows),
                Random.Next(columns));

                if (layout[wall] == Objects.None)
                {
                    layout[wall] = Objects.Wall;
                    numberofwalls--;
                }
            }

            // Defines number os max enemies 
            int maxNumberOfEnemies = 2*level+1;
            if(maxNumberOfEnemies > (rows*columns)/2)
            {
                maxNumberOfEnemies = (rows*columns)/2;
            }
            int numberOfEnemies = Random.Next(1, maxNumberOfEnemies);
            
            //Spawn enemies while the number of enemys to spawn
            // is not equal to 0
            while (numberOfEnemies != 0)
            {
                // chance of spawning a boss
                int percentage = Random.Next(101);

                // Random coords for the enemy
                Coords enemies = new Coords(Random.Next(rows),
                Random.Next(columns));

                //Instantiate a enemy
                Enemy enemy;

                //Checks if enemy can spawn in that position
                if (layout[enemies] == Objects.None)
                {
                    //Checks if the enemy to spawn is a boss
                    if(percentage >= 100 -(5*level))
                    {
                        //spawns a boss
                        enemy = new Enemy(enemies, Objects.Boss);
                        layout[enemies] = Objects.Boss;
                    }
                    else
                    {
                        //spawns a minion
                        enemy = new Enemy(enemies, Objects.Minion);
                        layout[enemies] = Objects.Minion;
                    }
                    //Number of enemies to spawn decreases
                    numberOfEnemies--;

                    //Add enemy to the list
                    EnemyList.Add(enemy);
                }
            }

            //Defines the max number of PowerUps
            int NumberOfPowerUps = -level + 11;
            //If NumberOfPowerUps is less or equal to 0~
            //force to spawn 1 power up
            if (NumberOfPowerUps <= 0)
            {
                NumberOfPowerUps = 1;
            }

            //Spawn power ups while the number of power ups 
            // is not equal to 0
            while (NumberOfPowerUps != 0)
            {
                //Change for the powerups
                int percentage = Random.Next(101);

                //Random coords for the powerups
                Coords Powerups = new Coords(Random.Next(rows),
                Random.Next(columns));

                //Instantiate a power up
                PowerUp powerUp;

                //Check if can spawn in that position
                if (layout[Powerups] == Objects.None)
                {   
                    //if percentage is less than 50 spawn
                    // a small power up
                    if (percentage <= 50)
                    {
                        powerUp = new PowerUp(Powerups);
                        layout[Powerups] = Objects.SmallPowerups;
                    }
                    //if percentage is in between 50 and 85 spawn
                    // a medium power up
                    else if (percentage > 50 && percentage <= 85)
                    {
                        powerUp = new PowerUp(Powerups);
                        layout[Powerups] = Objects.MediumPowerups;
                    }
                    //if percentage is above 85 spawn
                    // a medium power up
                    else
                    {
                        powerUp = new PowerUp(Powerups);
                        layout[Powerups] = Objects.LargePowerups;
                    }
                    //decrease the number of power ups to spawn
                    NumberOfPowerUps--;
                }
            }
        }
        /// <summary>
        /// Updates map on UI according with dictionary information
        /// </summary>
        /// <param name="rows"></param>
        /// <param name="columns"></param>
        public void RefreshMap(int rows, int columns)
        {
            
            // Goes through the dictionary
            foreach(KeyValuePair<Coords, Objects> tile in layout)
            {
                Console.BackgroundColor = ConsoleColor.Black;
                //Prints the the symbol according to the dictionary
                switch(tile.Value)
                {
                    case Objects.None:
                        Console.Write(". ");
                        break;
                    case Objects.Player:
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("☻ ");
                        Console.ResetColor();
                        break;
                    case Objects.Wall:
                        Console.Write("█ ");
                        break;
                    case Objects.Victory:
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("֍ ");
                        Console.ResetColor();
                        break;
                    case Objects.Minion:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("☼ ");
                        Console.ResetColor();
                        break;
                    case Objects.Boss:
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.Write("☺ ");
                        Console.ResetColor();
                        break;
                    case Objects.SmallPowerups:
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.Write("♠ ");
                        Console.ResetColor();
                        break;
                    case Objects.MediumPowerups:
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write("♣ ");
                        Console.ResetColor();
                        break;
                    case Objects.LargePowerups:
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("♥ ");
                        Console.ResetColor();
                        break;
                }
                

                //Switch line
                if (tile.Key.Y == (columns-1))
                {
                    Console.WriteLine("");
                }
            }
            Console.ResetColor();
        }
    }
}