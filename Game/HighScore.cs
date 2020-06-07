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
            string docPath = Directory.GetCurrentDirectory();
            string[] lines = System.IO.File.ReadAllLines(Path.Combine(docPath, "HighScores.txt"));
            int l = 0;
            string r = Convert.ToString(rows);
            string c = Convert.ToString(colums);
            bool highScoreExists = false;

            foreach (string line in lines)
            {
                if (line.Length >= 10 && line[0] == 'R')
                {

                    if (Convert.ToString(lines[l][7]) == r && Convert.ToString(lines[l][18]) == c)
                    {
                        highScoreExists = true;
                        break;
                    }
                    
                }
                l++;
            }
            
            if (highScoreExists)
            {
                int x = int.Parse(lines[l+1][19].ToString());

                if (x < level)
                {
                    lines[l] = null;
                    lines[l + 1] = null;
                    File.WriteAllText(Path.Combine(docPath, "HighScores.txt"), String.Empty);
                    File.WriteAllLines(Path.Combine(docPath, "HighScores.txt"), lines);
                    using (StreamWriter writer = new StreamWriter(Path.Combine(docPath, "HighScores.txt"), true))
                    {
                        writer.WriteLine($"Rows : {rows} Columns: {colums}");
                        writer.WriteLine($"HighScore (Level): {level}");
                    }
                }

            }
            else
            {
                using (StreamWriter writer = new StreamWriter(Path.Combine(docPath, "HighScores.txt"), true))
                {
                    writer.WriteLine($"Rows : {rows} Columns: {colums}");
                    writer.WriteLine($"HighScore (Level): {level}");
                }
            }

        }

        private void GetHighScore(int rows, int colums)
        {
            string docPath = Directory.GetCurrentDirectory();
            string[] lines = System.IO.File.ReadAllLines(Path.Combine(docPath, "HighScores.txt"));
            string r = Convert.ToString(rows);
            string c = Convert.ToString(colums);
            int l = 0;

            Console.WriteLine("Current HighScore: ");
            foreach (string line in lines)
            {
                if (line.Length >= 10 && line[0] == 'R')
                {

                    if (Convert.ToString(lines[l][7]) == r && Convert.ToString(lines[l][18]) == c)
                    {
                        Console.WriteLine(lines[l]);
                        Console.WriteLine(lines[l+1]);
                        break;
                    }
                    
                }
                l++;
            }
            
        }
    }
}