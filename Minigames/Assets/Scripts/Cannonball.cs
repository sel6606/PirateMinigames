using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannonball : MonoBehaviour {

    public GameObject board;

    public float speed;

	// Use this for initialization
	void Start () {
        speed = 10.0f;
	}
	
	// Update is called once per frame
	void Update () {
        Move();
        CheckBounds();
	}

    private void Move()
    {
        transform.position += transform.right * speed * Time.deltaTime;
    }

    private void CheckBounds()
    {
        if (Mathf.Abs(transform.position.x) > Mathf.Abs(board.transform.localScale.x / 2) ||
            Mathf.Abs(transform.position.y) > Mathf.Abs(board.transform.localScale.y / 2))
        {
            Destroy(gameObject);
        }
    }
}
