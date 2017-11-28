using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Struct that stores all of a ship's relevant data
/// </summary>
[System.Serializable]
public struct ShipData
{
    public string name;
    public int goldAmount;
    public bool isFlagship;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="shipName">The name of the ship</param>
    /// <param name="startingGold">The amount of gold the ship starts with</param>
    /// <param name="isFlagship">Whether or not this ship is the flagship</param>
    public ShipData(string shipName, int startingGold, bool isFlagship)
    {
        this.name = shipName;
        this.goldAmount = startingGold;
        this.isFlagship = isFlagship;
    }
}
