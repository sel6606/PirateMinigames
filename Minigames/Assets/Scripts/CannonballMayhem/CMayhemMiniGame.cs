using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum PlayerAdvantage
{
    Player1,
    Player2,
    None
}

public class CMayhemMiniGame : MonoBehaviour {

    //Who has the gold adavantage
    private PlayerAdvantage playerAdvantage;
    
    //UI Variables
    public GameObject startScreen;
    public GameObject gamePlayScreen;
    public GameObject gameOverScreen;
    public Text winner;
    public Text p1Lives;
    public Text p2Lives;

    //GameObjects in scene
    public GameObject background;
    public GameObject boardCollider;
    public GameObject[] bounds;

    public GameObject cannonPrefab;
    public GameObject entryPoint;
    public GameObject[] spawnPoints;
    public GameObject[] playerPrefabs;
    public GameObject[] players;

    //Spawn intervals of cannons
    private float spawnTime;
    private float spawnTimer;

    //Numbers of cannons spawning
    private int numCannons;
    private int maxCannons;

    //Gamestates
    private bool isStarted;
    private bool gameOver;

    #region Properties
    public bool GameOver
    {
        get { return gameOver; }
        set { gameOver = value; }
    }

    public PlayerAdvantage PlayerAdvantage
    {
        get { return playerAdvantage; }
        set { playerAdvantage = value; }
    }

    #endregion

    // Use this for initialization
    void Start()
    {
        //This will need to reference the singleton
        //I'm setting it manually here for testing
        playerAdvantage = PlayerInfo.instance.Advantage;



        players = new GameObject[2];

        spawnTime = 4.0f;
        spawnTimer = spawnTime;

        numCannons = 2;
        maxCannons = 24;

        isStarted = false;
        gameOver = false;
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

    /// <summary>
    /// Scales the background to fit the screen
    /// </summary>
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
        background.transform.localScale = new Vector3(xDistance * 2.0f, yDistance * 2.0f, background.transform.localScale.z);
        boardCollider.transform.localScale = new Vector3(xDistance * 2.0f, yDistance * 2.0f, boardCollider.transform.localScale.z);
        entryPoint.transform.localScale = new Vector3(xDistance * 2.0f, yDistance * 2.0f, entryPoint.transform.localScale.z);

        //Spawn points
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            if (spawnPoints[i].tag == "Vertical")
            {
                spawnPoints[i].transform.localScale = new Vector3(1.0f, yDistance * 2.0f, 1.0f);

                if (spawnPoints[i].name == "Spawn (Right)")
                {
                    spawnPoints[i].transform.position = new Vector3(-xDistance - 3.0f, 0.0f, 1.0f);
                }
                else
                {
                    spawnPoints[i].transform.position = new Vector3(xDistance + 3.0f, 0.0f, 1.0f);
                }
            }
            else
            {
                spawnPoints[i].transform.localScale = new Vector3(xDistance * 2.0f, 1.0f, 1.0f);

                if (spawnPoints[i].name == "Spawn (Top)")
                {
                    spawnPoints[i].transform.position = new Vector3(0.0f, yDistance + 3.0f, 1.0f);
                }
                else
                {
                    spawnPoints[i].transform.position = new Vector3(0.0f, -yDistance - 3.0f, 1.0f);
                }
            }
        }

        //Resizes and repositions bounds based on the screen size
        for (int i = 0; i < bounds.Length; i++)
        {
            //Right bound
            if (bounds[i].name == "Right Bound")
            {
                bounds[i].transform.localScale = new Vector3(0.5f, yDistance * 2.0f, 1.0f);
                bounds[i].transform.position = new Vector3(xDistance, 0.0f, 1.0f);
            }

            //Left bound
            else if (bounds[i].name == "Left Bound")
            {
                bounds[i].transform.localScale = new Vector3(0.5f, yDistance * 2.0f, 1.0f);
                bounds[i].transform.position = new Vector3(-xDistance, 0.0f, 1.0f);
            }

            //Top bound
            else if (bounds[i].name == "Top Bound")
            {
                bounds[i].transform.localScale = new Vector3(xDistance * 2.0f, 0.5f, 1.0f);
                bounds[i].transform.position = new Vector3(0.0f, yDistance, 1.0f);
            }

            //Bottom bound
            else
            {
                bounds[i].transform.localScale = new Vector3(xDistance * 2.0f, 0.5f, 1.0f);
                bounds[i].transform.position = new Vector3(0.0f, -yDistance, 1.0f);
            }
        }

