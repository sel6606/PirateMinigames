using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SARockScript : MonoBehaviour {

    private float speed;

    // Use this for initialization
    void Start()
    {
        speed = 5.0f;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    /// <summary>
    /// Destroys the rock when it goes off screen
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Board Collider")
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Moves coin down the screen
    /// </summary>
    private void Move()
    {
        transform.position += -transform.up * speed * Time.deltaTime;
    }
}
