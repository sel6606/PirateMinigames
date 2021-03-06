﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMayhemPlayer : MonoBehaviour {

    private CMayhemMiniGame game;

    public ParticleSystem particles;

    private KeyCode left;
    private KeyCode right;
    private KeyCode up;
    private KeyCode down;

    private bool isAlive;

    private float speed;

    private int lives;
    private bool isRespawning;
    private bool isInvincible;
    private float invincibleTimer;

    public bool IsAlive
    {
        get { return isAlive; }
    }

    public int Lives
    {
        get { return lives; }
        set { lives = value; }
    }

    // Use this for initialization
    void Start()
    {
        game = GameObject.Find("MiniGameManager").GetComponent<CMayhemMiniGame>();

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

        isRespawning = false;
        isInvincible = false;
        invincibleTimer = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!game.GameOver)
        {
            //Move
            ProcessInput();

            if (isRespawning && particles.isStopped)
            {
                //Mark that the player is ready to spawn (particle system stopped playing)
                isRespawning = false;

                //Respawn the player
                Respawn();
            }

            else if (!isRespawning && isInvincible)
            {
                //Decrease the invincible timer
                invincibleTimer -= Time.deltaTime;

                //Blink the player
                gameObject.GetComponent<SpriteRenderer>().enabled = !gameObject.GetComponent<SpriteRenderer>().enabled;

                if (invincibleTimer <= 0)
                {
                    //Stop making the player invincible
                    isInvincible = false;

                    //Make sure the player is visible
                    gameObject.GetComponent<SpriteRenderer>().enabled = true;

                    //Reset timer
                    invincibleTimer = 3.0f;
                }
            }
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
            if (!isInvincible)
            {
                //Subtract the player's life count
                lives--;

                //See if they can respawn
                if (lives > 0)
                {
                    //Show explosion
                    particles.Play();

                    //Make player invisible
                    gameObject.GetComponent<SpriteRenderer>().enabled = false;

                    //Mark that the player is respawning
                    isRespawning = true;

                    //Mark that the player is invincible
                    isInvincible = true;

                    //Update the UI display to show the player lives
                    game.DisplayLives();
                }

                //No more lives, end game if it hasn't ended
                else if (!game.GameOver)
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
                transform.right = new Vector3(-1, 0, 0);
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
                transform.right = new Vector3(1, 0, 0);
            }

            //Move
            transform.position += transform.right * speed * Time.deltaTime;
        }
    }

    private void Respawn()
    {
        //Respawn at the center of the background
        transform.position = new Vector3(game.background.transform.position.x, game.background.transform.position.y, 1.0f);
    }
}
