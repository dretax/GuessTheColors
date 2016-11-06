using System;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace GuessTheColors_AS4DCD_DreTaX
{
    internal class Program
    {
        private static bool Run = true;
        private const string AdmPW = "123";
        private static Game _game;
        private static string TopListPath;
        private static Database TopListIni;
        internal static bool Debug = false;

        /*
         *  Internal Voids
         */

        internal static void Main(string[] args)
        {
            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Üdvözöllek a SzínKitaláló játékban!");
            TopListPath = Directory.GetCurrentDirectory() + "\\TopList.txt";
            if (!File.Exists(TopListPath))
            {
                File.Create(TopListPath).Dispose();
            }
            TopListIni = new Database(TopListPath);
            TopListIni.ReadFile();
            Watcher();
        }

        internal static void Watcher()
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
                        if (TopListIni.AllSavedPlayers.Count == 0)
                        {
                            Console.WriteLine("Még nincs beállítva új rekord!");
                        }
                        else
                        {
                            int ii = 1;
                            foreach (var x in TopListIni.AllSavedPlayers)
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
                        Console.WriteLine("Kilépés megkezdése 5 másodpercen belül....");
                        if (_game != null)
                        {
                            TopListIni.SaveScores();
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
                        if (Fail) { break;}
                        Debug = !Debug;
                        if (Debug) Console.WriteLine("Debug bekapcsolva!");
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

        internal static void Help()
        {
            Console.WriteLine();
            Console.WriteLine("Válassz egy opciót (Üss be egy számot)");
            Console.WriteLine("1. Játék");
            Console.WriteLine("2. Toplista");
            Console.WriteLine("3. Kilépés");
            Console.WriteLine("4. Debug Be/Ki (Jelszó Szükséges)");
        }

        internal static Database GetDatabase
        {
            get { return TopListIni;}
        }
    }
}
