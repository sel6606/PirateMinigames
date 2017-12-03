using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    public void SetP1(int p1)
    {
        this.p1 = p1;
    }

    public void SetP2(int p2)
    {
        this.p2 = p2;
    }

    public void LaunchMultiplayerMinigame()
    {
        if(playerFields[p1].GetComponent<MinigameSelect>().IsSet && playerFields[p2].GetComponent<MinigameSelect>().IsSet)
        {
            int goldP1 = playerFields[p1].GetComponent<MinigameSelect>().GoldOnShip;
            int goldP2 = playerFields[p2].GetComponent<MinigameSelect>().GoldOnShip;

            if(goldP1 > goldP2)
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

            Debug.Log(PlayerInfo.instance.Advantage);
            SceneManager.LoadScene("Cannonball Mayhem");
        }
    }

    public void LaunchPortMinigame(bool earnGold)
    {
        if (playerFields[2].GetComponent<MinigameSelect>().IsSet)
        {
            if (earnGold)
            {
                SceneManager.LoadScene("GoldRush");
            }
            else
            {

            }
        }
    }
}
