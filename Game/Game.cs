namespace Game
{
    public class Game
    {
        public Player Player {get; set;}
        public Map Map {get; set;}
        public Game(int rows, int columns)
        {
            Player = new Player(rows, columns);
            Map = new Map(rows, columns, Player);       
        }
        public void StartGame()
        {
            while (!Player.IsDead())
            {
                //input
                //show input anwser
                //Generate map
                //game loop
                //ask for input
                //Move pieces & player
            }
        }
    }
}