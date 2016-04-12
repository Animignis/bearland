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

        private List<string> towns;
        private List<string> adjectives;
        private List<string> persons;
        private List<string> objects;

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
        }

        // create a new game with "n" rows, "m" columns, and "b" bears
        public void NewGame(int n, int m, int b)
        {
            List<string> _towns = new List<string>();
            List<string> _adjs = new List<string>();
            List<string> _persons = new List<string>();
            List<string> _objs = new List<string>();
            for (int t = 0; t < towns.Count; t++) { _towns.Add(towns[t]); }
            for (int t = 0; t < adjectives.Count; t++) { _adjs.Add(adjectives[t]); }
            for (int t = 0; t < persons.Count; t++) { _persons.Add(persons[t]); }
            for (int t = 0; t < objects.Count; t++) { _objs.Add(objects[t]); }

            grid = new Town[n, m];
            int i = 0;
            for (int r = 0; r < n; r++)
            {
                for (int c = 0; c < m; c++)
                {
                    int index = UnityEngine.Random.Range(0, _towns.Count);
                    string townName = _towns[index];
                    _towns.RemoveAt(index);

                    index = UnityEngine.Random.Range(0, _adjs.Count);
                    string adj = _adjs[index];
                    _adjs.RemoveAt(index);

                    index = UnityEngine.Random.Range(0, _persons.Count);
                    string[] personData = (_persons[index]).Split('\t');
                    _persons.RemoveAt(index);

                    string gender = genderMap[personData[1]];
                    if (gender.Equals("they"))
                    {
                        if (UnityEngine.Random.value <= 0.5)
                        {
                            gender = "he";
                        }
                        else
                        {
                            gender = "she";
                        }
                    }

                    index = UnityEngine.Random.Range(0, _objs.Count);
                    string[] objectData = (_objs[index]).Split('\t');
                    _objs.RemoveAt(index);

                    Checkable person = new Checkable(personData[0], gender);
                    Checkable obj = new Checkable(objectData[0], objectData[1]);
                    Tool tool = null;

                    Debug.Log(adj + " " + townName + ": " + personData[0] + " (" + gender +"), " + objectData[1] + " " + objectData[0]);

                    grid[r,c] = new Town(adj, townName, person, obj, tool);
                    i++;
                }
            }
        }

    }
}
