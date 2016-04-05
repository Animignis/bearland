using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
    public class Checkable
    {
        protected string name;
        protected bool hasChecked = false;

        public Checkable(string name)
        {
            this.name = name;
        }

        public Checkable()
        {
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
