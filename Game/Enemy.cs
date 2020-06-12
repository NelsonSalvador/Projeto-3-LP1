namespace Game
{
    /// <summary>
    /// Handles enemy type and coords for all enemies
    /// </summary>
    public class Enemy
    {
        /// <summary>
        /// Property that contains enemy coords
        /// </summary>
        /// <value></value>
        public Coords Coords {get; set;}

        /// <summary>
        /// Property that contains enemy type
        /// </summary>
        /// <value></value>
        public Objects Type {get;}

        /// <summary>
        /// Creates new enemy
        /// </summary>
        /// <param name="coords"></param>
        /// <param name="type"></param>
        public Enemy(Coords coords, Objects type)
        {
            Type = type;
            Coords = coords; 
        }

        /// <summary>
        /// Updates enemy coords
        /// </summary>
        /// <param name="coords"></param>
        public void move(Coords coords)
        {
            Coords = coords;
        }
    }
}