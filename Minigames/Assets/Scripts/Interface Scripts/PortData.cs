using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Struct that stores all of a port's relevant data
/// </summary>
[System.Serializable]
public struct PortData
{
    /// <summary>
    /// The name of the port
    /// </summary>
    public string name;

    /// <summary>
    /// The amount of gold currently at the port
    /// </summary>
    public int goldAtPort;

    /// <summary>
    /// Which player controls the port (1 or 2).
    /// If no player controls the port, the value is 0
    /// </summary>
    public int controllingPlayer;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="portName">The name of the port</param>
    /// <param name="startingGold">The amount of gold the port starts with</param>
    public PortData(string portName, int startingGold)
    {
        this.name = portName;
        this.goldAtPort = startingGold;
        this.controllingPlayer = 0;
    }
}