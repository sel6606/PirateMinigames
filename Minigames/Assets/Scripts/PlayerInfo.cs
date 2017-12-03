using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script to keep track of various game information required between multiple scenes
/// </summary>
public class PlayerInfo : MonoBehaviour
{

    //Represents the game info that is stored across all scenes
    public static PlayerInfo instance;

    /// <summary>
    /// The list of ships that the first player starts the game with
    /// </summary>
    public List<ShipData> startingShipsP1;

    /// <summary>
    /// The list of ships that the second player starts the game with
    /// </summary>
    public List<ShipData> startingShipsP2;

    #region Class Variables
    private List<ShipData> shipsP1;
    private List<ShipData> shipsP2;
    private List<PortData> ports;

    private PlayerAdvantage advantage;

    private int playerOneShip;
    private int playerTwoShip;

    private int singlePlayerShip;
    #endregion

    #region Properties
    public List<ShipData> ShipsP1
    {
        get { return shipsP1;}
        set { shipsP1 = value; }
    }

    public List<ShipData> ShipsP2
    {
        get { return shipsP2; }
        set { shipsP2 = value; }
    }

    public List<PortData> Ports
    {
        get { return ports; }
        set { ports = value; }
    }

    public PlayerAdvantage Advantage
    {
        get { return advantage; }
        set { advantage = value; }
    }

    public int PlayerOneShip
    {
        get { return playerOneShip; }
        set { playerOneShip = value; }
    }

    public int PlayerTwoShip
    {
        get { return playerTwoShip; }
        set { playerTwoShip = value; }
    }

    public int SinglePlayerShip
    {
        get { return singlePlayerShip; }
        set { singlePlayerShip = value; }
    }
    #endregion

    void Awake()
    {
        //If there is not already a PlayerInfo object, set it to this
        if (instance == null)
        {
            //Object this is attached to will be preserved between scenes
            DontDestroyOnLoad(gameObject);

            shipsP1 = startingShipsP1;
            shipsP2 = startingShipsP2;

            instance = this;
        }
        else if(instance != this)
        {
            //Ensures that there are no duplicate objects being made every time the scene is loaded
            Destroy(gameObject);
        }
    }

    // Use this for initialization
    void Start ()
    {
        //Initialize either the default values or the values from the previous scenes
        shipsP1 = instance.ShipsP1;
        shipsP2 = instance.ShipsP2;
        ports = instance.Ports;
        advantage = instance.Advantage;
        playerOneShip = instance.PlayerOneShip;
        playerTwoShip = instance.PlayerTwoShip;
        singlePlayerShip = instance.SinglePlayerShip;
		
	}

}
