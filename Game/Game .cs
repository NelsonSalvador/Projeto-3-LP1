using System;

namespace Game
{
    /// <summary>
    /// Creates a new game
    /// </summary>  
    public class Game
    {
        public Player Player {get; set;}
        public Map Map {get; set;}
        public HighScore highScore;
        public Menu menu;
        public bool Win {get; set;}
        public int Level{get; set;}

        /// <summary>
        /// Class constructor instantiates properties
        /// </summary>
        /// <param name="rows">amount of rows selected by the player in
        /// the beggining of the program</param>
        /// <param name="columns">amount of rows selected by the player in 
        /// the beggining of the program</param>
        public Game(int rows, int columns)
        {
            Level = 1;
            Player = new Player(rows, columns);
            Map = new Map(rows, columns, Player, Level);
            Win = false;
            StartGame(rows, columns, Player);       
        }
        /// <summary>
        /// Begins the main loop of the game
        /// </summary>
        /// <param name="rows">amount of rows selected by the player in
        /// the beggining of the program</param>
        /// <param name="columns">amount of rows selected by the player in
        /// the beggining of the program</param>
        /// <param name="player"> Instance of the player with his position
        /// </param>
        public void StartGame(int rows, int columns, Player player)
        {
            UpdateUI(rows,columns);
            while (!Player.IsDead())
            {
                //player move
                GetInput(rows,columns);

                // Check if player won after input
                if (Win)
                {
                    Level++;
                    Map = new Map(rows, columns, Player, Level);
                    Win = false;
                }
                if (Player.IsDead())
                    break;
                //refresh
                UpdateUI(rows,columns);
                //player move
                GetInput(rows,columns);
                
                // Check if player won after input
                if (Win)
                {
                    Level++;
                    Map = new Map(rows, columns, Player, Level);
                    Win = false;
                }
                //refresh
                UpdateUI(rows,columns);

                foreach(Enemy enemy in Map.EnemyList)
                {
                    EnemySelectMove(enemy);
                }
                UpdateUI(rows,columns);
                //newturn
            }
            if (Player.IsDead())
            {
                highScore = new HighScore(rows, columns, Level, 0);
                menu = new Menu(rows, columns);
            }
        }
        /// <summary>
        /// Handles receiving input and moving player accordingly
        /// </summary>
        /// <param name="rows">amount of rows selected by the player in
        /// the beggining of the program</param>
        /// <param name="columns">amount of columns selected by the player in
        /// the beggining of the program</param>
        public void GetInput(int rows,int columns)
        {
            Objects none = Objects.None; 
            string Input = "";
            ConsoleKeyInfo input;
            input = Console.ReadKey();
            
            if (input.Key == ConsoleKey.Escape)
            {
                menu = new Menu(rows, columns);
            }
            else if (input.Key == ConsoleKey.UpArrow ||
            input.Key == ConsoleKey.W)
                Input = "w";
            else if (input.Key == ConsoleKey.DownArrow ||
            input.Key == ConsoleKey.S)
                Input = "s";
            else if (input.Key == ConsoleKey.RightArrow ||
            input.Key == ConsoleKey.D)
                Input = "d";
            else if (input.Key == ConsoleKey.LeftArrow ||
            input.Key == ConsoleKey.A)
                Input = "a";

            switch (Input)
            {
                case "w" :
                    if (CheckMove(new Coords(Player.Coords.X-1,
                    Player.Coords.Y)))
                    {
                        none = Map.layout[new Coords(Player.Coords.X-1,
                        Player.Coords.Y)];
                        Map.layout[Player.Coords] = Objects.None;
                        Player.Coords = new Coords(Player.Coords.X-1,
                        Player.Coords.Y);
                    }
                    break;

                case "a":
                    if (CheckMove(new Coords(Player.Coords.X,
                    Player.Coords.Y-1)))
                    {
                        none = Map.layout[new Coords(Player.Coords.X,
                        Player.Coords.Y-1)];
                        Map.layout[Player.Coords] = Objects.None;
                        Player.Coords = new Coords(Player.Coords.X,
                        Player.Coords.Y-1);
                    }
                    break;

                case "s" :
                    if (CheckMove(new Coords(Player.Coords.X+1,
                    Player.Coords.Y)))
                    {
                        none = Map.layout[new Coords(Player.Coords.X+1,
                        Player.Coords.Y)];
                        Map.layout[Player.Coords] = Objects.None;
                        Player.Coords = new Coords(Player.Coords.X+1,
                        Player.Coords.Y);
                    }
                    break;

                case "d" :
                    if (CheckMove(new Coords(Player.Coords.X,
                    Player.Coords.Y+1)))
                    {
                        none = Map.layout[new Coords(Player.Coords.X,
                        Player.Coords.Y+1)];
                        Map.layout[Player.Coords] = Objects.None;
                        Player.Coords = new Coords(Player.Coords.X,
                        Player.Coords.Y+1);
                    }
                    break;
            }
            if (none == Objects.Victory)
            {
                Win = true;
            }
            Map.layout[Player.Coords] = Objects.Player;
            Player.HP-=1;
        }
        /// <summary>
        /// Verifies is the player can move to the requested position
        /// </summary>
        /// <param name="coords"> coordinates to which the object is going
        /// to move to, to compare with whatever is on said coordinates</param>
        /// <returns></returns>
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="enemy"></param>
        public void EnemySelectMove(Enemy enemy)
        {
            int moveNotAvaiable = 0;
            Coords enemyCoords = enemy.Coords;
            Coords newCoords;

            //Distance the enemy is from player on current position
            int distance = Math.Abs(Player.Coords.X - enemyCoords.X) +
            Math.Abs(Player.Coords.Y - enemyCoords.Y);
            
            //Distance the enemy is from player if he moves right
            int distanceRight = Math.Abs(Player.Coords.X - enemyCoords.X) +
            Math.Abs(Player.Coords.Y - enemyCoords.Y-1);
            //Distance the enemy is from player if he moves left
            int distanceLeft = Math.Abs(Player.Coords.X - enemyCoords.X) +
            Math.Abs(Player.Coords.Y - enemyCoords.Y+1);
            //Distance the enemy is from player if he moves up
            int distanceUp =  Math.Abs(Player.Coords.X - enemyCoords.X+1) +
            Math.Abs(Player.Coords.Y - enemyCoords.Y);
            //Distance the enemy is from player if he moves down
            int distanceDown = Math.Abs(Player.Coords.X - enemyCoords.X-1) +
            Math.Abs(Player.Coords.Y - enemyCoords.Y);;
            
            if(distanceUp < distance)
            {
                newCoords = new Coords (enemyCoords.X-1, enemyCoords.Y);
                if(EnemyCheckMove(enemy, newCoords, enemyCoords))
                {
                    moveNotAvaiable = 0;
                    return ;
                    
                }
                else
                {
                    moveNotAvaiable++;
                }
            }
            
            if( distanceLeft < distance)
            {
                newCoords = new Coords (enemyCoords.X, enemyCoords.Y-1);
                if(EnemyCheckMove(enemy, newCoords, enemyCoords))
                {
                    moveNotAvaiable = 0;
                    return;
                }
                else
                {
                    moveNotAvaiable++;
                }
            }
            
            if( distanceDown < distance)
            {
                newCoords = new Coords (enemyCoords.X+1, enemyCoords.Y);
                if(EnemyCheckMove(enemy, newCoords, enemyCoords))
                {
                    moveNotAvaiable = 0;
                    return ;
                }
                else
                {

                    moveNotAvaiable++;

                }
            }

            if( distanceRight < distance)
            {
                newCoords = new Coords (enemyCoords.X, enemyCoords.Y+1);
                if(EnemyCheckMove(enemy, newCoords, enemyCoords))
                {
                    moveNotAvaiable = 0;
                    return;
                }
                else
                {
                    moveNotAvaiable++;
                }
            }
            
            if(moveNotAvaiable>0)
            {
                int[] validRandom;
                int numberOfValidRandom = 0;
                validRandom = EnemyRandomMove(enemy, enemy.Coords);

                for(int i = 0; i < 4 ;i++)
                {
                    if (validRandom[i] == 1){
                        numberOfValidRandom++;
                    }
                }

                if (numberOfValidRandom > 0){
                    bool isValidMove = false;

                    while(!isValidMove)
                    {

                        int index = Map.Random.Next(4);
                        if (validRandom[index] == 1)
                        {
                            if(index == 0)
                            {
                                newCoords = new Coords (enemyCoords.X-1,
                                enemyCoords.Y);
                                isValidMove = EnemyCheckMove(enemy, newCoords,
                                enemyCoords);   

                            }
                            else if(index == 1)
                            {
                                newCoords = new Coords (enemyCoords.X+1,
                                enemyCoords.Y);
                                isValidMove = EnemyCheckMove(enemy, newCoords,
                                enemyCoords);  

                            }
                            else if(index == 2)
                            {
                                newCoords = new Coords (enemyCoords.X,
                                enemyCoords.Y-1);
                                isValidMove = EnemyCheckMove(enemy, newCoords,
                                enemyCoords);  

                            }
                            else if(index == 3)
                            {
                                newCoords = new Coords (enemyCoords.X,
                                enemyCoords.Y+1);
                                isValidMove = EnemyCheckMove(enemy, newCoords,
                                enemyCoords);  
                            }
                            
                        }

                    }
                }
            }
            return;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="enemy"></param>
        /// <param name="newCoords"></param>
        /// <param name="originalCoords"></param>
        /// <returns></returns>
        public bool EnemyCheckMove(Enemy enemy, Coords newCoords,
         Coords originalCoords)
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
            else if (Map.layout[newCoords] == Objects.Player)
            {
                if (Map.layout[originalCoords] == Objects.Boss)
                {
                    Player.HP =Player.HP - 10;
                }
                else
                {
                    Player.HP = Player.HP - 5;
                }
                return true;
            }
            else if (Map.layout[newCoords] == Objects.Wall)
            {
                return false;
            }
            return false;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="enemy"></param>
        /// <param name="originalCoords"></param>
        /// <returns></returns>
        public int[] EnemyRandomMove(Enemy enemy, Coords originalCoords)
        {
            int[] validArray = {0, 0, 0, 0};
            Coords movingUP = new Coords(originalCoords.X-1, 
            originalCoords.Y);
            Coords movingDown = new Coords(originalCoords.X+1,
             originalCoords.Y);
            Coords movingLeft = new Coords(originalCoords.X, 
            originalCoords.Y-1);
            Coords movingRight = new Coords(originalCoords.X, 
            originalCoords.Y+1);

            if (Map.layout.ContainsKey(movingUP) &&
             Map.layout[movingUP] == Objects.None)
            {
                validArray[0] = 1; 
            }

            if (Map.layout.ContainsKey(movingDown) &&
             Map.layout[movingDown] == Objects.None)
            {
                validArray[1] = 1; 
            }

            if (Map.layout.ContainsKey(movingLeft) &&
             Map.layout[movingLeft] == Objects.None)
            {
                validArray[2] = 1; 
            }

            if (Map.layout.ContainsKey(movingRight) &&
             Map.layout[movingRight] == Objects.None)
            {
                validArray[3] = 1; 
            }

            return validArray;
        }
        /// <summary>
        /// Updates the entire User interface
        /// </summary>
        /// <param name="rows"></param>
        /// <param name="columns"></param>
        public void UpdateUI(int rows, int columns)
        {
            Console.Clear();
            Console.WriteLine("Rogue-Like");
            Console.Write("☻ = Player | █ = Obstacle | ☼ = Minion | ☺ = Boss | ֍ = Victory\n"); 
            Console.Write("♠ = Small PowerUp | ♣ = Medium PowerUp | ♥ = Large PowerUp\n");
            Map.RefreshMap(rows,columns);
            Console.WriteLine("Level: " + Level + " " + "PlayerHP: " + Player.HP);
            Console.WriteLine("W to move Up | A to move left | S to move down | D to move right\n");
        }
    }
}