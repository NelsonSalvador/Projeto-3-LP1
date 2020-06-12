namespace Game
{
    /// <summary>
    /// Handles power up type and coords for all power ups
    /// </summary>
    public class PowerUp
    {
        /// <summary>
        /// Handles power up coords
        /// </summary>
        /// <value></value>
        public Coords Coords {get; set;} 

        /// <summary>
        /// Updates power up coords
        /// </summary>
        /// <param name="coords"></param>
        public PowerUp(Coords coords)
        {
            Coords = coords; 
        } 
    }
}