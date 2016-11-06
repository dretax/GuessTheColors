using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace GuessTheColors_AS4DCD_DreTaX
{
    internal class Player
    {
        private readonly string _Name;
        private readonly int _Score;

        internal Player(string name, int score)
        {
            _Name = name;
            _Score = score;
        }

        internal string Name
        {
            get { return _Name; }
        }

        internal int Score
        {
            get { return _Score; }
        }
    }
}
