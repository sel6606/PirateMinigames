using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Sets up the interface with the current ships and gold amounts
/// </summary>
public class SetupInterface : MonoBehaviour
{
    #region Public Variables
    public RectTransform shipyardP1;
    public RectTransform shipyardP2;
    public RectTransform draggableArea;
    public GameObject flagshipPrefab;
    public GameObject pirateShipPrefab;

    public List<RectTransform> targetsP1;
    public List<RectTransform> targetsP2;
    #endregion

    // Use this for initialization
    void Start ()
    {
        //Retrieve the list of ships for each player
        List<ShipData> shipsP1 = PlayerInfo.instance.ShipsP1;
        List<ShipData> shipsP2 = PlayerInfo.instance.ShipsP2;

        //Add the ships for player one
        foreach(ShipData s in shipsP1)
        {
            AddShip(shipsP1.IndexOf(s), true, s.isFlagship, s.goldAmount);
        }

        //Add the ships for player two
        foreach(ShipData s in shipsP2)
        {
            AddShip(shipsP2.IndexOf(s), false, s.isFlagship, s.goldAmount);
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    /// <summary>
    /// Initializes a ship on the interface
    /// </summary>
    /// <param name="listIndex">The index of the ship in the shipyard</param>
    /// <param name="isPlayer1">Which player to initialize the ship for true = player 1, false = player 2</param>
    /// <param name="isFlagship">Is the ship a flagship or not? Default is false</param>
    /// <param name="startGold">Optional parameter for the amount of gold to initialize the ship with</param>
    public void AddShip(int listIndex, bool isPlayer1, bool isFlagship = false, int startGold = -1)
    {
        RectTransform parentTemp;
        int defaultStartGold;
        GameObject temp;

        //Determine the rectangle transform to use when initializing
        if (isPlayer1)
        {
            parentTemp = shipyardP1;
        }
        else
        {
            parentTemp = shipyardP2;
        }

        //Is this ship a flagship?
        if (isFlagship)
        {
            //Set the default starting gold to 3 and instantiate the ship
            defaultStartGold = 3;
            temp = Instantiate(flagshipPrefab, parentTemp);
        }
        else
        {
            //Set the default starting gold to 1 and instantiate the ship
            defaultStartGold = 1;
            temp = Instantiate(pirateShipPrefab, parentTemp);
        }

        //If the start gold was not set by the player, use the default gold amount
        if (startGold < 0)
        {
            startGold = defaultStartGold;
        }

        if (isPlayer1)
        {
            temp.GetComponent<DragShips>().possibleTargets = targetsP1;
        }
        else
        {
            temp.GetComponent<DragShips>().possibleTargets = targetsP2;
        }

        //Set the text
        temp.GetComponentInChildren<Text>().text = startGold.ToString();

        //Set other necessary variables
        temp.GetComponent<DragShips>().draggableArea = draggableArea;
        temp.GetComponent<DragShips>().GoldAmount = startGold;
        temp.GetComponent<DragShips>().ListIndex = listIndex;

    }

}
