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
        health = 1;
        speed = 4.0f;
	}
	
	// Update is called once per frame
	void Update () {
        Move();
	}

    //moves the players in the four cardinal directions
    public void Move()
    {
        if (this.gameObject.name == "PlayerOne")
        {
            //move up
            if (Input.GetKey(KeyCode.W))
            {
                transform.eulerAngles = Vector3.zero;
                transform.position += transform.up * speed * Time.deltaTime;
            }
            //move down
            if (Input.GetKey(KeyCode.S))
            {
                //transform.right = new Vector3(-1, 0, 0);
                transform.eulerAngles = new Vector3(180, 0, 0);
                transform.position += transform.up * speed * Time.deltaTime;
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
        if (this.gameObject.name == "PlayerTwo")
        {
            //move up
            if (Input.GetKey(KeyCode.UpArrow))
            {
                transform.eulerAngles = Vector3.zero;
                transform.position += transform.up * speed * Time.deltaTime;
            }
            //move down
            if (Input.GetKey(KeyCode.DownArrow))
            {
                transform.eulerAngles = new Vector3(180, 0, 0);
                transform.position += transform.up * speed * Time.deltaTime;
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

    public void OnTriggerEnter2D(Collider2D collision)
    {
        
    }
}
