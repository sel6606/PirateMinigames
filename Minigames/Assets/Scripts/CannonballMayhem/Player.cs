using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    private MiniGame game;

    public ParticleSystem particles;

    private KeyCode left;
    private KeyCode right;
    private KeyCode up;
    private KeyCode down;

    private bool isAlive;

    private float speed;

    public bool IsAlive
    {
        get { return isAlive; }
    }

	// Use this for initialization
	void Start () {
        game = GameObject.Find("MiniGameManager").GetComponent<MiniGame>();

        //Set Player1 controls
        if (gameObject.tag == "Player1")
        {
            left = KeyCode.A;
            right = KeyCode.D;
            up = KeyCode.W;
            down = KeyCode.S;
        }

        //Set Player2 controls
        else
        {
            left = KeyCode.LeftArrow;
            right = KeyCode.RightArrow;
            up = KeyCode.UpArrow;
            down = KeyCode.DownArrow;
        }

        isAlive = true;

        speed = 2.5f;
	}
	
	// Update is called once per frame
	void Update () {
        if (!game.GameOver)
        {
            ProcessInput();
        }
	}

    /// <summary>
    /// Removes player and transitions to game over when
    /// collided with a cannonball
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Cannonball")
        {
            if (!game.GameOver)
            {
                //Mark that the game is over
                game.GameOver = true;

                //Show explosion
                particles.Play();

                //Mark that the player lost
                isAlive = false;

                //Make player invisible
                gameObject.GetComponent<SpriteRenderer>().enabled = false;

                //Switch to the game over game state
                game.ChangeGameState();

                //Set the winner of the game
                game.DisplayWinner();
            }
        }
    }
    
    /// <summary>
    /// Handles player input for movement.
    /// </summary>
    private void ProcessInput()
    {
        //Up
        if (Input.GetKey(up))
        {
            //Change vector for movement
            if (transform.right.y < 0.9)
            {
                transform.right = transform.up;
            }

            //Move
            transform.position += transform.right * speed * Time.deltaTime;
        }

        //Down
        else if (Input.GetKey(down))
        {
            //Change vector for movement
            if (transform.right.y > -0.9)
            {
                transform.right = -transform.up;
            }

            //Move
            transform.position += transform.right * speed * Time.deltaTime;
        }

        //Right
        else if (Input.GetKey(right))
        {
            //Change vector for movement
            if (transform.right.x > -0.9)
            {
                transform.right = new Vector3(-1,0,0);
            }

            //Move
            transform.position += transform.right * speed * Time.deltaTime;
        }

        //Left
        else if (Input.GetKey(left))
        {
            //Change vector for movement
            if (transform.right.x < 0.9)
            {
                transform.right = new Vector3(1,0,0);
            }

            //Move
            transform.position += transform.right * speed * Time.deltaTime;
        }
    }
}
