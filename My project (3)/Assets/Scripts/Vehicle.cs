using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public enum Direction
{
    North,
    East,
    South,
    West
}

public class Vehicle : MonoBehaviour
{
    [SerializeField] public float speedConstant = 10;
    public Direction PrimaryDirection;
    public Direction IntersectionDirection;
    [SerializeField] public VehicleType VehicleType;
    [SerializeField] public string NotMoveReason;

    public float TimeInIntersection = 0;

    public bool CanMove = true;

    public bool MakesLeftTurn = false;
    public bool MakesRightTurn = false;
    public bool IntersectionPassed = false;
    public bool IntersectionEntered = false;

    public int GetUUID()
    {
        return this.GetInstanceID();
    }

    public void UpdateVehiclePath()
    {
        this.PrimaryDirection = IntersectionDirection;
        switch (IntersectionDirection)
        {
            case Direction.North:
                this.transform.rotation = Quaternion.Euler(new Vector3(0, 90, 0));
                break;
            case Direction.East:
                this.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
                break;
            case Direction.South:
                this.transform.rotation = Quaternion.Euler(new Vector3(0, -90, 0));
                break;
            case Direction.West:
                this.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                break;
        }
    }

    public void Update()
    {
        if (CanMove)
            this.transform.Translate(new Vector3(0, 0, -Time.deltaTime * speedConstant));
    }

    public void FrontCollision(Collider other)
    {
        if(other.TryGetComponent<Vehicle>(out var hitVehicle))
        {
            CanMove = false;
            NotMoveReason = "Vehicle";
        }
        else if(other.TryGetComponent<Pedestrian>(out var hitPedestrian))
        {
            CanMove = false;
            NotMoveReason = "Pedestrian";
        }
        else if(other.TryGetComponent<LeftTurnPoint>(out var leftTurnPoint) && MakesLeftTurn)
        {
            if(leftTurnPoint.PrimaryDirection == PrimaryDirection)
                UpdateVehiclePath();
        }
        else if (other.TryGetComponent<RightTurnPoint>(out var rightTurnPoint) && MakesRightTurn)
        {
            UpdateVehiclePath();
        }
        else if(other.TryGetComponent<IntersectionCenter>(out var intersectionCenter))
        {
            IntersectionPassed = true;
        }
    }

    public void FrontCollisionCleared(Collider other)
    {
        if (other.TryGetComponent<Vehicle>(out var hitVehicle))
        {
            CanMove = true;
            NotMoveReason = "";
        }
        else if (other.TryGetComponent<Pedestrian>(out var hitPedestrian))
        {
            CanMove = true;
            NotMoveReason = "";
        }
    }

    public void Delete()
    {
        GameObject.Destroy(this);
    }

    public void SetCurrentTrafficLightState(List<State> AllowedPaths)
    {
        if (AllowedPaths.Any(x => x.PrimaryDirection == PrimaryDirection))
            CanMove = true;
    }
}