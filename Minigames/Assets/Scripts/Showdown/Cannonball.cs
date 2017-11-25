﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannonball : MonoBehaviour {

    public float speed;
    public int direction;
    public string owner;
    
	// Use this for initialization
	void Start () {
        speed = 4.0f;
	}
	
	// Update is called once per frame
	void Update () {
        Move();
        CheckScreenBounds();
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

    public void CheckScreenBounds()
    {
        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
        if (pos.x < 0.0)
        {
            Destroy(this.gameObject);
        }
        if (1.0 < pos.x)
        {
            Destroy(this.gameObject);
        }
    }
}
