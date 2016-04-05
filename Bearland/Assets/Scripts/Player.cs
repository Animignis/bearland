using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Assets.Scripts.Tools;

namespace Assets.Scripts
{
    public class Player
    {
        private List<Tool> inventory;
        private int passes;

        // SINGLETON PATTERN BEGIN
        private static volatile Player _instance;
        private static object _lock = new object();
        static Player() { }
        public static Player Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new Player();
                        }
                    }
                }
                return _instance;
            }
        }
        // SINGLETON PATTERN END

        private Player() // private constructor needed for Singleton pattern
        {
            Reset();
        }

        public void Reset()
        {
            inventory = new List<Tool>();
            passes = 0;
        }

        public void GainTool(Tool tool)
        {
            inventory.Add(tool);
        }

        public void Pass()
        {
            passes++;
        }
    }
}