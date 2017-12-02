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
            SceneManager.LoadScene("Cannonball Mayhem");
        }
    }
}
