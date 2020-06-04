using System;
using System.Collections.Generic;

namespace Game
{
    public class Map
    {
        public Dictionary<Coords , Objects> layout {get; set;}
        public Random Random {get; set;}
        public Player Player {get; set;}
        public List<Enemy> EnemyList {get; set;}
        public Map(int rows, int columns, Player player, int level)
        {
            Player = player;
            Random = new Random();
            layout = new Dictionary<Coords, Objects>();
            EnemyList = new List<Enemy>();

            Generate(rows, columns, player, level);
        }
        public void Generate(int rows, int columns, Player player, int level)
        {
            //Fill the map with blank available 
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
                Coords wall = new Coords(Random.Next(rows), Random.Next(columns));
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
            
            while (numberOfEnemies != 0)
            {
                int percentage = Random.Next(101);
                Coords enemies = new Coords(Random.Next(rows), Random.Next(columns));
                Enemy enemy;
                if (layout[enemies] == Objects.None)
                {
                    if(percentage >= 100 -(5*level))
                    {
                        enemy = new Enemy(enemies, Objects.Boss);
                        layout[enemies] = Objects.Boss;
                    }
                    else
                    {
                        enemy = new Enemy(enemies, Objects.Minion);
                        layout[enemies] = Objects.Minion;
                    }
                    numberOfEnemies--;
                    EnemyList.Add(enemy);
                }
            }

            //Defines number of Max PowerUps
            int NumberOfPowerUps = -level + 11;
            if (NumberOfPowerUps <= 0)
            {
                NumberOfPowerUps = 1;
            }
            while (NumberOfPowerUps != 0)
            {
                int percentage = Random.Next(101);
                Coords Powerups = new Coords(Random.Next(rows), Random.Next(columns));
                PowerUp powerUp;
                if (layout[Powerups] == Objects.None)
                {
                    if (percentage <= 50)
                    {
                        powerUp = new PowerUp(Powerups);
                        layout[Powerups] = Objects.SmallPowerups;
                    }
                    else if (percentage > 50 && percentage <= 85)
                    {
                        powerUp = new PowerUp(Powerups);
                        layout[Powerups] = Objects.MediumPowerups;
                    }
                    else
                    {
                        powerUp = new PowerUp(Powerups);
                        layout[Powerups] = Objects.LargePowerups;
                    }
                    NumberOfPowerUps--;
                }
            }

            //...
        }

        public void RefreshMap(int rows, int columns)
        {
            foreach(KeyValuePair<Coords, Objects> tile in layout)
            {
                switch(tile.Value)
                {
                    case Objects.None:
                        Console.Write(". ");
                        break;
                    case Objects.Player:
                        Console.Write("P ");
                        break;
                    case Objects.Wall:
                        Console.Write("# ");
                        break;
                    case Objects.Victory:
                        Console.Write("V ");
                        break;
                    case Objects.Minion:
                        Console.Write("M ");
                        break;
                    case Objects.Boss:
                        Console.Write("B ");
                        break;
                    case Objects.SmallPowerups:
                        Console.Write("s ");
                        break;
                    case Objects.MediumPowerups:
                        Console.Write("m ");
                        break;
                    case Objects.LargePowerups:
                        Console.Write("l ");
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