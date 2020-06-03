namespace Game
{
    public class Enemy
    {
        public Coords Coords {get; set;}

        public Objects Type {get;}
        public Enemy(Coords coords, Objects type)
        {
            Type = type;
            Coords = coords; 
        }

        public void move(Coords coords)
        {
            Coords = coords;
        }
    }
}