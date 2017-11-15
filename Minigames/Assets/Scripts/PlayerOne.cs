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
        if (Input.GetKeyDown(KeyCode.W))
        {
            this.transform.Translate(0, 0, -speed);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            this.transform.Translate(0, 0, speed);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            this.transform.Translate(-speed, 0, 0);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            this.transform.Translate(speed, 0, 0);
        }
    }
}
