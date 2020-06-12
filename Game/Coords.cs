  namespace Game
{
    /// <summary>
    /// Stores all positions
    /// </summary>
    public struct Coords
    {
        public int X {get;}
        public int Y {get;}

        public Coords(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}