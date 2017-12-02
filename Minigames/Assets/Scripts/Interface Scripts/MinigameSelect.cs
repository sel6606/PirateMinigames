using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Script that contains logic for choosing minigames
/// </summary>
public class MinigameSelect : MonoBehaviour
{
    private bool isSet;
    private int goldOnShip;
    private RectTransform setObject;

    public bool IsSet
    {
        get { return isSet; }
        set { isSet = value; }
    }

    public int GoldOnShip
    {
        get { return goldOnShip; }
        set { goldOnShip = value; }
    }

	// Use this for initialization
	void Start ()
    {
        isSet = false;
        goldOnShip = 0;
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(IsSet && transform.childCount == 1)
        {
            gameObject.GetComponentInChildren<Text>().enabled = true;
            IsSet = false;
        }
	}

}
