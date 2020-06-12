using System;

namespace Game
{
    /// <summary>
    /// Creates a new game
    /// </summary>  
    public class Game
    {
        /// <summary>
        /// Property that contains player instance
        /// </summary>
        /// <value></value>
        public Player Player {get; set;}

        /// <summary>
        /// Property that contains map instance
        /// </summary>
        /// <value></value>
        public Map Map {get; set;}

        /// <summary>
        /// Property that contains highscore instance
        /// </summary>
        public HighScore highScore;

        /// <summary>
        /// Property that contains menu instance
        /// </summary>
        public Menu menu;

        /// <summary>
        /// Property that contains victory status
        /// </summary>
        /// <value></value>
        public bool Win {get; set;}

        /// <summary>
        /// Property that contains level
        /// </summary>
        /// <value></value>
        public int Level{get; set;}
        
        /// <summary>
        /// Property that contains player instance turn message
        /// </summary>
        /// <value></value>
        public string feedback{get; set;}

        /// <summary>
        /// Class constructor instantiates properties
        /// </summary>
        /// <param name="rows">amount of rows selected by the player in
        /// the beggining of the program</param>
        /// <param name="columns">amount of columns selected by the player in 
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
        /// <param name="columns">amount of columns selected by the player in
        /// the beggining of the program</param>
        /// <param name="player"> Instance of the player with his position
        /// </param>
        public void StartGame(int rows, int columns, Player player)
        {            
            //Refresh the map
            UpdateUI(rows,columns);

            int moves = 0;
            //Game loop while player does not die
            while (!Player.IsDead())
            {
                //Number of moves player has made
                moves = 0;
                //While the player has not made 2 moves
                while(moves!= 2)
                {
                    //Move the player
                    GetInput(rows,columns);
                    //Increment the number of moves by 1
                    moves++;
                    //check if the player has passed the level
                    if (Win)
                    {
                        //Increment the level
                        Level++;
                        //Create new map
                        Map = new Map(rows, columns, Player, Level);
                        //Turns win to false for the next level
                        Win = false;
                        //reset number os moves made
                        moves = 0;
                    }
                    //Check if player died
                    if (Player.IsDead())
                    {
                        //break the cicles
                        break;
                    }
                    //refresh screen
                    UpdateUI(rows,columns);     
                }
                //pass through each enemy in Enemylist
                foreach(Enemy enemy in Map.EnemyList)
                {   
                    //Move enemy
                    EnemySelectMove(enemy);
                }

                //refresh map
                UpdateUI(rows,columns);
                //newturn
            }

            //If player dies
            if (Player.IsDead())
            {
                //save Highscore
                highScore = new HighScore(rows, columns, Level, 0);
                //Print new Menu
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
        /// <returns></returns>
        /// returns true if player made a valid move
        /// returns false if player made a invalid move
        public void GetInput(int rows,int columns)
        {
            //Instantiate a none object, aka available tile
            Objects none = Objects.None; 

            //Create string to hold the input
            string Input = "";

            bool isValidMove = false;
            while(!isValidMove)
            {
                //Gets input
                ConsoleKeyInfo input;
                input = Console.ReadKey();
                
                //Verifies if player want's to exit
                if (input.Key == ConsoleKey.Escape)
                {
                    //Print new menu
                    menu = new Menu(rows, columns);

                    isValidMove = true;
                }
                //Check if input is any of the directional keys
                //Assigns corresponding "WASD" key
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
                else
                {
                    Input = "h";   
                }
                //Switch for the movements
                switch (Input)
                {   
                    //If movement is up arrow key or "w"
                    case "w" :

                        //if checkmoves returns true, aka move is valid
                        if (CheckMove(new Coords(Player.Coords.X-1,
                        Player.Coords.Y)))
                        {
                            //Holds coords of player after movement
                            none = Map.layout[new Coords(Player.Coords.X-1,
                            Player.Coords.Y)];

                            //Turns player tile into a available tile
                            Map.layout[Player.Coords] = Objects.None;

                            //Updates player coords
                            Player.Coords = new Coords(Player.Coords.X-1,
                            Player.Coords.Y);

                            isValidMove = true;
                        }
                        //if checkmoves returns false, aka move is invalid
                        else
                        {
                            Console.WriteLine(" :Invalid Move!");                            
                        }
                        break;
                    //If movement is left arrow key or "a"
                    case "a":

                        //if checkmoves returns true, aka move is valid
                        if (CheckMove(new Coords(Player.Coords.X,
                        Player.Coords.Y-1)))
                        {
                            //Holds coords of player after movement
                            none = Map.layout[new Coords(Player.Coords.X,
                            Player.Coords.Y-1)];

                            //Turns player tile into a available tile
                            Map.layout[Player.Coords] = Objects.None;

                            //Updates player coords
                            Player.Coords = new Coords(Player.Coords.X,
                            Player.Coords.Y-1);

                            isValidMove = true;
                        }
                        //if checkmoves returns false, aka move is invalid
                        else
                        {   
                            Console.WriteLine(" :Invalid Move!");
                        }
                        break;
                    //If movement is down arrow key or "s"
                    case "s" :

                        //if checkmoves returns true, aka move is valid
                        if (CheckMove(new Coords(Player.Coords.X+1,
                        Player.Coords.Y)))
                        {
                            //Holds coords of player after movement
                            none = Map.layout[new Coords(Player.Coords.X+1,
                            Player.Coords.Y)];

                            //Turns player tile into a available tile
                            Map.layout[Player.Coords] = Objects.None;

                            //Updates player coords
                            Player.Coords = new Coords(Player.Coords.X+1,
                            Player.Coords.Y);

                            isValidMove = true;
                        }
                        //if checkmoves returns false, aka move is invalid
                        else
                        {
                            Console.WriteLine(" :Invalid Move!");
                        }
                        break;
                    //If movement is right arrow key or "d"
                    case "d" :

                        //if checkmoves returns true, aka move is valid
                        if (CheckMove(new Coords(Player.Coords.X,
                        Player.Coords.Y+1)))
                        {
                            //Holds coords of player after movement
                            none = Map.layout[new Coords(Player.Coords.X,
                            Player.Coords.Y+1)];

                            //Turns player tile into a available tile
                            Map.layout[Player.Coords] = Objects.None;

                            //Updates player coords
                            Player.Coords = new Coords(Player.Coords.X,
                            Player.Coords.Y+1);

                            isValidMove = true;
                        }
                        //if checkmoves returns false, aka move is invalid
                        else
                        {
                            Console.WriteLine(" :Invalid Move!");
                        }
                        break;
                    case "h":
                        Console.WriteLine(" :Invalid input!");
                        break;
                }
            }
              
            //Checks if player entered victory tile
            if (none == Objects.Victory)
            {
                //Turns win to true
                Win = true;
            }
            //Turns tile into a player
            Map.layout[Player.Coords] = Objects.Player;
            //Reduces player health by 1
            Player.HP-=1;
        }
        /// <summary>
        /// Verifies is the player can move to the requested position
        /// </summary>
        /// <param name="coords"> coordinates to which the object is going
        /// to move to, to compare with whatever is on said coordinates</param>
        /// <returns></returns>
        /// Returns true if move is valid
        /// Returns false if mve in invalid
        public bool CheckMove(Coords coords)
        {
            //Checks if the movement goes to out of bounds
            if (!Map.layout.ContainsKey(coords))
            {
                return false;
            }
            //Checks if the movement goes to a valid tile 
            if (Map.layout[coords] == Objects.None)
            {
                feedback = "Lost 1 hp because you walked";
                return true;
            }
            //Checks if the movement goes to a small power up
            if (Map.layout[coords] == Objects.SmallPowerups)
            {
                //Increases player health by 4
                Player.HP += 4;
                feedback = "You lost 1 hp by walking but picked up" + 
                " a small Power up healing you by 4 hp";
                return true;
            }
            //Checks if the movement goes to a medium power up
            if (Map.layout[coords] == Objects.MediumPowerups)
            {
                //Increases player health by 8
                Player.HP += 8;
                feedback = "You lost 1 hp by walking but picked up" + 
                " a medium Power up healing you by 8 hp";
                return true;
            }
            //Checks if the movement goes to a large power up
            if (Map.layout[coords] == Objects.LargePowerups)
            {
                //Increases player health by 16
                Player.HP += 16;
                feedback = "You lost 1 hp by walking but picked up" + 
                " a large Power up healing you by 16 hp";
                return true;
            }
            //Checks if the movement goes to victory tile
            if (Map.layout[coords] == Objects.Victory)
            {
                return true;
            }
            //Checks if the movement goes to out of bounds
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
            
            // Check if distance after movement is less than the original
            if(distanceUp < distance)
            {
                //Creates coords for the movement
                newCoords = new Coords (enemyCoords.X-1, enemyCoords.Y);

                //If EnemyCheckMove returns true, aka is a valid move 
                if(EnemyCheckMove(enemy, newCoords, enemyCoords))
                {
                    //Returns to 0 the number of moves not available
                    moveNotAvaiable = 0;

                    //Closes method 
                    return ;
                    
                }
                //If EnemyCheckMove returns false, aka is a invalid move
                else
                {
                    //Increments the number of moves not available
                    moveNotAvaiable++;
                }
            }

            // Check if distance after movement is less than the original
            if( distanceLeft < distance)
            {
                //Creates coords for the movement
                newCoords = new Coords (enemyCoords.X, enemyCoords.Y-1);

                //If EnemyCheckMove returns true, aka is a valid move
                if(EnemyCheckMove(enemy, newCoords, enemyCoords))
                {
                    //Returns to 0 the number of moves not available
                    moveNotAvaiable = 0;

                    //Closes method
                    return;
                }
                //If EnemyCheckMove returns false, aka is a invalid move
                else
                {
                    //Increments the number of moves not available
                    moveNotAvaiable++;
                }
            }
            // Check if distance after movement is less than the original
            if( distanceDown < distance)
            {
                //Creates coords for the movement
                newCoords = new Coords (enemyCoords.X+1, enemyCoords.Y);

                //If EnemyCheckMove returns true, aka is a valid move
                if(EnemyCheckMove(enemy, newCoords, enemyCoords))
                {
                    //Returns to 0 the number of moves not available
                    moveNotAvaiable = 0;

                    //Closes method
                    return ;
                }
                //If EnemyCheckMove returns false, aka is a invalid move
                else
                {
                    //Increments the number of moves not available
                    moveNotAvaiable++;
                }
            }
            // Check if distance after movement is less than the original
            if( distanceRight < distance)
            {
                //Creates coords for the movement
                newCoords = new Coords (enemyCoords.X, enemyCoords.Y+1);

                //If EnemyCheckMove returns true, aka is a valid move
                if(EnemyCheckMove(enemy, newCoords, enemyCoords))
                {
                    //Returns to 0 the number of moves not available
                    moveNotAvaiable = 0;

                    //Closes method
                    return;
                }
                //If EnemyCheckMove returns false, aka is a invalid move
                else
                {
                    //Increments the number of moves not available
                    moveNotAvaiable++;
                }
            }
            
            //If number of invalid moves is more than 0
            if(moveNotAvaiable>0)
            {
                //Creates an array to hold the valid moves
                int[] validRandom;

                //Number of valid moves
                int numberOfValidRandom = 0;

                //Gets the array of valid moves
                validRandom = EnemyRandomMove(enemy, enemy.Coords);

                //go through the array
                for(int i = 0; i < 4 ;i++)
                {
                    //if the array in index i equals 1
                    //Increment the number of valid moves
                    if (validRandom[i] == 1){
                        numberOfValidRandom++;
                    }
                }

                //Checks if the array as at least 1 valid move
                if (numberOfValidRandom > 0){
                    bool isValidMove = false;

                    //While the enemy does not make a valid move
                    while(!isValidMove)
                    {
                        //Get a random int to access the array in that index
                        int index = Map.Random.Next(4);

                        //If that index is equal to 1
                        if (validRandom[index] == 1)
                        {
                            //Check if index is 0
                            if(index == 0)
                            {
                                //Creates coords for the movement
                                newCoords = new Coords (enemyCoords.X-1,
                                enemyCoords.Y);
                                //Turns isValidMove to true
                                isValidMove = EnemyCheckMove(enemy, newCoords, 
                                enemyCoords);   

                            }
                            //Check if index is 1
                            else if(index == 1)
                            {
                                //Creates coords for the movement
                                newCoords = new Coords (enemyCoords.X+1,
                                enemyCoords.Y);
                                //Turns isValidMove to true
                                isValidMove = EnemyCheckMove(enemy, newCoords,
                                enemyCoords);  

                            }
                            //Check if index is 2
                            else if(index == 2)
                            {
                                //Creates coords for the movement
                                newCoords = new Coords (enemyCoords.X,
                                enemyCoords.Y-1);
                                //Turns isValidMove to true
                                isValidMove = EnemyCheckMove(enemy, newCoords,
                                enemyCoords);  

                            }
                            //Check if index is 3
                            else if(index == 3)
                            {
                                //Creates coords for the movement
                                newCoords = new Coords (enemyCoords.X,
                                enemyCoords.Y+1);
                                //Turns isValidMove to true
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
        /// Returns true if enemy can move
        /// Returns false if enemy can't move
        public bool EnemyCheckMove(Enemy enemy, Coords newCoords,
         Coords originalCoords)
        {
            //Checks if after movement the enemy is off the map
            if (!Map.layout.ContainsKey(newCoords))
            {   
                return false;
            }
            //Checks if it's going to a available tile
            else if (Map.layout[newCoords] == Objects.None)
            {
                //moves the enemy to the new coords
                enemy.move(newCoords);

                //Checks which type of enemy is moving
                if (Map.layout[originalCoords] == Objects.Boss)
                {
                    //Changes the available tile to a boss
                    Map.layout[newCoords] = Objects.Boss;
                }
                else
                {
                    //Changes the available tile to a minion
                    Map.layout[newCoords] = Objects.Minion;
                }
                //Changes the previous enemy tile to a 
                //available tile
                Map.layout[originalCoords] = Objects.None; 
                return true;            
            }
            //Checks if it's going to the player tile
            else if (Map.layout[newCoords] == Objects.Player)
            {
                //Checks which type of enemy is moving
                if (Map.layout[originalCoords] == Objects.Boss)
                {
                    //decreases player health by 10
                    Player.HP =Player.HP - 10;
                    feedback = "You got hit by a boss losing 10 hp";
                }
                else
                {
                    //decreases player health by 5
                    Player.HP = Player.HP - 5;
                    feedback = "You got hit by a minion losing 5 hp";
                }
                return true;
            }
            //Checks if it's going to a wall
            else if (Map.layout[newCoords] == Objects.Wall)
            {
                return false;
            }
            return false;
        }
        /// <summary>
        /// Checks which moves are available stores them in validArray
        /// </summary>
        /// <param name="enemy"></param>
        /// <param name="originalCoords"></param>
        /// <returns></returns>
        /// Returns the valid array
        public int[] EnemyRandomMove(Enemy enemy, Coords originalCoords)
        {
            //Creates the validArray
            int[] validArray = {0, 0, 0, 0};

            //Coords for moving up
            Coords movingUP = new Coords(originalCoords.X-1, 
            originalCoords.Y);

            //Coords for moving down
            Coords movingDown = new Coords(originalCoords.X+1,
            originalCoords.Y);

            //Coords for moving left
            Coords movingLeft = new Coords(originalCoords.X, 
            originalCoords.Y-1);

            //Coords for moving right
            Coords movingRight = new Coords(originalCoords.X, 
            originalCoords.Y+1);

            //Checks if enemy can move up
            if (Map.layout.ContainsKey(movingUP) &&
            Map.layout[movingUP] == Objects.None)
            {   
                //adds 1 to index 0 of validArray
                validArray[0] = 1; 
            }

            //Checks if enemy can move down
            if (Map.layout.ContainsKey(movingDown) &&
            Map.layout[movingDown] == Objects.None)
            {
                //adds 1 to index 1 of validArray
                validArray[1] = 1; 
            }

            //Checks if enemy can move left
            if (Map.layout.ContainsKey(movingLeft) &&
            Map.layout[movingLeft] == Objects.None)
            {
                //adds 1 to index 2 of validArray
                validArray[2] = 1; 
            }

            //Checks if enemy can move right
            if (Map.layout.ContainsKey(movingRight) &&
            Map.layout[movingRight] == Objects.None)
            {
                //adds 1 to index 3 of validArray
                validArray[3] = 1; 
            }
            
            //Returns the array
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

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("☻ = Player");
            Console.ResetColor();

            Console.Write(" | ");
            Console.Write("█ = Obstacle");
            Console.Write(" | ");

            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("☼ = Minion");
            Console.ResetColor();

            Console.Write(" | ");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write("☺ = Boss");
            Console.ResetColor();
            Console.Write(" | ");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("֍ = Victory\n");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("♠ = Small PowerUp");
            Console.ResetColor();
            Console.Write(" | ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write(" ♣ = Medium PowerUp");
            Console.ResetColor();
            Console.Write(" | ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("♥ = Large PowerUp\n");
            Console.ResetColor();

            Map.RefreshMap(rows,columns);

            Console.WriteLine("Level: " + Level + " " +
            "PlayerHP: " + Player.HP);

            Console.WriteLine("W to move Up | A to move left |" + 
            " S to move down | D to move right\n");
            
            Console.WriteLine(feedback);
        }
    }
}