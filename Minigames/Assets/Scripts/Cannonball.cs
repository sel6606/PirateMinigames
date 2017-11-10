using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannonball : MonoBehaviour {

    private float speed;

	// Use this for initialization
	void Start () {
        speed = 10.0f;
	}
	
	// Update is called once per frame
	void Update () {
        Move();
	}

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.tag == "Board")
        {
            Destroy(gameObject);
        }
    }

    private void Move()
    {
        transform.position += transform.right * speed * Time.deltaTime;
    }
}
