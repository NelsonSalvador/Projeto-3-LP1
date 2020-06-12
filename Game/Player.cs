using System;

namespace Game
{
    /// <summary>
    /// This class will has of the player related stats
    /// </summary>
    public class Player
    {
        /// <summary>
        /// Property to store player coordinates
        /// </summary>
        /// <value></value>
        public Coords Coords {get; set;}
        
        /// <summary>
        /// Property to store player hp
        /// </summary>
        /// <value></value>
        public int HP {get; set;}

        /// <summary>
        /// Creates a new instance of this class
        /// </summary>
        /// <param name="rows"></param>
        /// <param name="columns"></param>
        public Player(int rows, int columns)
        {
            Random rand = new Random();
            HP = (rows * columns)/4;
        }

        /// <summary>
        /// Verification is player is dead
        /// </summary>
        /// <returns></returns>
        public bool IsDead()
        { 
            if (HP <= 0)
            {
                return true;
            }
            return false;
        }
    }
}