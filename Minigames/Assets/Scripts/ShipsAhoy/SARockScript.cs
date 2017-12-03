using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SARockScript : MonoBehaviour {

    private SAMiniGame game;

    private float speed;

    private bool onScreen;

    public SAMiniGame Game
    {
        get { return game; }
        set { game = value; }
    }

    // Use this for initialization
    void Start()
    {
        speed = 5.0f;

        onScreen = false;

        if (game != null && !game.IsScrolling)
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (game.IsScrolling)
        {
            Move();
        }

        //Destroy rock if the player is scrolling and if
        //it is not already on screen
        if (!onScreen && !game.IsScrolling)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Board Collider")
        {
            onScreen = true;
        }
    }

    /// <summary>
    /// Destroys the rock when it goes off screen
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Board Collider")
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
