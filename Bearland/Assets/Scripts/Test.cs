using UnityEngine;
using System.Collections;

using Assets.Scripts;

public class Test : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GameManager.Instance.NewGame(3, 3, 1);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
