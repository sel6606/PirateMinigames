using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGame : MonoBehaviour {

    public GameObject board;
    public GameObject cannonPrefab;

    public GameObject player1;
    public GameObject player2;

    public float maxTime;
    public float timer;

    public float spawnTime;
    public float spawnTimer;

    public int numCannons;

    public bool gameOver;

	// Use this for initialization
	void Start () {
        maxTime = 30.0f;
        timer = maxTime;

        spawnTime = 4.0f;
        spawnTimer = spawnTime;

        numCannons = 1;

        gameOver = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (!gameOver)
        {
            Play();
        }
	}

    private void DecreaseTimer()
    {
        timer -= Time.deltaTime;
    }

    private void SpawnCannon()
    {
        for (int i = 0; i < numCannons; i++)
        {
            //Choose random edge to spawn at (left, right, up, down)
            float randEdge = Random.Range(1, 5);

            //Variables to hold position
            float offset = 3.0f;
            float x = 0;
            float y = 0;
            float z = 1;

            //Up
            if (randEdge == 1)
            {
                x = Random.Range(-board.transform.localScale.x / 2, board.transform.localScale.x / 2);
                y = board.transform.localScale.y / 2 + offset;
            }

            //Down
            else if (randEdge == 2)
            {
                x = Random.Range(-board.transform.localScale.x / 2, board.transform.localScale.x / 2);
                y = -board.transform.localScale.y / 2 - offset;
            }

            //Right
            else if (randEdge == 3)
            {
                x = board.transform.localScale.x / 2 + offset;
                y = Random.Range(-board.transform.localScale.y / 2, board.transform.localScale.y / 2);
            }

            //Left
            else
            {
                x = -board.transform.localScale.x / 2 - offset;
                y = Random.Range(-board.transform.localScale.y / 2, board.transform.localScale.y / 2);
            }

            //Set position vector
            Vector3 pos = new Vector3(x, y, z);

            //Variables to get a vector to a random player
            Vector3 toPlayer = Vector3.zero;
            int rand = Random.Range(0, 2);

            //Get a direction vector to a random player
            if (rand == 0)
            {
                toPlayer = player1.transform.position - pos;
            }
            else
            {
                toPlayer = player2.transform.position - pos;
            }

            //Normalize the vector
            toPlayer.Normalize();

            //Create cannon
            GameObject cannon = Instantiate(cannonPrefab, pos, Quaternion.identity);//Quaternion.Euler(Vector3.zero));

            //Set the right in the direction of the player
            cannon.transform.right = toPlayer;
        }
    }

    private void Play()
    {
        DecreaseTimer();

        if (spawnTimer > 0)
        {
            spawnTimer -= Time.deltaTime;
        }

        if (spawnTimer < 0)
        {
            //Reset spawn timer
            spawnTimer = 0;

            //Spawn cannons
            SpawnCannon();

            Debug.Log("Spawned");
        }
    }
}
