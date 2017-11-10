using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniGame : MonoBehaviour {

    //UI Variables
    public GameObject startScreen;
    public GameObject gamePlayScreen;
    public GameObject gameOverScreen;
    public Text timer;
    public Text winner;

    //GameObjects in scene
    public GameObject cannonPrefab;
    public GameObject[] spawnPoints;
    public GameObject[] playerPrefabs;
    public GameObject[] players;

    //Duration of the game
    private float playTimer;

    //Spawn intervals of cannons
    private float spawnTime;
    private float spawnTimer;

    //Numbers of cannons spawning
    private int numCannons;

    //Gamestates
    private bool isStarted;
    private bool gameOver;

    #region Properties
    public bool GameOver
    {
        get { return gameOver; }
        set { gameOver = value; }
    }

    public float PlayTimer
    {
        get { return playTimer; }
    }
    #endregion

    // Use this for initialization
    void Start () {
        players = new GameObject[2];

        playTimer = 30.0f;

        spawnTime = 4.0f;
        spawnTimer = spawnTime;

        numCannons = 2;

        isStarted = false;
        gameOver = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (isStarted && !gameOver)
        {
            Play();
        }
	}

    public void StartGame()
    {
        //Mark that the game has started
        isStarted = true;

        //Switch to the game play game state
        ChangeGameState();

        //Spawn in the players
        SpawnPlayers();

        //Spawn in the cannons
        SpawnCannons();
    }

    private void Play()
    {
        //Decrease minigame timer
        DecreaseTimer();

        //Display the minigame timer
        DisplayTimer();

        //Decrease time to spawn cannon
        spawnTimer -= Time.deltaTime;

        if (spawnTimer <= 0)
        {
            //Reset spawn timer
            spawnTimer = spawnTime;

            //Spawn cannons
            SpawnCannons();

            //Increase number of cannons spawning
            numCannons += 2;
        }
    }

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

    public void DisplayWinner()
    {
        for (int i = 0; i < players.Length; i++)
        {
            //If player is not alive
            if (!players[i].GetComponent<Player>().IsAlive)
            {
                //Player 2 died
                if (i == players.Length - 1)
                {
                    winner.text = "Player 1 is the winner!";
                }

                //Player 1 died
                else
                {
                    winner.text = "Player 2 is the winner!";
                }

                return;
            }
        }

        winner.text = "It's a Tie!";
    }

    public void DisplayTimer()
    {
        timer.text = playTimer.ToString("f1") + "s";
    }

    private void DecreaseTimer()
    {
        //Decrease minigame timer
        playTimer -= Time.deltaTime;

        if (playTimer <= 0)
        {
            //Mark that the game ended
            gameOver = true;

            //Switch to the game over game state
            ChangeGameState();

            //Set the winner of the game
            DisplayWinner();
        }
    }

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

            //Add player to array of players in the scene
            players[i] = player;
        }

    }

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

            //Get a random index in the players array
            //int randIndex = Random.Range(0, 2);

            //Set vector to player
            //Vector3 toPlayer = players[randIndex].transform.position - pos;

            //Create cannon
            Instantiate(cannonPrefab, pos, Quaternion.identity);
            //GameObject cannon = Instantiate(cannonPrefab, pos, Quaternion.identity);

            //Set the right in the direction of the player (movement is with right vector, not forward vector)
            //cannon.transform.right = toPlayer.normalized;
        }
    }
}
