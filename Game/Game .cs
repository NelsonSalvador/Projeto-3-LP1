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
                enemySelect();

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

        public void enemySelect()
        {
            foreach(Enemy enemy in Map.EnemyList)
            {
                Coords coords = new Coords(enemy.Coords.X, enemy.Coords.Y);
                Coords newCoords;
                int taxicabDistance = (Player.Coords.X - coords.Y)+(coords.X - Player.Coords.Y);

                if (taxicabDistance < (Player.Coords.X - coords.Y)+(coords.X-1 - Player.Coords.Y))
                {
                    newCoords = new Coords(enemy.Coords.X-1, enemy.Coords.Y);
                    enemyCheckmove(enemy, newCoords, coords);
                }
                else if (taxicabDistance < (Player.Coords.X - coords.Y)+(coords.X+1 - Player.Coords.Y))
                {
                    newCoords = new Coords(enemy.Coords.X+1, enemy.Coords.Y);
                    enemyCheckmove(enemy, newCoords, coords);
                }
                else if (taxicabDistance < (Player.Coords.X - coords.Y-1)+(coords.X - Player.Coords.Y))
                {
                    newCoords = new Coords(enemy.Coords.X, enemy.Coords.Y-1);
                    enemyCheckmove(enemy, newCoords, coords);

                }
                else if (taxicabDistance < (Player.Coords.X - coords.Y-1)+(coords.X - Player.Coords.Y))
                {
                    newCoords = new Coords(enemy.Coords.X, enemy.Coords.Y+1);
                    enemyCheckmove(enemy, newCoords, coords);
                }
                
            }
        }

        public void enemyCheckmove(Enemy enemy, Coords newCoords, Coords coords)
        {
            
            if (!Map.layout.ContainsKey(enemy.Coords))
            {

            }
            if (Map.layout[newCoords] == Objects.None)
            {
                enemy.move(newCoords);
                if (Map.layout[coords] == Objects.Boss)
                {
                    Map.layout[newCoords] = Objects.Boss;
                }
                else
                {
                    Map.layout[newCoords] = Objects.Minion;
                }
                Map.layout[coords] = Objects.None; 
                                    
            }
            Console.WriteLine(coords);
        }
    }
}