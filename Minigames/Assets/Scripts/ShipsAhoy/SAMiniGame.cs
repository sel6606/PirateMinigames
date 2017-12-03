using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SAMiniGame : MonoBehaviour {

    public GameObject instructionsScreen;
    public GameObject gameoverScreen;
    public Text results;

    public GameObject background;
    public GameObject boardCollider;
    public GameObject spawnPoint;
    public GameObject[] bounds;
    public GameObject[] water;
    public GameObject playerPrefab;
    private GameObject player;
    public GameObject[] rockPrefabs;

    private float scrollSpeed;
    private bool isScrolling;

    private bool isStarted;
    private bool gameOver;

    private float spawnTimer;

    public bool GameOver
    {
        get { return gameOver; }
    }

    public bool IsScrolling
    {
        get { return isScrolling; }
    }

    // Use this for initialization
    void Start()
    {
        scrollSpeed = 3.0f;
        isScrolling = false;

        isStarted = false;
        gameOver = false;

        spawnTimer = 0.4f;
    }

    // Update is called once per frame
    void Update()
    {
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

        //Board Collider
        boardCollider.transform.localScale = new Vector3(xDistance * 2.0f, yDistance * 2.0f, 1.0f);

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

        //Bounds
        for (int i = 0; i < bounds.Length; i++)
        {
            //Left Bounds
            if (i == 0)
            {
                bounds[i].transform.localScale = new Vector3(1.0f, yDistance * 3.0f, 1.0f);
                bounds[i].transform.position = new Vector3(background.transform.position.x + background.transform.localScale.x / 4.0f, background.transform.position.y, 1.0f);
            }

            //Right Bounds
            else
            {
                bounds[i].transform.localScale = new Vector3(1.0f, yDistance * 3.0f, 1.0f);
                bounds[i].transform.position = new Vector3(background.transform.position.x - background.transform.localScale.x / 4.0f, background.transform.position.y, 1.0f);
            }
        }

        //Spawn Point
        float maxX = bounds[0].transform.position.x - bounds[0].transform.localScale.x * 0.75f;
        float minX = bounds[1].transform.position.x + bounds[1].transform.localScale.x * 0.75f;

        spawnPoint.transform.position = new Vector3(background.transform.position.x, background.transform.position.y + yDistance * 1.3f, 1.0f);
        spawnPoint.transform.localScale = new Vector3(Mathf.Abs(maxX - minX), 0.5f, 1.0f);
    }

    private void ScrollGame()
    {
        //Scroll
        for (int i = 0; i < water.Length; i++)
        {
            water[i].transform.position += -water[i].transform.up * scrollSpeed * Time.deltaTime;
        }

        //If the last water block is scrolling off screen
        if (water[water.Length - 1].transform.position.y < 0.0f)
        {
            //Set it on screen
            water[water.Length - 1].transform.position = Vector3.zero;

            //Stop scrolling
            isScrolling = false;
        }
    }

    private void ScrollPlayer()
    {
        player.transform.position += player.transform.right * scrollSpeed * Time.deltaTime;
    }

    public void SetGameOverState(bool hasWon)
    {
        if (hasWon)
        {
            //Create a new pawn ship with 1 gold
            ShipData temp = new ShipData("NewShip", 1, false);

            //Determine which player controls the new ship and add it to their shipyard
            if (PlayerInfo.instance.Advantage == PlayerAdvantage.Player1)
            {
                PlayerInfo.instance.ShipsP1.Add(temp);
            }
            else if (PlayerInfo.instance.Advantage == PlayerAdvantage.Player2)
            {
                PlayerInfo.instance.ShipsP2.Add(temp);
            }

            results.text = "You successfully recruited a ship.";
        }
        else
        {
            results.text = "Your ship sank to the bottom of the sea.";
        }

        gameOver = true;
        gameoverScreen.SetActive(true);
    }

    private void SpawnPlayer()
    {
        //Spawn slightly below center
        player = Instantiate(playerPrefab, new Vector3(0.0f, background.transform.position.y - background.transform.localScale.y / 4.0f, 1.0f), Quaternion.Euler(new Vector3(0.0f, 0.0f, 90.0f)));

        //Set player reference to this script
        player.GetComponent<SAPlayerScript>().Game = this;
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

    public void ExitGame()
    {
        //Write code to switch to interface scene
        SceneManager.LoadScene("MainScene");
    }

    private void Play()
    {
        //Scroll the game while the game is not at the end
        if (isScrolling)
        {
            ScrollGame();

            //Decrease spawn timer for rocks
            spawnTimer -= Time.deltaTime;

            if (spawnTimer <= 0)
            {
                //Reset timer
                spawnTimer = 2.0f;

                //Spawn in rocks
                SpawnRockInterval();
            }
        }

        //Scroll the player once the game is at the end
        else
        {
            ScrollPlayer();
        }
    }

    /// <summary>
    /// Spawns the rocks in the game
    /// </summary>
    private void SpawnRocks()
    {
        int randCount = Random.Range(1, 4);

        for (int i = 0; i < randCount; i++)
        {
            //Get a random position along the spawn point's width
            float x = Random.Range(spawnPoint.transform.position.x - spawnPoint.transform.localScale.x / 2.0f, spawnPoint.transform.position.x + spawnPoint.transform.localScale.x / 2.0f);

            //Choose a random rock prefab to spawn
            int randRock = Random.Range(0, 3);

            //Spawn the rock
            //Note: Spawn at z = 1.0f because all 2D gameobjects will be moving on z = 1.0f
            GameObject rock = Instantiate(rockPrefabs[randRock], new Vector3(x, spawnPoint.transform.position.y, 1.0f), Quaternion.identity);

            //Set reference to this script
            rock.GetComponent<SARockScript>().Game = this;
        }
    }

    private void SpawnRockInterval()
    {
        float time = 1.0f;

        for (int i = 0; i < 4; i++)
        {
            Invoke("SpawnRocks", time);

            time += 0.5f;
        }
    }
}
