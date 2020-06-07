using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
namespace Game
{
    public class HighScore
    {
        public HighScore(int rows, int colums, int level, int option)
        {
            if (option == 0)
                SetHighScore(rows, colums, level);
            else
                GetHighScore(rows, colums);
        }
        private void SetHighScore(int rows, int colums, int level)
        {
            List<Scores> scores = new List<Scores>();

            string docPath = Directory.GetCurrentDirectory();
            string[] lines = System.IO.File.ReadAllLines(Path.Combine(docPath, "HighScores.txt"));
            int l = 0;

            string name = String.Empty;
            int score = 0;
            int frow = 0;
            int fcolumn = 0;

            foreach (string line in lines)
            {
                if (l == 4)
                {
                    l = 0;
                }
                if(l == 0)
                {
                    name = line;
                }
                if (l == 1)
                {
                    score = int.Parse(line);
                }
                if (l == 2)
                {
                    frow = int.Parse(line);
                }
                if (l == 3)
                {
                    fcolumn = int.Parse(line);
                    scores.Add(new Scores(name, score, frow, fcolumn));
                }

                l++;
            }

            File.WriteAllText(Path.Combine(docPath, "HighScores.txt"), String.Empty);

            int scoresCount = 0;
            foreach (Scores sc in scores)
            {
                if (sc.Rows == rows && sc.Columns == colums)
                {
                    scoresCount += 1;
                }
            }

            if (scoresCount < 10)
            {
                scores.Add(new Scores("Nelson", level, rows, colums));
            }
                
            scores.Sort();

            
            foreach (Scores sc in scores)
            {
                using (StreamWriter writer = new StreamWriter(Path.Combine(docPath, "HighScores.txt"), true))
                {
                    writer.WriteLine(sc.Name);
                    writer.WriteLine(sc.Score);
                    writer.WriteLine(sc.Rows);
                    writer.WriteLine(sc.Columns);
                }
            }

        }

        private void GetHighScore(int rows, int colums)
        {
            List<Scores> scores = new List<Scores>();
            string docPath = Directory.GetCurrentDirectory();
            string[] lines = System.IO.File.ReadAllLines(Path.Combine(docPath, "HighScores.txt"));
            int l = 0;

            string name = String.Empty;
            int score = 0;
            int frow = 0;
            int fcolumn = 0;

            foreach (string line in lines)
            {
                if (l == 4)
                {
                    l = 0;
                }
                if(l == 0)
                {
                    name = line;
                }
                if (l == 1)
                {
                    score = int.Parse(line);
                }
                if (l == 2)
                {
                    frow = int.Parse(line);
                }
                if (l == 3)
                {
                    fcolumn = int.Parse(line);
                    scores.Add(new Scores(name, score, frow, fcolumn));
                }

                l++;
            }
            Console.WriteLine($"Top 10 HighScores ({rows} rows | {colums} columns)");
            foreach (Scores sc in scores)
            {
                if (sc.Rows == rows && sc.Columns == colums)
                {
                    Console.WriteLine($"Name: {sc.Name}");
                    Console.WriteLine($"Score: {sc.Score}");
                    Console.WriteLine("----------------------------");
                }
                
            }
            
        }
    }
}