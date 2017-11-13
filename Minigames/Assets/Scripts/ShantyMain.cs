using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ShantyMain : MonoBehaviour {

    public string[] shantyArr;
    public string shanty;
    public List<string> wronganswers;
    public string correctAnswer;

    public Button optionOne;
    public Button optionTwo;
    public Button optionThree;
    public Text shantyText;

	// Use this for initialization
	void Start () {
        shantyArr = new string[5];
        shantyArr[0] = "Yo Ho Ho and a Bottle of ___.";
        shantyArr[1] = "Davy Jones' ___.";
        shantyArr[2] = "Pirates say \"___!\"";
        shantyArr[3] = "You're a filthy ___lubber!";
        shantyArr[4] = "Blow - Blow - Blow the ___ down!";

        wronganswers.Add("Vodka");
        wronganswers.Add("Water");
        wronganswers.Add("Potato");
        wronganswers.Add("Sea");
        wronganswers.Add("Gold");
        wronganswers.Add("Wooden");
        wronganswers.Add("Dubloon");
        wronganswers.Add("Captain");

        PlayGame();
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    public void PlayGame()
    {
        GetRandomShanty();
        CreateOptions();
        optionOne.onClick.AddListener(delegate { OptionClick(optionOne); });
        optionTwo.onClick.AddListener(delegate { OptionClick(optionTwo); });
        optionThree.onClick.AddListener(delegate { OptionClick(optionThree); });
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
        else if (random == 2)
        {
            correctAnswer = "Arrgh";
        }
        else if (random == 3)
        {
            correctAnswer = "Land";
        }
        else
        {
            correctAnswer = "Man";
        }
        shantyText.text = shanty;

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

    public void OptionClick(Button option)
    {
        //Debug.Log("Button Pressed!");
        //Debug.Log(option.GetComponentInChildren<Text>().text);
        if (option.GetComponentInChildren<Text>().text == correctAnswer)
        {
            shantyText.text = "You Won!";
        }
        else
        {
            shantyText.text = "Better Luck Next Time!";
            
        }
    }
}
