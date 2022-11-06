using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StateConstants
{
    public static State NW = new State() { PrimaryDirection = Direction.North, SecondaryDirection = Direction.West };
    public static State NE = new State() { PrimaryDirection = Direction.North, SecondaryDirection = Direction.East };
    public static State NS = new State() { PrimaryDirection = Direction.North, SecondaryDirection = Direction.South };
    public static State EW = new State() { PrimaryDirection = Direction.East, SecondaryDirection = Direction.West };
    public static State ES = new State() { PrimaryDirection = Direction.East, SecondaryDirection = Direction.South };
    public static State EN = new State() { PrimaryDirection = Direction.East, SecondaryDirection = Direction.North };
    public static State SE = new State() { PrimaryDirection = Direction.South, SecondaryDirection = Direction.East };
    public static State SN = new State() { PrimaryDirection = Direction.South, SecondaryDirection = Direction.North };
    public static State SW = new State() { PrimaryDirection = Direction.South, SecondaryDirection = Direction.West };
    public static State WS = new State() { PrimaryDirection = Direction.West, SecondaryDirection = Direction.South };
    public static State WE = new State() { PrimaryDirection = Direction.West, SecondaryDirection = Direction.East };
    public static State WN = new State() { PrimaryDirection = Direction.West, SecondaryDirection = Direction.North };
    public static State PN = new State() { PrimaryDirection = Direction.North, SecondaryDirection = Direction.South, Pedestrian = true };
    public static State PE = new State() { PrimaryDirection = Direction.East, SecondaryDirection = Direction.West, Pedestrian = true };
    public static State PW = new State() { PrimaryDirection = Direction.West, SecondaryDirection = Direction.East, Pedestrian = true };
    public static State PS = new State() { PrimaryDirection = Direction.South, SecondaryDirection = Direction.North, Pedestrian = true };

    public static List<State> VehicleStates = new List<State>() { NW, NE, NS, EW, ES, EN, SE, SN, SW, WS, WE, WN };
}


public class State
{
    public Direction PrimaryDirection { get; set; }
    public Direction SecondaryDirection { get; set; }
    public bool Pedestrian { get; set; } = false;
}
