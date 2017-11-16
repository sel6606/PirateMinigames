using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Showdown : MonoBehaviour {

    public GameObject instructionScreen;
    public GameObject gameScreen;
    public GameObject gameOver;

    public GameObject pOne;
    public GameObject pTwo;

	// Use this for initialization
	void Start () {
        instructionScreen.SetActive(true);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SpawnPlayers()
    {
        
    }
}
