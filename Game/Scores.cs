using System;
namespace Game
{
    public struct Scores : IComparable<Scores>
    {
        public string Name;
        public int Score;
        public int Rows;
        public int Columns;

        public Scores(string name, int score, int rows, int columns)
        {
            Name = name;
            Score = score;
            Rows = rows;
            Columns = columns;
        }

        public int CompareTo(Scores other)
        {
            if (other.Name == null) return -1;
            return other.Score - Score;
        }
    }
}