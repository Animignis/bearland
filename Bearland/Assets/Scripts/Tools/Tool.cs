using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Tools
{
    abstract public class Tool
    {
        protected string name;
        abstract public void Use();
    }
}
