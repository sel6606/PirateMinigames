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

    public GameObject playerSprite;

	// Use this for initialization
	void Start () {
        //instructionScreen.SetActive(false);
        //gameOver.SetActive(false);
        //gameScreen.SetActive(true);
        SpawnPlayers();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SpawnPlayers()
    {
        pOne = Instantiate(playerSprite, new Vector3(1, 0, 0), Quaternion.identity);
        pOne.name = "PlayerOne";
        pOne.AddComponent<Player>();
        pTwo = Instantiate(playerSprite, new Vector3(-1, 0, 0), Quaternion.identity);
        pTwo.name = "PlayerTwo";
        pTwo.AddComponent<Player>();
    }
}
