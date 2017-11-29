﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bill : MonoBehaviour {

    private MiniGame game;

    private float speed;

    public MiniGame Game
    {
        set { game = value; }
    }

    // Use this for initialization
    void Start()
    {
        speed = 5.0f;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Hat")
        {
            //Decrease the player's score
            game.Score -= 10;

            //Remove the coin
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Destroys the coin when it goes off screen
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Board")
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Moves coin down the screen
    /// </summary>
    private void Move()
    {
        transform.position += -transform.up * speed * Time.deltaTime;
    }
}
