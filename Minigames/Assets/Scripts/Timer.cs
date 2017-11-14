using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Script that handles the countdown timer for the game
/// </summary>
public class Timer : MonoBehaviour
{
    public float timeInSeconds;
    public InputField input;
    public Text timer;

    private float timeRemaining;

	// Use this for initialization
	void Start ()
    {
        timeRemaining = timeInSeconds;
	}

    // Update is called once per frame
    void Update()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
        }
        else
        {
            timeRemaining = 0;
            GameOver();
        }

        timer.text = "Time Remaining: " + (int)timeRemaining;
	}


    /// <summary>
    /// End the game
    /// </summary>
    void GameOver()
    {
        input.readOnly = true;
        input.text = "";
        input.DeactivateInputField();
        input.interactable = false;
    }
}
