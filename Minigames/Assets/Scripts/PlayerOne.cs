using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOne : MonoBehaviour {

    public int health;
    private float speed;

	// Use this for initialization
	void Start () {
        health = 1;
        speed = 3.0f;
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
            if (Input.GetKey(KeyCode.W))
            {
                
            }
            //move down
            if (Input.GetKey(KeyCode.S))
            {
                this.transform.Translate(0, 0, speed);
            }
            //move left
            if (Input.GetKey(KeyCode.A))
            {
                this.transform.Translate(-speed, 0, 0);
            }
            //move right
            if (Input.GetKey(KeyCode.D))
            {
                this.transform.Translate(speed, 0, 0);
            }
        }
        if (gameObject.tag == "PlayerTwo")
        {
            //move up
            if (Input.GetKey(KeyCode.UpArrow))
            {

            }
            //move down
            if (Input.GetKey(KeyCode.DownArrow))
            {

            }
            //move left
            if (Input.GetKey(KeyCode.LeftArrow))
            {

            }
            //move right
            if (Input.GetKey(KeyCode.RightArrow))
            {

            }
        }
    }
}
