using System.Collections;
using Assets.Scripts;
using Assets.Scripts.Tools;

namespace Assets.Scripts
{
    public class Town
    {
        private string adjective;
        private string name;
        private Checkable person;
        private Checkable obj;
        private Tool tool;
        private bool markedSafe = false;
        private bool markedThief = false;
        private bool markedBear = false;
        private bool isNuke = false;

        public Town(string adjective, string name, Checkable person, Checkable obj, Tool tool)
        {
            this.adjective = adjective;
            this.name = name;
            this.person = person;
            this.obj = obj;
            this.tool = tool;
        }

        public void Talk()
        {
            person.Check();
        }

        public void Check()
        {
            obj.Check();
            if (tool == null)
            {
                // display "...but you didn't find anything useful."
            }
            else
            {
                // add tool to player inventory
            }
        }

        public bool CanTalk()
        {
            return !person.HasChecked();
        }

        public bool CanCheck()
        {
            return !obj.HasChecked();
        }

        public bool MarkedSafe()
        {
            return markedSafe;
        }

    }
}