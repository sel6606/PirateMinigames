using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGame : MonoBehaviour {

    public GameObject background;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        SetBackgroundToScreen();
	}

    private void SetBackgroundToScreen()
    {
        /* 
            This only works if the camera is orthographic and the x and y of the
            background position is set to 0.
            In Commented Useful Code, SetBackgroundToScreen2() works for all cases.
        */

        //Precheck to avoid setting background to screen over and over
        Vector3 backgroundToScreen = Camera.main.WorldToScreenPoint(new Vector3(-background.transform.localScale.x / 2.0f, background.transform.localScale.y / 2.0f, 0.0f));
        if (backgroundToScreen.x == Screen.width && backgroundToScreen.y == Screen.height) //background already set to screen
        {
            return;
        }

        //Get screen width and height
        float width = Screen.width;
        float height = Screen.height;

        //Convert to world coordinates
        Vector3 screenToWorld = Camera.main.ScreenToWorldPoint(new Vector3(width, height, 0.0f));

        //Get distance from background's center to the world coordinates
        float xDistance = Mathf.Abs(background.transform.position.x - screenToWorld.x); //halfwidth.x
        float yDistance = Mathf.Abs(background.transform.position.y - screenToWorld.y); //halfwidth.y

        //Scale the background by this distance
        background.transform.localScale = new Vector3(xDistance * 2.0f, yDistance * 2.0f, background.transform.localScale.z);
    }

    #region Commented Useful Code
    private void SetBackgroundToScreen2()
    {
        //Set this value to the z distance between the background and the camera position
        float distanceFromCamera = -26.0f;

        //Get background's distance from the camera according to the viewport
        Vector3 bottomLeftCamera = new Vector3(0.0f, 0.0f, distanceFromCamera);

        //Get the bottom left point of the viewport based on the distance
        Vector3 bottomLeftWorld = Camera.main.ViewportToWorldPoint(bottomLeftCamera);

        //Get the x distance from the background's center to the left of the viewport
        float xDistance = Mathf.Abs(background.transform.position.x - bottomLeftWorld.x);

        //Get the background's distance from the camera according to the viewport
        Vector3 topRightCamera = new Vector3(1.0f, 1.0f, distanceFromCamera);

        //Get the top right point of the viewport based on the distance
        Vector3 topRightWorld = Camera.main.ViewportToWorldPoint(topRightCamera);

        //Get the y distance from the background's center to the top of the viewport
        float yDistance = Mathf.Abs(background.transform.position.y - topRightWorld.y);

        //Scale the background to fit the viewport
        background.transform.localScale = new Vector3(xDistance * 2.0f, yDistance * 2.0f, background.transform.localScale.z);

    }
    #endregion
}
