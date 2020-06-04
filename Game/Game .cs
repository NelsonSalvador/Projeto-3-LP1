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
            UpdateUI(rows,columns);
            while (!Player.IsDead() && !Win)
            {
                //player move
                GetInput();
                if (Win)
                    break;
                if (Player.IsDead())
                    break;
                //refresh
                UpdateUI(rows,columns);
                //player move
                GetInput();
                //refresh
                UpdateUI(rows,columns);

                foreach(Enemy enemy in Map.EnemyList)
                {
                    bool enemyMove = EnemySelectMove(enemy);
                }
                UpdateUI(rows,columns);
                //newturn
            }
            if (Win)
            {
                Level++;
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
                    if (CheckMove(new Coords(Player.Coords.X-1, Player.Coords.Y)))
                    {
                        none = Map.layout[new Coords(Player.Coords.X-1, Player.Coords.Y)];
                        Map.layout[Player.Coords] = Objects.None;
                        Player.Coords = new Coords(Player.Coords.X-1, Player.Coords.Y);
                        Map.layout[Player.Coords] = Objects.Player;
                    }
                    break;

                case "a" :
                    if (CheckMove(new Coords(Player.Coords.X, Player.Coords.Y-1)))
                    {
                        none = Map.layout[new Coords(Player.Coords.X, Player.Coords.Y-1)];
                        Map.layout[Player.Coords] = Objects.None;
                        Player.Coords = new Coords(Player.Coords.X, Player.Coords.Y-1);
                        Map.layout[Player.Coords] = Objects.Player;
                    }
                    break;

                case "s" :
                    if (CheckMove(new Coords(Player.Coords.X+1, Player.Coords.Y)))
                    {
                        none = Map.layout[new Coords(Player.Coords.X+1, Player.Coords.Y)];
                        Map.layout[Player.Coords] = Objects.None;
                        Player.Coords = new Coords(Player.Coords.X+1, Player.Coords.Y);
                        Map.layout[Player.Coords] = Objects.Player;
                    }
                    break;

                case "d" :
                    if (CheckMove(new Coords(Player.Coords.X, Player.Coords.Y+1)))
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
            Player.HP-=1;
        }
        public bool CheckMove(Coords coords)
        {
            if (!Map.layout.ContainsKey(coords))
            {
                return false;
            }
            if (Map.layout[coords] == Objects.None)
            {
                return true;
            }
            if (Map.layout[coords] == Objects.SmallPowerups)
            {
                Player.HP += 4;
                return true;
            }
            if (Map.layout[coords] == Objects.MediumPowerups)
            {
                Player.HP += 8;
                return true;
            }
            if (Map.layout[coords] == Objects.LargePowerups)
            {
                Player.HP += 16;
                return true;
            }
            if (Map.layout[coords] == Objects.Victory)
            {
                return true;
            }
            return false;
        }

        public bool EnemySelectMove(Enemy enemy)
        {
            int moveNotAvaiable = 0;
            Coords enemyCoords = enemy.Coords;
            Coords newCoords;

            int distance = Math.Abs(Player.Coords.X - enemyCoords.X) + Math.Abs(Player.Coords.Y - enemyCoords.Y);
            
            int distanceRight = Math.Abs(Player.Coords.X - enemyCoords.X) + Math.Abs(Player.Coords.Y - enemyCoords.Y-1);
            int distanceLeft = Math.Abs(Player.Coords.X - enemyCoords.X) + Math.Abs(Player.Coords.Y - enemyCoords.Y+1);
            int distanceUp =  Math.Abs(Player.Coords.X - enemyCoords.X+1) + Math.Abs(Player.Coords.Y - enemyCoords.Y);
            int distanceDown = Math.Abs(Player.Coords.X - enemyCoords.X-1) + Math.Abs(Player.Coords.Y - enemyCoords.Y);;
            
            if(distanceUp < distance)
            {
                newCoords = new Coords (enemyCoords.X-1, enemyCoords.Y);
                if(EnemyCheckMove(enemy, newCoords, enemyCoords))
                {
                    moveNotAvaiable = 0;
                    return true;
                    
                }
                else
                {
                    moveNotAvaiable = 1;
                }
            }
            else if( distanceLeft < distance)
            {
                newCoords = new Coords (enemyCoords.X, enemyCoords.Y-1);
                if(EnemyCheckMove(enemy, newCoords, enemyCoords))
                {
                    moveNotAvaiable = 0;
                    return true;
                }
                else
                {
                    moveNotAvaiable = 2;
                }
            }
            else if( distanceDown < distance)
            {
                newCoords = new Coords (enemyCoords.X+1, enemyCoords.Y);
                if(EnemyCheckMove(enemy, newCoords, enemyCoords))
                {
                    moveNotAvaiable = 0;
                    return true;
                }
                else
                {
                    moveNotAvaiable = 0;
                    moveNotAvaiable = 3;
                }
            }
            else if( distanceRight < distance)
            {
                newCoords = new Coords (enemyCoords.X, enemyCoords.Y+1);
                if(EnemyCheckMove(enemy, newCoords, enemyCoords))
                {
                    moveNotAvaiable = 0;
                    return true;
                }
                else
                {
                    moveNotAvaiable = 4;
                }
            }
            
            if(moveNotAvaiable>0)
            {
                Coords coordsRandom;
                bool validRandomMove = false;
                int randomMove;
                int noMove = 0;
                while(!validRandomMove)
                {
                    randomMove = Map.Random.Next(1, 4);
                    switch(moveNotAvaiable)
                    {
                        case 1:
                            if (randomMove == 1)
                            {
                                coordsRandom = new Coords (enemyCoords.X, enemyCoords.Y-1);
                                validRandomMove = EnemyCheckMove(enemy, coordsRandom, enemyCoords);
                                noMove =+ 1;
                            }
                            else if(randomMove == 2)
                            {
                                coordsRandom = new Coords (enemyCoords.X+1, enemyCoords.Y);
                                validRandomMove = EnemyCheckMove(enemy, coordsRandom, enemyCoords);
                                noMove =+ 1;
                            }
                            else if(randomMove == 3)
                            {
                                coordsRandom = new Coords (enemyCoords.X, enemyCoords.Y+1);
                                validRandomMove = EnemyCheckMove(enemy, coordsRandom, enemyCoords);
                                noMove =+ 1;
                            }
                            break;
                        case 2:
                            if (randomMove == 1)
                            {
                                coordsRandom = new Coords (enemyCoords.X-1, enemyCoords.Y);
                                validRandomMove = validRandomMove = EnemyCheckMove(enemy, coordsRandom, enemyCoords);
                                noMove =+ 1;
                            }
                            else if(randomMove == 2)
                            {
                                coordsRandom = new Coords (enemyCoords.X+1, enemyCoords.Y);
                                validRandomMove = EnemyCheckMove(enemy, coordsRandom, enemyCoords);
                                noMove =+ 1;
                            }
                            else if(randomMove == 3)
                            {
                                coordsRandom = new Coords (enemyCoords.X, enemyCoords.Y+1);
                                validRandomMove = EnemyCheckMove(enemy, coordsRandom, enemyCoords);
                                noMove =+ 1;
                            }
                            break;
                        case 3:
                            if (randomMove == 1)
                            {
                                coordsRandom = new Coords (enemyCoords.X-1, enemyCoords.Y);
                                validRandomMove = EnemyCheckMove(enemy, coordsRandom, enemyCoords);
                                noMove =+ 1;
                            }
                            else if(randomMove == 2)
                            {
                                coordsRandom = new Coords (enemyCoords.X, enemyCoords.Y-1);
                                validRandomMove = EnemyCheckMove(enemy, coordsRandom, enemyCoords);
                                noMove =+ 1;
                            }
                            else if(randomMove == 3)
                            {
                                coordsRandom = new Coords (enemyCoords.X, enemyCoords.Y+1);
                                validRandomMove = EnemyCheckMove(enemy, coordsRandom, enemyCoords);
                                noMove =+ 1;
                            }
                            break;
                        case 4:
                            if (randomMove == 1)
                            {
                                coordsRandom = new Coords (enemyCoords.X-1, enemyCoords.Y);
                                validRandomMove = EnemyCheckMove(enemy, coordsRandom, enemyCoords);
                                noMove =+ 1;
                            }
                            else if(randomMove == 2)
                            {
                                coordsRandom = new Coords (enemyCoords.X, enemyCoords.Y-1);
                                validRandomMove = EnemyCheckMove(enemy, coordsRandom, enemyCoords);
                                noMove =+ 1;
                            }
                            else if(randomMove == 3)
                            {
                                coordsRandom = new Coords (enemyCoords.X+1, enemyCoords.Y);
                                validRandomMove = EnemyCheckMove(enemy, coordsRandom, enemyCoords);
                                noMove =+ 1;
                            }
                            break;
                    }
                    Console.WriteLine(noMove);
                    if (noMove == 3)
                    {
                        return true;
                    }
                }
                return true;
            }

            return false;
            
        }

        public bool EnemyCheckMove(Enemy enemy, Coords newCoords, Coords originalCoords)
        {
            
            if (!Map.layout.ContainsKey(newCoords))
            {
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
        public void UpdateUI(int rows, int columns)
        {
            //Console.Clear();
            Console.WriteLine("Rogue-Like");
            Console.Write("P = Player | # = Obstacle | M = Minion | B = Boss | V = Victory\n"); 
            Console.Write("s = Small PowerUp | m = Medium PowerUp | l = Large PowerUp\n");
            Map.RefreshMap(rows,columns);
            Console.WriteLine("Level: " + Level + " " + "PlayerHP: " + Player.HP);
            Console.WriteLine("W to move Up | A to move left | S to move down | D to move right\n");
        }
    }
}