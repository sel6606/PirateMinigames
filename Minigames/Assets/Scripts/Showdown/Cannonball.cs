using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannonball : MonoBehaviour {

    public float speed;
    public int direction;
    public string owner;
    
	// Use this for initialization
	void Start () {
        speed = 3.0f;
	}
	
	// Update is called once per frame
	void Update () {
        Move();
	}

    public void Move()
    {
        //move left
        if (direction == 0)
        {
            transform.right = new Vector3(-1, 0, 0);
            transform.position += transform.right * speed * Time.deltaTime;
        }
        //move right
        else
        {
            transform.right = new Vector3(1, 0, 0);
            transform.position += transform.right * speed * Time.deltaTime;
        }
    }
}
