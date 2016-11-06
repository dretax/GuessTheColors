using System;
using System.Collections.Generic;
using System.Linq;

namespace GuessTheColors_AS4DCD_DreTaX
{
    internal class Game
    {
        internal List<string> Colors = new List<string> { "K", "Z", "P", "S", "F", "L", "N" };
        internal Random r;
        internal readonly Dictionary<int, string> RandomizedColors = new Dictionary<int, string>();
        internal readonly Dictionary<int, string> UserTips = new Dictionary<int, string>();
        internal int Trials = 0;
        internal bool Match = false;

        internal Game()
        {
            RandomColors();
            Console.WriteLine("Gondoltam 5 random színre!");
            Console.WriteLine("A következő színek fordulhatnak elő (Akár többször is)");
            Console.WriteLine("K(ék), Z(öld), P(iros), S(árga), F(ehér), L(ila), N(arancs)");
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

        internal void TakeGuesses()
        {
            for (int i = 1; i <= 5; i++)
            {
                Console.WriteLine("Válassz egy színt! (" + (i-1) + "/5" + ") [Eddigi Próbálkozások: " + Trials + "]");
                string s = Console.ReadLine();
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

        internal void CheckGuesses()
        {
            string s = " ";
            int p2 = 0;
            foreach (var x in RandomizedColors.Keys)
            {
                if (Program.Debug)
                {
                    Console.WriteLine();
                    Console.WriteLine("DEBUG(GÉP - JÁTÉKOS) " + RandomizedColors[x] + " -- " + UserTips[x]);
                    Console.WriteLine();
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

        internal void RandomColors()
        {
            r = new Random();
            RandomizedColors.Clear();
            for (int i = 1; i <= 5; i++)
            {
                RandomizedColors[i] = Colors[r.Next(0, 7)];
            }
        }
    }
}
