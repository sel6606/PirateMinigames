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

        if (gameObject.tag == "Player1")
        {
            left = KeyCode.A;
            right = KeyCode.D;
            up = KeyCode.W;
            down = KeyCode.S;
        }
        else
        {
            left = KeyCode.LeftArrow;
            right = KeyCode.RightArrow;
            up = KeyCode.UpArrow;
            down = KeyCode.DownArrow;
        }

        isAlive = true;

        speed = 6.0f;
	}
	
	// Update is called once per frame
	void Update () {
        if (!game.GameOver)
        {
            ProcessInput();
        }
	}

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
    

    private void ProcessInput()
    {
        if (Input.GetKey(up))
        {
            if (transform.right.y < 0.9)
            {
                transform.right = transform.up;
            }

            transform.position += transform.right * speed * Time.deltaTime;
        }

        else if (Input.GetKey(down))
        {
            if (transform.right.y > -0.9)
            {
                transform.right = -transform.up;
            }

            transform.position += transform.right * speed * Time.deltaTime;
        }

        else if (Input.GetKey(right))
        {
            if (transform.right.x > -0.9)
            {
                transform.right = new Vector3(-1,0,0);
            }

            transform.position += transform.right * speed * Time.deltaTime;
        }

        else if (Input.GetKey(left))
        {
            if (transform.right.x < 0.9)
            {
                transform.right = new Vector3(1,0,0);
            }

            transform.position += transform.right * speed * Time.deltaTime;
        }
    }
}
