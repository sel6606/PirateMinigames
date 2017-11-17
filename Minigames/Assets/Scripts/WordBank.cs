using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Script that manages the word bank for the game
/// </summary>
public class WordBank : MonoBehaviour
{
    #region Public Variables
    public TextAsset[] wordLists;
    public GameObject cellPrefab;
    public InputField playerInput;
    public Canvas canvas;
    public Text listName;
    public GameObject listPanel;
    #endregion

    #region Class Variables
    private string[] currentWordBank;
    private List<string> wordBankList;
    private List<Text> allWords;
    #endregion

    #region Properties
    public List<string> WordBankList
    {
        get { return wordBankList; }
    }

    public int TotalWords
    {
        get { return currentWordBank.Length; }
    }
    #endregion

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
        listName.text="Category: " + wordLists[index].name;
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
        int numRows = numWords / 5;

        //If the number of words is not divisible by 5, there is one more row than what was calculated
        if(numWords % 5 != 0)
        {
            numRows++;
        }

        //Calculate the height of each cell based off of the number of rows
        float height = listPanel.GetComponent<RectTransform>().rect.height / numRows ;

        //Set the width and height of the cell
        //Since there is a maximum of 5 cells per row, the width is a fixed value
        listPanel.GetComponent<GridLayoutGroup>().cellSize = new Vector2(200, height);

        //Initialize each word
        for (int i = 0; i < numWords; i++)
        {
            //Instantiate a word and set the cell as a child of the panel
            GameObject temp = Instantiate(cellPrefab,listPanel.transform, false);

            //Retrieve the text object for the item
            Text word = temp.GetComponentInChildren<Text>();

            //Set the text for the word
            word.text = currentWordBank[i];
            allWords.Add(word);
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
