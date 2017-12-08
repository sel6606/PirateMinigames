using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Script that handles the countdown timer and game over logic for the game
/// </summary>
public class Timer : MonoBehaviour
{
    #region Public Variables
    public float timeInSeconds;
    public InputField input;
    public Text timer;
    public Text scoreLeft;
    public Text scoreRight;
    public GameObject endScreen;
    public GameObject listManager;
    #endregion

    #region Class Variables
    private float timeRemaining;
    #endregion

	// Use this for initialization
	void Start ()
    {
        //Make sure the end screen is not visible at the start
        endScreen.SetActive(false);
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
        //Calculate how many words the player got correct and set the text accordingly
        int totalWords = listManager.GetComponent<WordBank>().TotalWords;
        int numCorrect = totalWords - listManager.GetComponent<WordBank>().WordBankList.Count;
        scoreLeft.text = numCorrect.ToString();
        scoreRight.text = totalWords.ToString();

        //Make the end screen visible
        endScreen.SetActive(true);

        //Make it so the player can no longer edit the input field
        input.readOnly = true;
        input.text = "";
        input.DeactivateInputField();
        input.interactable = false;
    }
}
