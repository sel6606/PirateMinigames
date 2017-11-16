using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public int health;
    private float speed;

	// Use this for initialization
	void Start () {
        health = 1;
        speed = 1.0f;
	}
	
	// Update is called once per frame
	void Update () {
        Move();
	}

    //moves Player One in the four cardinal directions
    public void Move()
    {
        if (gameObject.tag == "PlayerOne")
        {
            //move up
            if (Input.GetKeyDown(KeyCode.W))
            {
                transform.position += transform.right * speed * Time.deltaTime;
            }
            //move down
            if (Input.GetKey(KeyCode.S))
            {
                transform.position -= transform.right * speed * Time.deltaTime;
            }
            //move left
            if (Input.GetKey(KeyCode.A))
            {
                transform.position += transform.right * speed * Time.deltaTime;
            }
            //move right
            if (Input.GetKey(KeyCode.D))
            {
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