        //Put player back on board if they're off the board
        for (int i = 0; i < players.Length; i++)
        {
            if (players[i] != null)
            {
                if (Mathf.Abs(players[i].transform.position.x) > xDistance ||
                Mathf.Abs(players[i].transform.position.y) > yDistance)
                {
                    players[i].transform.position = new Vector3(0.0f, 0.0f, 1.0f);
                }
            }
        }
    }

    /// <summary>
    /// Called when play button is clicked to start the game
    /// </summary>
    public void StartGame()
    {
        //Mark that the game has started
        isStarted = true;

        //Spawn in the players
        SpawnPlayers();

        //Spawn in the cannons
        SpawnCannons();

        //Switch to the game play game state
        ChangeGameState();

        //Display the player lives in the UI
        DisplayLives();
    }

    public void ExitGame()
    {
        //Write code to return to interface scene
        SceneManager.LoadScene("MainScene");
    }

    /// <summary>
    /// Runs the game
    /// </summary>
    private void Play()
    {
        //Decrease time to spawn cannon
        spawnTimer -= Time.deltaTime;

        if (spawnTimer <= 0)
        {
            //Reset spawn timer
            spawnTimer = spawnTime;

            //Spawn cannons
            SpawnCannons();

            //Increase number of cannons spawning
            numCannons += 4;

            //Clamp numCannons
            if (numCannons >= maxCannons)
            {
                numCannons = maxCannons;
            }
        }
    }

    /// <summary>
    /// Handles switching between game states (UI)
    /// </summary>
    public void ChangeGameState()
    {
        if (isStarted && !gameOver)
        {
            //Hide instructions screen
            startScreen.SetActive(false);

            //Show gameplay screen
            gamePlayScreen.SetActive(true);
        }
        else
        {
            //Hide gameplay screen
            gamePlayScreen.SetActive(false);

            //Show gameover screen
            gameOverScreen.SetActive(true);
        }
    }

    public void DisplayLives()
    {
        for (int i = 0; i < 2; i++)
        {
            //Display player 1's lives
            if (i == 0)
            {
                p1Lives.text = "P1 Lives: " + players[i].GetComponent<CMayhemPlayer>().Lives;
            }

            //Display player 2's lives
            else
            {
                p2Lives.text = "P2 Lives: " + players[i].GetComponent<CMayhemPlayer>().Lives;
            }
        }
    }

    /// <summary>
    /// Displays the winner when the game is over
    /// </summary>
    public void DisplayWinner()
    {
        for (int i = 0; i < players.Length; i++)
        {
            //If player is not alive
            if (!players[i].GetComponent<CMayhemPlayer>().IsAlive)
            {
                //Player 2 died
                if (i == players.Length - 1)
                {
                    #region Calculate Player One's Winnings
                    int winnings = PlayerInfo.instance.ShipsP2[PlayerInfo.instance.PlayerTwoShip].goldAmount;

                    ShipData temp = PlayerInfo.instance.ShipsP1[PlayerInfo.instance.PlayerOneShip];

                    temp.goldAmount += winnings;

                    if(temp.goldAmount > 9)
                    {
                        temp.goldAmount = 9;
                    }

                    PlayerInfo.instance.ShipsP1[PlayerInfo.instance.PlayerOneShip] = temp;
                    #endregion

                    PlayerInfo.instance.ShipsP2.RemoveAt(PlayerInfo.instance.PlayerTwoShip);
                    winner.text = "Player 1 is the winner!";
                }

                //Player 1 died
                else
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
                    winner.text = "Player 2 is the winner!";
                }

                return;
            }
        }

        winner.text = "It's a Tie!";
    }

    /// <summary>
    /// Spawns both of the players in the scene
    /// </summary>
    private void SpawnPlayers()
    {
        //Get the collider that represents the bounds that players can spawn in
        GameObject bounds = GameObject.FindGameObjectWithTag("Entry");

        //Create 2 players
        for (int i = 0; i < 2; i++)
        {
            //Variables to hold position
            float x = 0;
            float y = 0;
            float z = 1;

            //Get a random x and y position for the player
            x = Random.Range(bounds.transform.position.x - bounds.transform.localScale.x / 4.0f, bounds.transform.position.x + bounds.transform.localScale.x / 4.0f);
            y = Random.Range(bounds.transform.position.y - bounds.transform.localScale.y / 4.0f, bounds.transform.position.y + bounds.transform.localScale.y / 4.0f);

            //Spawn the player
            GameObject player = Instantiate(playerPrefabs[i], new Vector3(x, y, z), Quaternion.identity);

            //Check if player has an advantage
            if (i == 0 && playerAdvantage == PlayerAdvantage.Player1)
            {
                player.GetComponent<CMayhemPlayer>().Lives = 2;
            }
            else if (i == 1 && playerAdvantage == PlayerAdvantage.Player2)
            {
                player.GetComponent<CMayhemPlayer>().Lives = 2;
            }
            else
            {
                player.GetComponent<CMayhemPlayer>().Lives = 1;
            }

            //Add player to array of players in the scene
            players[i] = player;
        }

    }

    /// <summary>
    /// Spawns cannons in the scene.
    /// </summary>
    private void SpawnCannons()
    {
        for (int i = 0; i < numCannons; i++)
        {
            //Choose random edge to spawn at (left, right, top, bottom)
            int randEdge = Random.Range(0, 4);

            //Variables to hold position
            float x = 0;
            float y = 0;
            float z = 1;

            //Left and Right spawn points
            if (spawnPoints[randEdge].tag == "Vertical")
            {
                GameObject spawnPoint = spawnPoints[randEdge];
                float minY = spawnPoint.transform.position.y - spawnPoint.transform.localScale.y / 2.0f;
                float maxY = spawnPoint.transform.position.y + spawnPoint.transform.localScale.y / 2.0f;

                x = spawnPoint.transform.position.x;
                y = Random.Range(minY, maxY);
            }

            //Top and Bottom spawn points
            else
            {
                GameObject spawnPoint = spawnPoints[randEdge];
                float minX = spawnPoint.transform.position.x - spawnPoint.transform.localScale.x / 2.0f;
                float maxX = spawnPoint.transform.position.x + spawnPoint.transform.localScale.x / 2.0f;

                x = Random.Range(minX, maxX);
                y = spawnPoint.transform.position.y;
            }

            //Set position vector
            Vector3 pos = new Vector3(x, y, z);

            //Create cannon
            Instantiate(cannonPrefab, pos, Quaternion.identity);
        }
    }
}
