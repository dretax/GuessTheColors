using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GuessTheColors_AS4DCD_DreTaX
{
    internal class Database
    {
        internal readonly string FilePath;
        internal string[] Lines; 
        internal List<Player> AllSavedPlayers = new List<Player>(5); 

        internal Database(string path)
        {
            FilePath = path;
            if (!File.Exists(path))
            {
                File.Create(path).Dispose();
            }
        }

        internal bool CheckIfNewTop(int Score)
        {
            if (AllSavedPlayers.Count < 5)
            {
                return true;
            }
            foreach (var x in AllSavedPlayers)
            {
                if (Score < x.Score)
                {
                    return true;
                }
            }
            return false;
        }

        internal void AddNewTop(string name, int Score)
        {
            var user = new Player(name, Score);
            AllSavedPlayers.Add(user);
            SortScores();
            for (int i = 0; i < AllSavedPlayers.Count; i++)
            {
                if (i > 4)
                {
                    AllSavedPlayers.RemoveAt(i);    
                }
            }
            SaveScores();
        }

        internal void ReadFile()
        {
            AllSavedPlayers.Clear();
            Lines = System.IO.File.ReadAllLines(FilePath);
            foreach (string line in Lines)
            {
                var split = line.Split(Convert.ToChar("="));
                if (split.Length <= 1 || string.IsNullOrEmpty(split[0]))
                {
                    continue;
                }
                int score;
                bool b = int.TryParse(split[1], out score);
                if (!b) score = 0;
                var user = new Player(split[0], score);
                AllSavedPlayers.Add(user);
            }
            SortScores();
        }

        internal void SortScores()
        {
            AllSavedPlayers = AllSavedPlayers.OrderBy(x => x.Score).ToList();
        }

        internal void SaveScores()
        {
            System.IO.File.WriteAllText(FilePath, string.Empty);
            var fileadder = System.IO.File.AppendText(FilePath);
            foreach (var x in AllSavedPlayers)
            {
                fileadder.WriteLine(x.Name + "=" + x.Score);
            }
            fileadder.Close();
        }
    }
}