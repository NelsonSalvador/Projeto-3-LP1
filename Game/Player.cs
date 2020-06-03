using System;

namespace Game
{
    /// <summary>
    /// This class will have all of the player related stats
    /// </summary>
    public class Player
    {
        public Coords Coords {get; set;}
        public int HP {get; set;}

        public Player(int rows, int columns)
        {
            Random rand = new Random();
            Coords = new Coords(0, rand.Next(rows));
            HP = (rows * columns)/4;
        }
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