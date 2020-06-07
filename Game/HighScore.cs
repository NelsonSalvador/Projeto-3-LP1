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

            //Put the file information into the list scores
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

            scores.Sort();

            int ScoreCounter = 0;
            int lastScorePos = 0;
            //Cicle to count how many scores does this specific map has and the position in the list of the last score
            foreach (Scores sc in scores)
            {
                if (sc.Rows == rows && sc.Columns == colums)
                {
                    ScoreCounter += 1;
                }

                if(ScoreCounter == 10)
                    break;
                lastScorePos += 1;
            }

            //Decide if it is a new high score
            if (ScoreCounter < 10)
            {
                scores.Add(new Scores("Nelson", level, rows, colums));
            }
            else if (level > scores[lastScorePos].Score)
            {
                scores.RemoveAt(lastScorePos);
                scores.Add(new Scores("Nelson", level, rows, colums));
            }
                
            scores.Sort();

            // Update the file 
            File.WriteAllText(Path.Combine(docPath, "HighScores.txt"), String.Empty);
            StreamWriter writer = new StreamWriter(Path.Combine(docPath, "HighScores.txt"));
            foreach (Scores sc in scores)
            {
                writer.WriteLine(sc.Name);
                writer.WriteLine(sc.Score);
                writer.WriteLine(sc.Rows);
                writer.WriteLine(sc.Columns);
            }
            writer.Close();

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

            //Put the file information into the list scores
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

            //Output the list of HighScores of this MapLayout
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