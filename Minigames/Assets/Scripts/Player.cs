using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    private MiniGame game;

    public string left;
    public string right;
    public string up;
    public string down;

    public float speed;

	// Use this for initialization
	void Start () {
        game = GameObject.Find("MiniGameManager").GetComponent<MiniGame>();

        speed = 6.0f;
	}
	
	// Update is called once per frame
	void Update () {
        ProcessInput();
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Cannon")
        {
            if (!game.gameOver)
            {
                game.gameOver = true;
                Destroy(gameObject);
            }
        }

        Debug.Log("Collision");
    }

    private void ProcessInput()
    {
        if (Input.GetKey(up))
        {
            if (transform.right.y < 1)
            {
                transform.right = transform.up;
                Debug.Log("Set Up");
            }

            transform.position += transform.right * speed * Time.deltaTime;
        }

        else if (Input.GetKey(down))
        {
            if (transform.right.y > -1)
            {
                transform.right = -transform.up;
                Debug.Log("Set Down");
            }

            transform.position += transform.right * speed * Time.deltaTime;
        }

        else if (Input.GetKey(right))
        {
            if (transform.right.x > -1)
            {
                transform.right = new Vector3(-1,0,0);
                Debug.Log("Set Right");
            }

            transform.position += transform.right * speed * Time.deltaTime;
        }

        else if (Input.GetKey(left))
        {
            if (transform.right.x < 1)
            {
                transform.right = new Vector3(1,0,0);
                Debug.Log("Set Left");
            }

            transform.position += transform.right * speed * Time.deltaTime;
        }
    }
}
