using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour {

    public GameObject board;
    public GameObject cannonballPrefab;

    public Vector3 spawnPos;

    public float waitTimer;

    public float speed;
    public bool onScreen;

	// Use this for initialization
	void Start () {
        spawnPos = transform.position;

        waitTimer = 2.0f;

        speed = 2.0f;
        onScreen = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (!onScreen)
        {
            MoveOnScreen();
        }
        
        else
        {
            if (waitTimer > 0)
            {
                waitTimer -= Time.deltaTime;
            }
            else
            {
                MoveOffScreen();
            }
        }
	}

    private void MoveOnScreen()
    {
        if (Mathf.Abs(transform.position.x) > Mathf.Abs(board.transform.localScale.x / 2) ||
            Mathf.Abs(transform.position.y) > Mathf.Abs(board.transform.localScale.y / 2))
        {
            //Move forward
            transform.position += transform.right * speed * Time.deltaTime;
        }
        else
        {
            onScreen = true;
            Fire();
        }
    }

    private void Fire()
    {
        //Spawn cannonball
        GameObject cannonball = Instantiate(cannonballPrefab, transform.GetChild(0).transform.position, Quaternion.identity);

        //Set cannonball's right to the same as the cannon
        cannonball.transform.right = gameObject.transform.right;
    }

    private void MoveOffScreen()
    {
        if (Mathf.Abs(transform.position.x) < Mathf.Abs(spawnPos.x) ||
            Mathf.Abs(transform.position.y) < Mathf.Abs(spawnPos.y))
        {
            //Move forward
            transform.position += -transform.right * speed * Time.deltaTime;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
