using System;
using System.IO;

namespace GuessTheColors_AS4DCD_DreTaX
{
    /// <summary>
    /// A Program osztály maga
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// TopLista elérési útvonalát tároló változó
        /// </summary>
        private static string TopListPath;
        /// <summary>
        /// TopLista file-t feldolgozó osztály, és annak változója
        /// </summary>
        private static Database TopListIni;
        /// <summary>
        /// A menü osztálya, és annak változója
        /// </summary>
        private static Menu MHandler;

        /// <summary>
        /// A Main függvény, alap beállítások, deklarálások, menü elindítása
        /// </summary>
        /// <param name="args">Console Argumentumjai, amelyek jelen esetben nincsenek/nem szükségesek</param>
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
            MHandler = new Menu();
            MHandler.Watcher();
        }

        /// <summary>
        /// Visszadobja az Adatbázis Osztályát
        /// </summary>
        internal static Database GetDatabase
        {
            get { return TopListIni;}
        }

        /// <summary>
        /// A TopLista file elérési útvonalát dobja vissza
        /// </summary>
        internal static string GetTopPath
        {
            get { return TopListPath; }
        }

        /// <summary>
        /// A Menü osztályt dobja vissza
        /// </summary>
        internal static Menu Menu
        {
            get { return MHandler; }
        }
    }
}
