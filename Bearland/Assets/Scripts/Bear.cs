using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
    public class Bear : Checkable
    {
        public Bear()
        {
            this.name = "BEAR";
        }

        override public void Check()
        {
            // TODO: display game-loss state, restart the game
        }
    }
}
