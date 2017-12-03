using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SAPlayerScript : MonoBehaviour {

    private SAMiniGame game;

    public ParticleSystem particles;

    private float speed;
    private Vector3 rightVector;

    public SAMiniGame Game
    {
        get { return game; }
        set { game = value; }
    }

    // Use this for initialization
    void Start()
    {
        speed = 3.5f;
        rightVector = transform.up;
    }

    // Update is called once per frame
    void Update()
    {
        if (!game.GameOver)
        {
            Move();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Collision with rock (Player died)
        if (collision.tag == "Rock")
        {
            if (!game.GameOver)
            {
                //Hide player
                gameObject.GetComponent<SpriteRenderer>().enabled = false;

                //Play particle system
                particles.Play();

                //Switch to game over state
                game.SetGameOverState(false);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //Player won
        if (collision.tag == "Board Collider")
        {
            //Make player invisible
            gameObject.GetComponent<SpriteRenderer>().enabled = false;

            //Switch to game over state
            game.SetGameOverState(true);
        }
    }

    private void Move()
    {
        //Both arrows pressed, keep ship straight
        if (Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.RightArrow))
        {
            transform.rotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, 90.0f));
        }

        //Left arrow, tilt to the left
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position += -rightVector * speed * Time.deltaTime;
            transform.rotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, 80.0f));
        }

        //Right arrow, tilt to the right
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += rightVector * speed * Time.deltaTime;
            transform.rotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, 100.0f));
        }

        //Nothing pressed, keep ship straight
        else
        {
            if (transform.rotation.eulerAngles.z != 90.0f)
            {
                transform.rotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, 90.0f));
            }
        }
    }
}
