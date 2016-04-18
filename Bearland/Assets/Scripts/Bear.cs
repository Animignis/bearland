using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
    public class Bear : Checkable
    {
        public Bear(string name, string identifier)
        {
            this.name = name;
            this.identifier = identifier;
        }

        override public void Check()
        {
            // TODO: display game-loss state, restart the game
        }
    }
}
