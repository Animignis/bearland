using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
    public class Checkable
    {
        protected string name;
        protected string identifier = "the";
        protected bool hasChecked = false;

        public Checkable()
        {
        }

        public Checkable(string name)
        {
            this.name = name;
        }

        public Checkable(string name, string identifier)
        {
            this.name = name;
            this.identifier = identifier;
        }

        public bool HasChecked()
        {
            return hasChecked;
        }

        public string GetName()
        {
            return name;
        }

        public virtual void Check()
        {
            hasChecked = true;
        }
    }
}
