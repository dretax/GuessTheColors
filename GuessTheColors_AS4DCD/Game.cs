using System;
using System.Collections.Generic;
using System.Linq;

namespace GuessTheColors_AS4DCD_DreTaX
{
    /// <summary>
    /// A Game osztály, amely kezeli a Játékot
    /// </summary>
    internal class Game
    {
        /// <summary>
        /// A játékban szereplő színek kezdőbetűi
        /// </summary>
        internal readonly List<string> Colors = new List<string> { "K", "Z", "P", "S", "F", "L", "N" };
        /// <summary>
        /// Random változó
        /// </summary>
        internal readonly Random r;
        /// <summary>
        /// Az N-edik helyen szereplő, randomolt betűket tárolja
        /// </summary>
        internal readonly Dictionary<int, string> RandomizedColors = new Dictionary<int, string>();
        /// <summary>
        /// Az N-edik helyen szereplő, játékos által tippelt betűket tárolja
        /// </summary>
        internal readonly Dictionary<int, string> UserTips = new Dictionary<int, string>();
        /// <summary>
        /// A játékos eddigi próbálkozásait tároló változó
        /// </summary>
        internal int Trials = 0;
        /// <summary>
        /// Olyan változó, amellyel a játék futását lehet ellenőrizni, és annak függvényében változik
        /// </summary>
        internal bool Match = false;

        /// <summary>
        /// A Game osztály konstruktora. Az alap információk itt leszen kiírva
        /// a változók itt lesznek deklarálva, és a játékot magát is innen irányítjuk.
        /// </summary>
        internal Game()
        {
            r = new Random();
            RandomColors();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Gondoltam 5 random színre!");
            Console.WriteLine("A következő színek fordulhatnak elő (Akár többször is)");
            Console.WriteLine("K(ék), Z(öld), P(iros), S(árga), F(ehér), L(ila), N(arancs)");
            Console.WriteLine("A 'színek' szóval újra lekérheted az összes tippelhető színt.");
            Console.WriteLine();
            Console.WriteLine("Találd ki a színeket a kezdőbetűk segítségével, természetesen segítek a tipp után!");
            Console.WriteLine();
            Console.WriteLine();
            while (!Match) { TakeGuesses();}
            Console.WriteLine("Gratula megnyerted a gamet!");
            bool b = Program.GetDatabase.CheckIfNewTop(Trials);
            if (b)
            {
                Console.WriteLine();
                Console.WriteLine("Új rekord! Üss be egy felhasználónevet!");
                string nname = Console.ReadLine();
                Program.GetDatabase.AddNewTop(nname, Trials);
            }
        }

        /// <summary>
        /// A játékos tippelt színeit ellenőrizzük/kérjük be
        /// </summary>
        internal void TakeGuesses()
        {
            if (Program.Menu.PDebug)
            {
                foreach (var x in RandomizedColors.Keys)
                {
                    if (Program.Menu.PDebug)
                    {
                        Console.WriteLine();
                        Console.WriteLine("DEBUG(GÉP) " + RandomizedColors[x]);
                    }
                }
            }
            for (int i = 1; i <= 5; i++)
            {
                Console.WriteLine("Válassz egy színt! (" + (i-1) + "/5" + ") [Eddigi Próbálkozások: " + Trials + "]");
                string s = Console.ReadLine();
                while (s == "színek")
                {
                    Console.WriteLine("K(ék), Z(öld), P(iros), S(árga), F(ehér), L(ila), N(arancs)");
                    s = Console.ReadLine();
                }
                while (string.IsNullOrEmpty(s) || s.Length > 1 || s.Length == 0 || !Colors.Contains(s.ToUpper()))
                {
                    Console.WriteLine("Na de most úgy őszintén... Egy betűt kértem...");
                    Console.WriteLine("K(ék), Z(öld), P(iros), S(árga), F(ehér), L(ila), N(arancs)");
                    Console.WriteLine();
                    Console.WriteLine();
                    s = Console.ReadLine();
                }
                UserTips[i] = s.ToUpper();
            }
            Console.WriteLine("Tippelt Színek: " + string.Join(" ", UserTips.Values));
            CheckGuesses();
        }

        /// <summary>
        /// A játékos tippelt színeit ellenőrizzük, a gépi randomok függvényében.
        /// </summary>
        internal void CheckGuesses()
        {
            string s = " ";
            int p2 = 0;
            foreach (var x in RandomizedColors.Keys)
            {
                if (Program.Menu.PDebug)
                {
                    Console.WriteLine();
                    Console.WriteLine("DEBUG(GÉP - JÁTÉKOS) " + RandomizedColors[x] + " -- " + UserTips[x]);
                }
                if (RandomizedColors[x] == UserTips[x])
                {
                    s = s + "+ ";
                    p2++;
                }
                else if (RandomizedColors.Values.Contains(UserTips[x]))
                {
                    s = s + "* ";
                }
                else
                {
                    s = s + "X ";
                }
            }
            if (p2 == 5)
            {
                Match = true;
            }
            Console.WriteLine("Találatok: " + s);
            Console.WriteLine();
            Console.WriteLine();
            Trials++;
            UserTips.Clear();
        }

        /// <summary>
        /// A gép itt választ ki random kezdőbetűket, amelyet a játékosnak majd ki kell találnia.
        /// </summary>
        internal void RandomColors()
        {
            RandomizedColors.Clear();
            for (int i = 1; i <= 5; i++)
            {
                RandomizedColors[i] = Colors[r.Next(0, 7)];
            }
        }
    }
}
