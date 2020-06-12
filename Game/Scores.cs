using System;
namespace Game
{ 
    /// <summary>
    /// Stores scores information
    /// </summary>
    public struct Scores : IComparable<Scores>
    {
        public string Name;
        public int Score;
        public int Rows;
        public int Columns;

        /// <summary>
        /// Enum that has the score information
        /// </summary>
        /// <param name="name"> The player name </param>
        /// <param name="score">The Player score</param>
        /// <param name="rows">The rows of the map on witch the score was
        ///  set</param>
        /// <param name="columns">The columns of the map on witch the score was
        ///  set</param>
        public Scores(string name, int score, int rows, int columns)
        {
            Name = name;
            Score = score;
            Rows = rows;
            Columns = columns;
        }

        /// <summary>
        /// Used to sort the list of highscores
        /// </summary>
        /// <param name="other">Enum scores</param>
        /// <returns></returns>
        public int CompareTo(Scores other)
        {
            if (other.Name == null) return -1;
            return other.Score - Score;
        }
    }
}