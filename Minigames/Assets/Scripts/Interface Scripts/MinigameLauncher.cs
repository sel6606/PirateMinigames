using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


/// <summary>
/// Script that contains methods called when launching various minigames
/// </summary>
public class MinigameLauncher : MonoBehaviour
{
    public GameObject[] playerFields;


    private int p1;
    private int p2;

	// Use this for initialization
	void Start ()
    {
        p1 = -1;
        p2 = -1;
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    /// <summary>
    /// Set player 1
    /// </summary>
    /// <param name="p1">The index of player 1</param>
    public void SetP1(int p1)
    {
        this.p1 = p1;
    }

    /// <summary>
    /// Set player 2
    /// </summary>
    /// <param name="p2">The index of player 2</param>
    public void SetP2(int p2)
    {
        this.p2 = p2;
    }

    /// <summary>
    /// Launch a multiplayer minigame
    /// </summary>
    public void LaunchMultiplayerMinigame()
    {
        //Make sure both players have a ship in the corresponding minigame slots
        if(playerFields[p1].GetComponent<MinigameSelect>().IsSet && playerFields[p2].GetComponent<MinigameSelect>().IsSet)
        {
            //Get the gold on each ship
            int goldP1 = playerFields[p1].GetComponent<MinigameSelect>().GoldOnShip;
            int goldP2 = playerFields[p2].GetComponent<MinigameSelect>().GoldOnShip;

            //Set the index of each ship in the PlayerInfo singleton
            PlayerInfo.instance.PlayerOneShip = playerFields[p1].GetComponent<MinigameSelect>().ListIndex;
            PlayerInfo.instance.PlayerTwoShip = playerFields[p2].GetComponent<MinigameSelect>().ListIndex;

            //Determine which player (if any) has the advantage
            if (goldP1 > goldP2)
            {
                PlayerInfo.instance.Advantage = PlayerAdvantage.Player1;
            }
            else if(goldP2 > goldP1)
            {
                PlayerInfo.instance.Advantage = PlayerAdvantage.Player2;
            }
            else
            {
                PlayerInfo.instance.Advantage = PlayerAdvantage.None;
            }

            //Load the minigame
            SceneManager.LoadScene("Cannonball Mayhem");
        }
    }

    /// <summary>
    /// Launch a port minigame
    /// </summary>
    /// <param name="earnGold">Is this a gold earning minigame?</param>
    public void LaunchPortMinigame(bool earnGold)
    {
        //Make sure there is a ship in the corresponding slot
        if (playerFields[2].GetComponent<MinigameSelect>().IsSet)
        {
            //If it is a gold earning minigame, launch a gold earning minigame
            //Otherwise, launch a ship building minigame
            if (earnGold)
            {
                //Tell the PlayerInfo singleton the index of the ship and which player the ship belongs to
                PlayerInfo.instance.SinglePlayerShip = playerFields[2].GetComponent<MinigameSelect>().ListIndex;
                PlayerInfo.instance.Advantage = playerFields[2].GetComponent<MinigameSelect>().WhichPlayer;

                //Load the minigame
                SceneManager.LoadScene("GoldRush");
            }
            else
            {

            }
        }
    }
}
