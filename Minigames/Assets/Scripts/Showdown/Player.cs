using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public int health;
    public float speed;
    public Showdown game;

    public int Health
    {
        get { return health; }
        set { health = value; }
    }

	// Use this for initialization
	void Start () {
        game = GameObject.Find("GameManager").GetComponent<Showdown>();
        health = 3;
        speed = 4.0f;
	}
	
	// Update is called once per frame
	void Update () {
        if (health > 0)
        {
            Move();
            SpawnCannonballs();
            CheckScreenBounds();
        }
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

    //spawns cannonballs on-screen
    public void SpawnCannonballs()
    {
        //spawn a cannonball on the right if player one is pressing 'E'
        if (Input.GetKeyDown(KeyCode.E) && this.gameObject.name == "PlayerOne")
        {
            GameObject ball = Instantiate(game.cannonballSprite, transform.position, Quaternion.identity);
            ball.AddComponent<Cannonball>();
            ball.GetComponent<Cannonball>().owner = 1;
            ball.transform.right = this.transform.right;
        }
        //spawn a cannonball on the left if player one is pressing 'Q'
        if (Input.GetKeyDown(KeyCode.Q) && this.gameObject.name == "PlayerOne")
        {
            GameObject ball = Instantiate(game.cannonballSprite, transform.position, Quaternion.identity);
            ball.AddComponent<Cannonball>();
            ball.GetComponent<Cannonball>().owner = 1;
            ball.transform.right = -(this.transform.right);
        }
        //spawn a cannonball on the right if player two is pressing '3' on the keypad
        if (Input.GetKeyDown(KeyCode.Keypad3) && this.gameObject.name == "PlayerTwo")
        {
            GameObject ball = Instantiate(game.cannonballSprite, transform.position, Quaternion.identity);
            ball.AddComponent<Cannonball>();
            ball.GetComponent<Cannonball>().owner = 2;
            ball.transform.right = this.transform.right;
        }
        //spawn a cannonball on the left if player two is pressing '1' on the keypad
        if (Input.GetKeyDown(KeyCode.Keypad1) && this.gameObject.name == "PlayerTwo")
        {
            GameObject ball = Instantiate(game.cannonballSprite, transform.position, Quaternion.identity);
            ball.AddComponent<Cannonball>();
            ball.GetComponent<Cannonball>().owner = 2;
            ball.transform.right = -(this.transform.right);
        }
    }

    //checks if the player collides with a cannonball or the border of the screen
    public void OnTriggerEnter2D(Collider2D collision)
    {
        //if the player collides with a cannonball...
        if (collision.gameObject.tag == "cannonball")
        {
            //reduce pleyer two's health if the cannonball was fired by player one and collides with player two
            if (collision.gameObject.GetComponent<Cannonball>().owner == 1 && this.gameObject.name == "PlayerTwo")
            {
                this.health--;
                Destroy(collision.gameObject);
            }
            //reduce player one's health if the cannonball was fired by player two and collides with player one
            if (collision.gameObject.GetComponent<Cannonball>().owner == 2 && this.gameObject.name == "PlayerOne")
            {
                this.health--;
                Destroy(collision.gameObject);
            }
        }
    }

    public void CheckScreenBounds()
    {
        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
        if (pos.y < 0.0)
        {
            this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x, pos.y +5f, this.gameObject.transform.position.z);
        }
        if (1.0 < pos.y)
        {
            this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x, pos.y -4f, this.gameObject.transform.position.z);
        }
    }
}
