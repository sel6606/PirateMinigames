﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Showdown : MonoBehaviour {

    private bool gameover;

    public Text gameOverText;
    public Text oneHealth;
    public Text twoHealth;

    public Button exit;

    public GameObject pOne;
    public GameObject pTwo;

    public GameObject playerOneSprite;
    public GameObject playerTwoSprite;
    public GameObject cannonballSprite;

	// Use this for initialization
	void Start ()
    {
        gameover = false;
        SpawnPlayers();
	}
	
	// Update is called once per frame
	void Update ()
    {
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

        //If a player has more gold than the other, give them more starting health
        if (PlayerInfo.instance.Advantage == PlayerAdvantage.Player1)
        {
            //Debug.Log("Player 1 advantage");
            pOne.GetComponent<Player>().Health = 5;
            pTwo.GetComponent<Player>().Health = 3;
        }
        else if (PlayerInfo.instance.Advantage == PlayerAdvantage.Player2)
        {
            pOne.GetComponent<Player>().Health = 3;
            pTwo.GetComponent<Player>().Health = 5;
        }
        else
        {
            pOne.GetComponent<Player>().Health = 3;
            pTwo.GetComponent<Player>().Health = 3;
        }
    }

    //ends the game once a player reaches 0 health
    public void GameOver()
    {
        if (pOne.GetComponent<Player>().Health == 0)
        {
            #region Calculate Player Two's Winnings
            int winnings = PlayerInfo.instance.ShipsP1[PlayerInfo.instance.PlayerOneShip].goldAmount;

            ShipData temp = PlayerInfo.instance.ShipsP2[PlayerInfo.instance.PlayerTwoShip];

            temp.goldAmount += winnings;

            if (temp.goldAmount > 9)
            {
                temp.goldAmount = 9;
            }

            PlayerInfo.instance.ShipsP2[PlayerInfo.instance.PlayerTwoShip] = temp;
            #endregion

            PlayerInfo.instance.ShipsP1.RemoveAt(PlayerInfo.instance.PlayerOneShip);

            gameOverText.text = "Player Two Wins!";
            Destroy(pOne);
            gameover = true;

            exit.gameObject.SetActive(true);
        }
        else if (pTwo.GetComponent<Player>().Health == 0)
        {
            #region Calculate Player One's Winnings
            int winnings = PlayerInfo.instance.ShipsP2[PlayerInfo.instance.PlayerTwoShip].goldAmount;

            ShipData temp = PlayerInfo.instance.ShipsP1[PlayerInfo.instance.PlayerOneShip];

            temp.goldAmount += winnings;

            if (temp.goldAmount > 9)
            {
                temp.goldAmount = 9;
            }

            PlayerInfo.instance.ShipsP1[PlayerInfo.instance.PlayerOneShip] = temp;
            #endregion

            PlayerInfo.instance.ShipsP2.RemoveAt(PlayerInfo.instance.PlayerTwoShip);

            gameOverText.text = "Player One Wins!";
            Destroy(pTwo);
            gameover = true;

            exit.gameObject.SetActive(true);
        }

    }

    //updates UI for each player's health
    public void UpdateHealth()
    {
        oneHealth.text = "Health: " + pOne.GetComponent<Player>().Health;
        twoHealth.text = "Health: " + pTwo.GetComponent<Player>().Health;
    }

    public void ExitGame()
    {
        SceneManager.LoadScene("MainScene");
    }
}
