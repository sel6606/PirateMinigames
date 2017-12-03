using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Showdown : MonoBehaviour {

    private bool gameover;

    public Text gameOverText;
    public Text oneHealth;
    public Text twoHealth;

    public GameObject pOne;
    public GameObject pTwo;

    public GameObject playerOneSprite;
    public GameObject playerTwoSprite;
    public GameObject cannonballSprite;

	// Use this for initialization
	void Start () {
        gameover = false;
        SpawnPlayers();
	}
	
	// Update is called once per frame
	void Update () {
        if (gameover != true)
        {
            UpdateHealth();
            GameOver();
        }
    }

    //spawns players on-screen
    public void SpawnPlayers()
    {
        pOne = Instantiate(playerOneSprite, new Vector3(-5, 0, 0), Quaternion.identity);
        pOne.name = "PlayerOne";
        pOne.AddComponent<Player>();
        pTwo = Instantiate(playerTwoSprite, new Vector3(5, 0, 0), Quaternion.identity);
        pTwo.name = "PlayerTwo";
        pTwo.AddComponent<Player>();
        pTwo.transform.eulerAngles = new Vector3(180, 0, 0);
    }

    //ends the game once a player reaches 0 health
    public void GameOver()
    {
        if (pOne.GetComponent<Player>().Health == 0)
        {
            gameOverText.text = "Player Two Wins!";
            Destroy(pOne);
            gameover = true;
        }
        else if (pTwo.GetComponent<Player>().Health == 0)
        {
            gameOverText.text = "Player One Wins!";
            Destroy(pTwo);
            gameover = true;
        }

    }

    //updates UI for each player's health
    public void UpdateHealth()
    {
        oneHealth.text = "Health: " + pOne.GetComponent<Player>().Health;
        twoHealth.text = "Health: " + pTwo.GetComponent<Player>().Health;
    }
}
