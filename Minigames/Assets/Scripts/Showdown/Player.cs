using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public int health;
    public float speed;

    public int Health
    {
        get { return health; }
        set { health = value; }
    }

	// Use this for initialization
	void Start () {
        health = 3;
        speed = 4.0f;
	}
	
	// Update is called once per frame
	void Update () {
        Move();
	}

    //moves the players up and down (uncomment code to allow left and right movement)
    public void Move()
    {
        //movement for player one (left side)
        if (this.gameObject.name == "PlayerOne")
        {
            //move up
            if (Input.GetKey(KeyCode.W))
            {
                transform.eulerAngles = new Vector3(180, 0, 0);
                transform.position -= transform.up * speed * Time.deltaTime;
            }
            //move down
            if (Input.GetKey(KeyCode.S))
            {
                //transform.right = new Vector3(-1, 0, 0);
                transform.eulerAngles = Vector3.zero;
                transform.position -= transform.up * speed * Time.deltaTime;
            }
            /*move left
            if (Input.GetKey(KeyCode.A))
            {
                transform.right = new Vector3(-1, 0, 0);
                transform.position += transform.right * speed * Time.deltaTime;
            }
            //move right
            if (Input.GetKey(KeyCode.D))
            {
                transform.right = new Vector3(1, 0, 0);
                transform.position += transform.right * speed * Time.deltaTime;
            }*/
        }
        //movement for player two (right side)
        if (this.gameObject.name == "PlayerTwo")
        {
            //move up
            if (Input.GetKey(KeyCode.UpArrow))
            {
                transform.eulerAngles = new Vector3(180, 0, 0);
                transform.position -= transform.up * speed * Time.deltaTime;
            }
            //move down
            if (Input.GetKey(KeyCode.DownArrow))
            {
                transform.eulerAngles = Vector3.zero;
                transform.position -= transform.up * speed * Time.deltaTime;
            }
            /*move left
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                transform.right = new Vector3(-1, 0, 0);
                transform.position += transform.right * speed * Time.deltaTime;
            }
            //move right
            if (Input.GetKey(KeyCode.RightArrow))
            {
                transform.right = new Vector3(1, 0, 0);
                transform.position += transform.right * speed * Time.deltaTime;
            }*/
        }
    }

    //checks if the player collides with a cannonball or the border of the screen
    public void OnTriggerEnter2D(Collider2D collision)
    {
        //if the player collides with a cannonball...
        if (collision.gameObject.tag == "cannonball")
        {
            Debug.Log("collision!");
            //reduce pleyer two's health if the cannonball was fired by player one and collides with player two
            if (collision.gameObject.GetComponent<Cannonball>().direction == 1 && this.gameObject.name == "PlayerTwo")
            {
                this.health--;
                Debug.Log("player two hit!");
                Destroy(collision.gameObject);
            }
            //reduce player one's health if the cannonball was fired by player two and collides with player one
            if (collision.gameObject.GetComponent<Cannonball>().direction == 0 && this.gameObject.name == "PlayerOne")
            {
                this.health--;
                Debug.Log("player one hit!");
                Destroy(collision.gameObject);
            }
        }

        //if the player collides with the borders of the screen
    }
}
