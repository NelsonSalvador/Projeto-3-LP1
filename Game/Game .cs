using System;

namespace Game
{
    public class Game
    {
        public Player Player {get; set;}
        public Map Map {get; set;}
        public bool Win {get; set;}
        public int Level{get; set;}

        public Game(int rows, int columns)
        {
            Level = 1;
            Player = new Player(rows, columns);
            Map = new Map(rows, columns, Player, Level);
            Win = false;
            StartGame(rows, columns, Player);       
        }
        public void StartGame(int rows, int columns, Player player)
        {
            while (!Player.IsDead() && !Win)
            {
                //player move
                GetInput();
                if (Win)
                    break;
                //refresh
                Map.refreshMap(rows, columns);
                //player move
                GetInput();
                //refresh
                Map.refreshMap(rows, columns);

                bool enemyMove = enemySelectMove();

                Map.refreshMap(rows, columns);
                
                //newturn
            }
            if (Win)
            {
                Level++;
                Player = new Player(rows, columns);
                Map = new Map(rows, columns, Player, Level);
                Win = false;
                StartGame(rows, columns, Player);
            }
        }
        public void GetInput()
        {
            Objects none = Objects.None; 
            string input = Console.ReadLine();
            switch (input)
            {
                case "w" :
                    if (checkMove(new Coords(Player.Coords.X-1, Player.Coords.Y)))
                    {
                        none = Map.layout[new Coords(Player.Coords.X-1, Player.Coords.Y)];
                        Map.layout[Player.Coords] = Objects.None;
                        Player.Coords = new Coords(Player.Coords.X-1, Player.Coords.Y);
                        Map.layout[Player.Coords] = Objects.Player;
                    }
                    break;

                case "a" :
                    if (checkMove(new Coords(Player.Coords.X, Player.Coords.Y-1)))
                    {
                        none = Map.layout[new Coords(Player.Coords.X, Player.Coords.Y-1)];
                        Map.layout[Player.Coords] = Objects.None;
                        Player.Coords = new Coords(Player.Coords.X, Player.Coords.Y-1);
                        Map.layout[Player.Coords] = Objects.Player;
                    }
                    break;

                case "s" :
                    if (checkMove(new Coords(Player.Coords.X+1, Player.Coords.Y)))
                    {
                        none = Map.layout[new Coords(Player.Coords.X+1, Player.Coords.Y)];
                        Map.layout[Player.Coords] = Objects.None;
                        Player.Coords = new Coords(Player.Coords.X+1, Player.Coords.Y);
                        Map.layout[Player.Coords] = Objects.Player;
                    }
                    break;

                case "d" :
                    if (checkMove(new Coords(Player.Coords.X, Player.Coords.Y+1)))
                    {
                        none = Map.layout[new Coords(Player.Coords.X, Player.Coords.Y+1)];
                        Map.layout[Player.Coords] = Objects.None;
                        Player.Coords = new Coords(Player.Coords.X, Player.Coords.Y+1);
                        Map.layout[Player.Coords] = Objects.Player;
                    }
                    break;
            }
            if (none == Objects.Victory)
            {
                Win = true;
            }
        }
        public bool checkMove(Coords coords)
        {
            if (!Map.layout.ContainsKey(coords))
            {
                return false;
            }
            if (Map.layout[coords] == Objects.None)
            {
                return true;
            }
            if (Map.layout[coords] == Objects.Victory)
            {
                return true;
            }
            return false;
        }

        public bool enemySelectMove()
        {
            foreach(Enemy enemy in Map.EnemyList)
            {
                Coords enemyCoords = enemy.Coords;
                Console.WriteLine(enemyCoords);

                int distance = Math.Abs(Player.Coords.X - enemyCoords.X) + Math.Abs(Player.Coords.Y - enemyCoords.Y);
                
                int distanceRight = Math.Abs(Player.Coords.X - enemyCoords.X) + Math.Abs(Player.Coords.Y - enemyCoords.Y-1);
                int distanceLeft = Math.Abs(Player.Coords.X - enemyCoords.X) + Math.Abs(Player.Coords.Y - enemyCoords.Y+1);
                int distanceUp =  Math.Abs(Player.Coords.X - enemyCoords.X+1) + Math.Abs(Player.Coords.Y - enemyCoords.Y);
                int distanceDown = Math.Abs(Player.Coords.X - enemyCoords.X-1) + Math.Abs(Player.Coords.Y - enemyCoords.Y);;
                
                if(distanceUp < distance)
                {
                    Coords newCoords = new Coords (enemyCoords.X-1, enemyCoords.Y);
                    if(enemyCheckmove(enemy, newCoords, enemyCoords))
                    {
                        return true;
                    }
                }

                if( distanceLeft < distance)
                {
                    Coords newCoords = new Coords (enemyCoords.X, enemyCoords.Y-1);
                    if(enemyCheckmove(enemy, newCoords, enemyCoords))
                    {
                        return true;
                    }
                }
                
                if( distanceDown < distance)
                {
                    Coords newCoords = new Coords (enemyCoords.X+1, enemyCoords.Y);
                    if(enemyCheckmove(enemy, newCoords, enemyCoords))
                    {
                        return true;
                    }
                }
                
                if( distanceRight < distance)
                {
                    Coords newCoords = new Coords (enemyCoords.X, enemyCoords.Y+1);
                    if(enemyCheckmove(enemy, newCoords, enemyCoords))
                    {
                        return true;
                    }
                }
            }
            return false;
            
        }

        public bool enemyCheckmove(Enemy enemy, Coords newCoords, Coords originalCoords)
        {
            
            if (!Map.layout.ContainsKey(newCoords))
            {
                Console.WriteLine("Fds");
                return false;
            }
            else if (Map.layout[newCoords] == Objects.None)
            {
                enemy.move(newCoords);
                if (Map.layout[originalCoords] == Objects.Boss)
                {
                    Map.layout[newCoords] = Objects.Boss;
                }
                else
                {
                    Map.layout[newCoords] = Objects.Minion;
                }
                Map.layout[originalCoords] = Objects.None; 
                return true;            
            }
            else if (Map.layout[newCoords] == Objects.Wall)
            {
                return false;
            }
            return false;
        }
    }
}