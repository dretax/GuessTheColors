using System;
using System.Diagnostics;
using System.Threading;

namespace GuessTheColors_AS4DCD_DreTaX
{
    /// <summary>
    /// Menü kezelését végző osztály
    /// </summary>
    internal class Menu
    {
        /// <summary>
        /// A teljes program ezen változó alatt fog futni. Ha ez hamis, a program leáll.
        /// </summary>
        private bool Run = true;
        /// <summary>
        /// A Game osztályt itt tároljuk
        /// </summary>
        private static Game _game;
        /// <summary>
        /// A debugolásához szükséges jelszó. (Csak az egyszerűség érdekében =) )
        /// </summary>
        private const string AdmPW = "123";
        /// <summary>
        /// A Debug mode-t tároló bool típus
        /// </summary>
        internal bool PDebug = false;

        /// <summary>
        /// A játékos utasításainak végrehajtásáért felelős void.
        /// </summary>
        internal void Watcher()
        {
            while (Run)
            {
                Help();
                string s = Console.ReadLine();
                int i;
                bool b = int.TryParse(s, out i);
                while (!b)
                {
                    Console.WriteLine("Hibás Opció! Listázom a lehetőségeket.");
                    Help();
                    s = Console.ReadLine();
                    b = int.TryParse(s, out i);
                }
                switch (i)
                {
                    case 1:
                    {
                        _game = new Game();
                        break;
                    }
                    case 2:
                    {
                        Console.WriteLine();
                        Console.WriteLine("===TopLista===");
                        if (Program.GetDatabase.AllSavedPlayers.Count == 0)
                        {
                            Console.WriteLine("Még nincs beállítva új rekord!");
                        }
                        else
                        {
                            int ii = 1;
                            foreach (var x in Program.GetDatabase.AllSavedPlayers)
                            {
                                Console.WriteLine(ii + ". " + x.Name + " Pontok: " + x.Score);
                                ii++;
                            }
                        }
                        Console.WriteLine("======");
                        Console.WriteLine();
                        break;
                    }
                    case 3:
                    {
                        Run = false;
                        Console.WriteLine("Kilépés megkezdése 5 másodpercen belül....");
                        if (_game != null)
                        {
                            Program.GetDatabase.SaveScores();
                        }
                        Thread.Sleep(5000);
                        Process.GetCurrentProcess().Kill();
                        break;
                    }
                    case 4:
                    {
                        Console.WriteLine("Kérem a jelszót! Ha nem tudod írd be: 'exit'");
                        string code = Console.ReadLine();
                        bool Fail = false;
                        while (code != AdmPW)
                        {
                            if (code == "exit")
                            {
                                Fail = true;
                                break;
                            }
                            Console.WriteLine("Kérem a jelszót! (Az előző hibás volt!)");
                            code = Console.ReadLine();
                        }
                        if (Fail)
                        {
                            break;
                        }
                        PDebug = !PDebug;
                        Console.WriteLine();
                        if (PDebug) Console.WriteLine("Debug bekapcsolva!");
                        else Console.WriteLine("Debug kikapcsolva!");
                        break;
                    }
                    default:
                    {
                        Console.WriteLine("Hibás Opció! Listázom a lehetőségeket.");
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Szimpla 'Help' üzenetek
        /// </summary>
        internal void Help()
        {
            Console.WriteLine();
            Console.WriteLine("Válassz egy opciót (Üss be egy számot)");
            Console.WriteLine("1. Játék");
            Console.WriteLine("2. Toplista");
            Console.WriteLine("3. Kilépés");
            Console.WriteLine("4. Debug Be/Ki (Jelszó Szükséges)");
        }

    }
}
