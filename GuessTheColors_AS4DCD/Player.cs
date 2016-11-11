using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace GuessTheColors_AS4DCD_DreTaX
{
    /// <summary>
    /// Itt tároljuk a TopListában szereplő játékosok adatait
    /// </summary>
    internal class Player
    {
        /// <summary>
        /// Játékos Neve
        /// </summary>
        private readonly string _Name;
        /// <summary>
        /// Játékos Pontja
        /// </summary>
        private readonly int _Score;

        /// <summary>
        /// Player Konstruktor.
        /// </summary>
        /// <param name="name">Játékos Neve</param>
        /// <param name="score">Játékos Pontja</param>
        internal Player(string name, int score)
        {
            _Name = name;
            _Score = score;
        }

        /// <summary>
        /// Visszadobja a játékos nevét
        /// </summary>
        internal string Name
        {
            get { return _Name; }
        }

        /// <summary>
        /// Visszadobja a játékos pontszámát
        /// </summary>
        internal int Score
        {
            get { return _Score; }
        }
    }
}
