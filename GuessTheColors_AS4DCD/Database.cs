using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GuessTheColors_AS4DCD_DreTaX
{
    /// <summary>
    /// A TopLista adatbázisát kezelő osztály
    /// </summary>

    internal class Database
    {
        /// <summary>
        /// TXT Elérési útvonala
        /// </summary>
        internal readonly string FilePath;
        /// <summary>
        /// A fileból olvasott sorok
        /// </summary>
        internal string[] Lines; 
        /// <summary>
        /// A Tárolt Top Játékosok Listában, Player Object-el
        /// </summary>
        internal List<Player> AllSavedPlayers = new List<Player>(5); 


        /// <summary>
        /// Adatbázis Konstruktor
        /// </summary>
        /// <param name="path">Adott elérési útvonalon létrehozza a TXT-t ha az nem létezik</param>
        internal Database(string path)
        {
            FilePath = path;
            if (!File.Exists(path))
            {
                File.Create(path).Dispose();
            }
        }


        /// <summary>
        /// Ellenőrzi, hogy az adott pont amelyet elért a játékos, toplistába való-e
        /// </summary>
        /// <param name="Score">A pontszám</param>
        /// <returns>Ha igen akkor true-t dob vissza</returns>
        internal bool CheckIfNewTop(int Score)
        {
            if (AllSavedPlayers.Count < 5)
            {
                return true;
            }
            //return AllSavedPlayers.Any(x => Score < x.Score);
            foreach (var x in AllSavedPlayers)
            {
                if (Score < x.Score)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Hozzáadja a játékosnevet, és a pontszámát a toplistához (Memóriában, és Fileban is)
        /// </summary>
        /// <param name="name">Játékos Neve</param>
        /// <param name="Score">Játékos Pontja</param>
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

        /// <summary>
        /// Beolvassa a toplista file-t, és ha vannak játékosok, akkor létrehozza az osztályt,
        /// majd egy listába helyezi őket
        /// </summary>
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

        /// <summary>
        /// Egy szimpla LINQ Query, amely egy egysoros ciklus.
        /// Lényegében az összes Player osztályon végig fut, és azokat a
        /// Score változó alapján növekvő sorrendbe rendezi őket, majd
        /// az Enumerable Listát rendes Listává konvertálja.
        /// </summary>
        internal void SortScores()
        {
            AllSavedPlayers = AllSavedPlayers.OrderBy(x => x.Score).ToList();
        }

        /// <summary>
        /// Kiüríti a Toplista fileját, és a memóriában tárolt adatokat beleírja.
        /// </summary>
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