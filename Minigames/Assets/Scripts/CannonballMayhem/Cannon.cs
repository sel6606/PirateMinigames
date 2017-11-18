using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour {

    public GameObject cannonballPrefab;

    private float startTimer;

    private float waitTimer;

    private float speed;

    private bool onScreen;

	// Use this for initialization
	void Start () {
        startTimer = Random.Range(2.0f, 6.0f);

        waitTimer = 2.0f;

        speed = 2.0f;

        onScreen = false;
	}

    //Fires cannonball when moved on screen
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Entry")
        {
            onScreen = true;
            Fire();
        }
    }

    //Destroys cannon when moved off screen
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.tag == "Board")
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update () {
        if (!onScreen)
        {
            //Wait to start moving
            if (startTimer >= 0)
            {
                startTimer -= Time.deltaTime;

                if (startTimer < 0)
                {
                    //Get a random player
                    int rand = Random.Range(1, 3);

                    //Get player's position
                    Vector3 playerPos = GameObject.FindGameObjectWithTag("Player" + rand).transform.position;

                    //Get vector to player
                    Vector3 toPlayer = playerPos - transform.position;

                    //Set right vector for movement
                    transform.right = toPlayer.normalized;
                }
            }

            //Then start moving
            else
            {
                MoveOnScreen();
            }
        }
        
        else
        {
            //Wait on the screen
            if (waitTimer >= 0)
            {
                waitTimer -= Time.deltaTime;
            }

            //Then move off screen
            else
            {
                MoveOffScreen();
            }
        }
	}

    /// <summary>
    /// Moves cannon on screen
    /// </summary>
    private void MoveOnScreen()
    {
        //Move forward
        transform.position += transform.right * speed * Time.deltaTime;
    }

    /// <summary>
    /// Moves cannon off screen
    /// </summary>
    private void MoveOffScreen()
    {
        //Move backward
        transform.position += -transform.right * speed * Time.deltaTime;
    }

    /// <summary>
    /// Fires a cannonball from the cannon
    /// </summary>
    private void Fire()
    {
        //Spawn cannonball
        GameObject cannonball = Instantiate(cannonballPrefab, transform.GetChild(0).transform.position, Quaternion.identity);

        //Set cannonball's right to the same as the cannon
        cannonball.transform.right = gameObject.transform.right;
    }
}
