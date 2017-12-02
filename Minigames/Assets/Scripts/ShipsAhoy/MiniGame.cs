using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniGame : MonoBehaviour {

    public GameObject instructionsScreen;
    public GameObject gameoverScreen;

    public GameObject background;
    public GameObject[] water;
    public GameObject playerPrefab;
    private GameObject player;

    private float scrollSpeed;
    private bool isScrolling;

    private bool isStarted;
    private bool gameOver;

	// Use this for initialization
	void Start () {
        scrollSpeed = 3.0f;
        isScrolling = false;

        isStarted = false;
        gameOver = false;
	}
	
	// Update is called once per frame
	void Update () {
        SetBackgroundToScreen();

        if (isStarted && !gameOver)
        {
            Play();
        }
	}

    private void SetBackgroundToScreen()
    {
        /* 
            This only works if the camera is orthographic and the x and y of the
            background position is set to 0.
            In Commented Useful Code, SetBackgroundToScreen2() works for all cases.
        */

        //Precheck to avoid setting background to screen over and over
        Vector3 backgroundToScreen = Camera.main.WorldToScreenPoint(new Vector3(-background.transform.localScale.x / 2.0f, background.transform.localScale.y / 2.0f, 0.0f));
        if (backgroundToScreen.x == Screen.width && backgroundToScreen.y == Screen.height) //background already set to screen
        {
            return;
        }

        //Get screen width and height
        float width = Screen.width;
        float height = Screen.height;

        //Convert to world coordinates
        Vector3 screenToWorld = Camera.main.ScreenToWorldPoint(new Vector3(width, height, 0.0f));

        //Get distance from background's center to the world coordinates
        float xDistance = Mathf.Abs(background.transform.position.x - screenToWorld.x); //halfwidth.x
        float yDistance = Mathf.Abs(background.transform.position.y - screenToWorld.y); //halfwidth.y

        //Scale the game by this distance
        //Background
        background.transform.localScale = new Vector3(xDistance * 2.0f, yDistance * 2.0f, background.transform.localScale.z);

        //Water
        for (int i = 0; i < water.Length; i++)
        {
            water[i].transform.localScale = background.transform.localScale;

            //Reposition water
            if (i > 0)
            {
                water[i].transform.position = new Vector3(0.0f, water[i - 1].transform.position.y + water[i].transform.localScale.y, 0.0f);
            }
        }
    }

    private void ScrollGame()
    {
        //If the last water block is scrolling off screen
        if (water[water.Length - 1].transform.position.y < 0.0f)
        {
            //Set it on screen
            water[water.Length - 1].transform.position = Vector3.zero;

            //Stop scrolling
            isScrolling = false;
        }

        //Scroll
        for (int i = 0; i < water.Length; i++)
        {
            water[i].transform.position += -water[i].transform.up * scrollSpeed * Time.deltaTime;
        }
    }

    private void ScrollPlayer()
    {
        player.transform.position += player.transform.up * scrollSpeed * Time.deltaTime;
    }

    public void SetGameOverState()
    {
        gameOver = true;
        gameoverScreen.SetActive(true);
    }

    private void SpawnPlayer()
    {
        //Spawn slightly below center
        player = Instantiate(playerPrefab, new Vector3(0.0f, background.transform.position.y - background.transform.localScale.y / 4.0f, 1.0f), Quaternion.Euler(new Vector3(0.0f, 0.0f, 90.0f)));

        //Set player reference to this script
        player.GetComponent<Player>().Game = this;
    }

    public void StartGame()
    {
        //Mark that the game started
        isStarted = true;

        //Hide the instructions screen
        instructionsScreen.SetActive(false);

        //Create the player
        SpawnPlayer();

        //Start scrolling the game
        isScrolling = true;
    }

    private void Play()
    {
        //Scroll the game while the game is not at the end
        if (isScrolling)
        {
            ScrollGame();
        }

        //Scroll the player once the game is at the end
        else
        {
            ScrollPlayer();
        }
    }
}
