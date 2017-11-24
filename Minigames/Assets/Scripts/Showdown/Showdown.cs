using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Showdown : MonoBehaviour {

    public GameObject instructionScreen;
    public GameObject gameScreen;
    public GameObject gameOver;

    private bool gameover;

    public Text gameOverText;
    public GameObject pOne;
    public GameObject pTwo;

    public GameObject playerOneSprite;
    public GameObject playerTwoSprite;
    public GameObject cannonballSprite;

	// Use this for initialization
	void Start () {
        //instructionScreen.SetActive(false);
        //gameOver.SetActive(false);
        //gameScreen.SetActive(true);
        gameover = false;
        SpawnPlayers();
        
	}
	
	// Update is called once per frame
	void Update () {
        if (gameover != true)
        {
            SpawnCannonballs();
            CheckHealth();
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
    }

    //ends the game once a player reaches 0 health
    public void GameOver()
    {
        if (pOne.GetComponent<Player>().Health == 0)
        {
            gameOverText.text = "Player Two Wins!";
            gameover = true;
        }
        else if (pTwo.GetComponent<Player>().Health == 0)
        {
            gameOverText.text = "Player One Wins!";
            gameover = true;
        }
        //gameOverText = Instantiate(gameOverText, new Vector3(0, 2, 0), Quaternion.identity);
    }

    //spawns cannonballs on-screen
    public void SpawnCannonballs()
    {
        //spawn a cannonball if player one is pressing space
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject ball = Instantiate(cannonballSprite, pOne.transform.position, Quaternion.identity);
            ball.AddComponent<Cannonball>();
            ball.GetComponent<Cannonball>().direction = 1;
        }
        //spawn a cannonball if player two is pressing enter on the keypad
        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            GameObject ball = Instantiate(cannonballSprite, pTwo.transform.position, Quaternion.identity);
            ball.AddComponent<Cannonball>();
            ball.GetComponent<Cannonball>().direction = 0;
        }
    }
    public void CheckHealth()
    {
        
    }
}
