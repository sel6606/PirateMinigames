using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Script that manages the word bank for the game
/// </summary>
public class WordBank : MonoBehaviour
{
    public TextAsset[] wordLists;
    public GameObject rowPrefab;
    public InputField playerInput;
    public Canvas canvas;
    public Text listName;

    private string[] currentWordBank;
    private List<string> wordBankList;
    private List<Text> allWords;

	// Use this for initialization
	void Start ()
    {
        allWords = new List<Text>();
        InitializeWordBank();
        playerInput.Select();
	}
	
	// Update is called once per frame
	void Update ()
    {
	}

    /// <summary>
    /// Sets the word bank to use for this round of the game
    /// </summary>
    /// <param name="index">The index of the word bank</param>
    void SetWordBank(int index)
    {
        listName.text=wordLists[index].name;
        string temp = wordLists[index].text;

        currentWordBank = temp.Split('\n');

        //Normalize the words
        for(int i = 0; i < currentWordBank.Length; i++)
        {
            currentWordBank[i] = currentWordBank[i].Replace("\r", string.Empty);
        }

        wordBankList = new List<string>(currentWordBank);

        //foreach(string s in currentWordBank)
        //{
            //Debug.Log(s);
        //}
    }

    /// <summary>
    /// Initialize and display the word bank
    /// </summary>
    void InitializeWordBank()
    {
        //Choose a random work bank from the possible word banks
        int randomIndex = Random.Range(0, wordLists.Length);
        SetWordBank(randomIndex);

        //Calculate the number of rows to display
        int numWords = currentWordBank.Length;
        int numRow = numWords / 5;

        if(numWords % 5 != 0)
        {
            numRow++;
        }

        //Initialize each row of words
        for(int i = 0; i < numRow; i++)
        {
            //Instantiate a row, and get the rectTransform
            GameObject temp = Instantiate(rowPrefab,canvas.transform, false);
            RectTransform rectangleTransform = temp.GetComponent<RectTransform>();

            //Retrieve the text objects for each item in the row
            Text[] words = temp.GetComponentsInChildren<Text>();

            //Iterate through each item in the row
            for(int j = 0; j < 5; j++)
            {
                //Calculate which word to add to this item
                int wordNumber = (5 * i) + j;

                //Make sure there are still words left, otherwise, destroy the item in the row
                if(wordNumber < currentWordBank.Length)
                {
                    words[j].text = currentWordBank[wordNumber];
                    allWords.Add(words[j]);
                }
                else
                {
                    Destroy(words[j].gameObject.transform.parent.gameObject);
                }
            }

            //Position the rows
            float yPos = i * -40;
            rectangleTransform.anchoredPosition = new Vector2(0, yPos);
        }
    }

    /// <summary>
    /// Checks the current input that has been entered by the player
    /// </summary>
    public void CheckInput()
    {
        string input = playerInput.text;
        int index = -1;

        foreach(string s in wordBankList)
        {
            index++;

            if (input.Equals(s, System.StringComparison.OrdinalIgnoreCase))
            {
                allWords[index].enabled = true;
                allWords.RemoveAt(index);
                wordBankList.RemoveAt(index);
                ClearInput();
                break;
            }
        }
    }

    /// <summary>
    /// Clears the input field
    /// </summary>
    public void ClearInput()
    {
        playerInput.Select();
        playerInput.text = "";
    }
}
