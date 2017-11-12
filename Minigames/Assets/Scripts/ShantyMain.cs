using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;

public class ShantyMain : MonoBehaviour {

    public string[] shantyArr;
    public string shanty;
    public List<string> wronganswers;

    public Button optionOne;
    public Button optionTwo;
    public Button optionThree;
    public GameObject shantyText;
    public string correctAnswer;

	// Use this for initialization
	void Start () {
        shantyArr = new string[3];
        shantyArr[0] = "Yo Ho Ho and a Bottle of ___.";
        shantyArr[1] = "Dave Jones' ___.";
        shantyArr[2] = "Pirates say \"___!\"";

        wronganswers.Add("Vodka");
        wronganswers.Add("Water");
        wronganswers.Add("Potato");

        PlayGame();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void PlayGame()
    {
        GetRandomShanty();
        CreateOptions();
    }

    /// <summary>
    /// Gets a random shanty from shantyArr and identifies the correct answer
    /// </summary>
    /// <returns> A random shanty string </returns>
    public string GetRandomShanty()
    {
        int random = (int)Random.Range(0, 3);
        shanty = shantyArr[random];
        if (random == 0)
        {
            correctAnswer = "Rum";
        }
        else if (random == 1)
        {
            correctAnswer = "Locker";
        }
        else
        {
            correctAnswer = "Arrgh";
        }

        return shanty;
    }

    /// <summary>
    /// Change buttons text to have the correct answer on a random button, and two incorrect answers on the other two buttons
    /// </summary>
    public void CreateOptions()
    {
        int random = (int)Random.Range(0, 3);
        if (random == 0)
        {
            optionOne.GetComponentInChildren<Text>().text = correctAnswer;
        }
        else
        {
            optionOne.GetComponentInChildren<Text>().text = wronganswers[(int)Random.Range(0, wronganswers.Count)];
        }

        if (random == 1)
        {
            optionTwo.GetComponentInChildren<Text>().text = correctAnswer;
        }
        else
        {
            optionTwo.GetComponentInChildren<Text>().text = wronganswers[(int)Random.Range(0, wronganswers.Count)];
        }

        if (random == 2)
        {
            optionThree.GetComponentInChildren<Text>().text = correctAnswer;
        }
        else
        {
            optionThree.GetComponentInChildren<Text>().text = wronganswers[(int)Random.Range(0, wronganswers.Count)];
        }
    }
}
