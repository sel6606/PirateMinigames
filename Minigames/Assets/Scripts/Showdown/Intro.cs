using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour {

    public Button play;

    // Use this for initialization
    void Start () {
        play.onClick.AddListener(LoadGame);
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    public void LoadGame()
    {
        SceneManager.LoadScene("ShowdownMain");
    }
}
