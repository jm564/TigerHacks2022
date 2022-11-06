using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class Pedestrian : MonoBehaviour
{
    [SerializeField] public float speedConstant = 10;
    public Direction PrimaryDirection;
    public Direction IntersectionDirection = 0;
    [SerializeField] public VehicleType VehicleType = VehicleType.Pedestrian;

    public float TimeInIntersection = 0;

    public bool CanMove = true;
    public bool IntersectionPassed = false;
    public bool IntersectionEntered = false;

    public void Update()
    {
        if (CanMove)
        {
            switch (PrimaryDirection)
            {
                case Direction.North:
                    this.transform.Translate(new Vector3(-Time.deltaTime * speedConstant, 0, 0));
                    break;
                case Direction.East:
                    this.transform.Translate(new Vector3(0, 0, Time.deltaTime * speedConstant));
                    break;
                case Direction.South:
                    this.transform.Translate(new Vector3(Time.deltaTime * speedConstant, 0, 0));
                    break;
                case Direction.West:
                    this.transform.Translate(new Vector3(0, 0, -Time.deltaTime * speedConstant));
                    break;
            }
        }
    }

    public void Collision(Collider other)
    {
        if (other.TryGetComponent<PedestrianIntersectionCenter>(out var hitCenter))
        {
            IntersectionPassed = true;
        }
    }

    public void SetCurrentTrafficLightState(List<State> AllowedPaths)
    {
        if (AllowedPaths.Any(x => x.PrimaryDirection == PrimaryDirection))
            CanMove = true;
    }
}
