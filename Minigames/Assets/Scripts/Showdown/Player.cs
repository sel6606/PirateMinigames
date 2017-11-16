using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public int health;
    private float speed;

	// Use this for initialization
	void Start () {
        health = 1;
        speed = 3.0f;
        //transform.right = new Vector3(1, 0, 0);
	}
	
	// Update is called once per frame
	void Update () {
        Move();
	}

    //moves Player One in the four cardinal directions
    public void Move()
    {
        if (this.gameObject.name == "PlayerOne")
        {
            //move up
            if (Input.GetKey(KeyCode.W))
            {
                //if (transform.right.y < 0.9)
                //{
                    transform.right = new Vector3(1,0,0);
                //}
                transform.position += transform.right * speed * Time.deltaTime;
            }
            //move down
            if (Input.GetKey(KeyCode.S))
            {
                //if (transform.right.y > 0.9)
                //{
                transform.right = new Vector3(-1, 0, 0);
                //}
                transform.position -= transform.right * speed * Time.deltaTime;
            }
            //move left
            if (Input.GetKey(KeyCode.A))
            {
                //if (transform.right.x > 0.9)
                //{
                transform.right = new Vector3(0, 1, 0);
                //}
                transform.position += transform.right * speed * Time.deltaTime;
            }
            //move right
            if (Input.GetKey(KeyCode.D))
            {
                //if (transform.right.x < 0.9)
                //{
                transform.right = new Vector3(0, -1, 0);
                //}
                transform.position -= transform.right * speed * Time.deltaTime;
            }
        }
        if (gameObject.tag == "PlayerTwo")
        {
            //move up
            if (Input.GetKey(KeyCode.UpArrow))
            {
                transform.position += transform.right * speed * Time.deltaTime;
            }
            //move down
            if (Input.GetKey(KeyCode.DownArrow))
            {
                transform.position -= transform.right * speed * Time.deltaTime;
            }
            //move left
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                transform.position += transform.right * speed * Time.deltaTime;
            }
            //move right
            if (Input.GetKey(KeyCode.RightArrow))
            {
                transform.position -= transform.right * speed * Time.deltaTime;
            }
        }
    }
}
