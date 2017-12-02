using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SRHat : MonoBehaviour {

    private float speed;

    // Use this for initialization
    void Start()
    {
        speed = 3.0f;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position += transform.right * speed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position -= transform.right * speed * Time.deltaTime;
        }
    }
}
