using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using Assets.Scripts.Tools;
using UnityEngine;

namespace Assets.Scripts
{
    public class GameManager
    {
        private Town[,] grid;
        private string DATA_PATH = Application.dataPath + Path.DirectorySeparatorChar + "Data" + Path.DirectorySeparatorChar;
        private const string OBJECTS = "objects.txt";
        private const string PERSONS = "persons.txt";
        private const string TOWNS = "towns.txt";
        private const string ADJECTIVES = "adjectives.txt";
        private const string HINTS_TOOLS = "hints_tools.txt";

        private List<string> towns;
        private List<string> adjectives;
        private List<string> persons;
        private List<string> objects;
        private List<string> hintsTools;

        private Dictionary<string, string> genderMap;

        // SINGLETON PATTERN BEGIN
        private static volatile GameManager _instance;
        private static object _lock = new object();
        static GameManager() { }
        public static GameManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new GameManager();
                        }
                    }
                }
                return _instance;
            }
        }
        // SINGLETON PATTERN END

        private GameManager() // private constructor needed for Singleton pattern
        {
            genderMap = new Dictionary<string, string>();
            genderMap.Add("male", "he");
            genderMap.Add("female", "she");
            genderMap.Add("either", "they");
            genderMap.Add("asexual", "it");

            towns = new List<string>();
            adjectives = new List<string>();
            persons = new List<string>();
            objects = new List<string>();

            StreamReader reader = new StreamReader(DATA_PATH + TOWNS);
            while (!reader.EndOfStream)
            {
                towns.Add(reader.ReadLine());
            }
            reader.Close();

            reader = new StreamReader(DATA_PATH + ADJECTIVES);
            while (!reader.EndOfStream)
            {
                adjectives.Add(reader.ReadLine());
            }
            reader.Close();

            reader = new StreamReader(DATA_PATH + PERSONS);
            while (!reader.EndOfStream)
            {
                persons.Add(reader.ReadLine());
            }
            reader.Close();

            reader = new StreamReader(DATA_PATH + OBJECTS);
            while (!reader.EndOfStream)
            {
                objects.Add(reader.ReadLine());
            }
            reader.Close();

            reader = new StreamReader(DATA_PATH + HINTS_TOOLS);
            while (!reader.EndOfStream)
            {
                hintsTools.Add(reader.ReadLine());
            }
            reader.Close();
        }

        // create a new game with "n" rows, "m" columns, and "b" bears
        // n*m cannot be any greater than 30
        public void NewGame(int n, int m, int b)
        {
            int bearType = 1; // might refactor to enum if needed
            if (UnityEngine.Random.value <= 0.5)
            {
                bearType = 0;
            }

            towns = Shuffle(towns);
            adjectives = Shuffle(adjectives);
            persons = Shuffle(persons);
            objects = Shuffle(objects);

            List<string> _hintsTools = new List<string>();
            for (int i = 0; i < n * m; i++) { _hintsTools.Add(hintsTools[i]); }
            _hintsTools = Shuffle(_hintsTools);

            grid = new Town[n, m];
            int i = 0;
            for (int r = 0; r < n; r++)
            {
                for (int c = 0; c < m; c++)
                {
                    string townName = towns[i];
                    string adj = adjectives[i];
                    string[] personData = (persons[i]).Split('\t');
                    string identifier = genderMap[personData[1]];
                    string[] objectData = (objects[i]).Split('\t');
                    string[] hint_tool = (_hintsTools[i]).Split('\t');
                    string hintType = hint_tool[0];
                    string toolType = hint_tool[1];

                    // set person's identifier
                    if (identifier.Equals("they"))
                    {
                        if (UnityEngine.Random.value <= 0.5)
                        {
                            identifier = "he";
                        }
                        else
                        {
                            identifier = "she";
                        }
                    }

                    Checkable person = new Checkable(personData[0], identifier);
                    Checkable obj = new Checkable(objectData[0], objectData[1]);
                    
                    if (bearType == 1 && hintType.Equals("disguise"))
                    {
                        // make the person a bear
                        person = new Bear(personData[0], identifier);
                        bearType = -1;
                    }
                    else if (bearType == 0 && toolType.Equals("disguise"))
                    {
                        // make the object a bear
                        obj = new Bear(objectData[0], objectData[1]);
                        bearType = -1;
                    }

                    Tool tool = null;
                    switch(toolType) {
                        // TODO
                    }

                    switch (hintType)
                    {
                        // TODO
                    }

                    Debug.Log(adj + " " + townName + ": " + personData[0] + " (" + identifier + "), " + objectData[1] + " " + objectData[0]);

                    grid[r,c] = new Town(adj, townName, person, obj, tool);
                    i++;
                }
            }
        }


        private List<T> Shuffle<T>(List<T> list)
        {
            System.Random rng = new System.Random();
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }

            return list;
        }
    }
}
