using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum VehicleType
{
    Car,
    Bus,
    Bike,
    Pedestrian,
    EmergencyVehicle
}

public class EnvironmentManaager : MonoBehaviour
{
    public List<GameObject> Moveables;

    [SerializeField] int MaxMoveableObjCount = 15;
    [SerializeField] GameObject Car;
    [SerializeField] GameObject Bus;
    [SerializeField] GameObject Pedestrian;
    [SerializeField] GameObject EmergencyVehicle;
    [SerializeField] GameObject Bike;

    private float TimeSinceLastVehicleSpawned = 0;

    void Update()
    {
        TimeSinceLastVehicleSpawned += Time.deltaTime;

        if (Moveables.Count <= MaxMoveableObjCount && TimeSinceLastVehicleSpawned > 3)
        {
            TimeSinceLastVehicleSpawned = 0;

            GameObject AddedObject = null;

            switch(Random.Range(1, 5))
            {
                case 1:
                    AddedObject = GameObject.Instantiate(Car);
                    AddedObject.transform.SetPositionAndRotation(PutVehicleInCorrectLane(AddedObject), Quaternion.Euler(GetCorrectVehicleRotation(AddedObject)));
                    break;

                case 2:
                    AddedObject = GameObject.Instantiate(Pedestrian);
                    AddedObject.transform.localPosition = PutPedestrianInCorrectLane(AddedObject);
                    break;

                case 3:
                    AddedObject = GameObject.Instantiate(Bus);
                    AddedObject.transform.SetPositionAndRotation(PutVehicleInCorrectLane(AddedObject), Quaternion.Euler(GetCorrectVehicleRotation(AddedObject)));
                    break;

                case 4:
                    AddedObject = GameObject.Instantiate(EmergencyVehicle);
                    AddedObject.transform.SetPositionAndRotation(PutVehicleInCorrectLane(AddedObject), Quaternion.Euler(GetCorrectVehicleRotation(AddedObject)));
                    break;
            }

            Moveables.Add(AddedObject);
            TimeSinceLastVehicleSpawned = 0;
        }

        if(TimeSinceLastVehicleSpawned > 5)
        {
            var vehiclesThatFellOff = Moveables.Where(x => x.transform.localPosition.y < -5f);
            foreach(var vehicle in vehiclesThatFellOff)
            {
                Moveables.Remove(vehicle);
                GameObject.Destroy(vehicle);
            }
        }
    }

    public Vector3 PutVehicleInCorrectLane(GameObject VehicleGameObject)
    {
        var vehicle = VehicleGameObject.GetComponent<Vehicle>();
        var direction = StateConstants.VehicleStates.ElementAt(Random.Range(0, StateConstants.VehicleStates.Count));
        vehicle.PrimaryDirection = direction.PrimaryDirection;
        vehicle.IntersectionDirection = direction.SecondaryDirection;

        return MapDirectionsToSpawner(vehicle);
    }

    public Vector3 MapDirectionsToSpawner(Vehicle vehicle)
    {
        switch ((int) vehicle.PrimaryDirection)
        {
            case (int) Direction.North:
                if (vehicle.IntersectionDirection == Direction.West)
                {
                    vehicle.MakesLeftTurn = true;
                    return new Vector3(200, 2, 0);
                }
                else if(vehicle.IntersectionDirection == Direction.East)
                {
                    vehicle.MakesRightTurn = true;
                }
                return new Vector3(200, 2, 14);
            case (int) Direction.East:
                if (vehicle.IntersectionDirection == Direction.North)
                {
                    vehicle.MakesLeftTurn = true;
                    return new Vector3(0, 2, -200);
                }
                else if (vehicle.IntersectionDirection == Direction.South)
                {
                    vehicle.MakesRightTurn = true;
                }
                return new Vector3(14, 2, -200);
            case (int) Direction.South:
                if (vehicle.IntersectionDirection == Direction.East)
                {
                    vehicle.MakesLeftTurn = true;
                    return new Vector3(-200, 2, 0);
                }
                else if (vehicle.IntersectionDirection == Direction.West)
                {
                    vehicle.MakesRightTurn = true;
                }
                return new Vector3(-200, 2, -14);
            case (int) Direction.West:
                if (vehicle.IntersectionDirection == Direction.South)
                {
                    vehicle.MakesLeftTurn = true;
                    return new Vector3(0, 2, 200);
                }
                else if (vehicle.IntersectionDirection == Direction.North)
                {
                    vehicle.MakesRightTurn = true;
                }
                return new Vector3(-14, 2, 200);
        }
        return Vector3.zero;
    }

    public Vector3 PutPedestrianInCorrectLane(GameObject PedestrianGameObject)
    {
        var pedestrian = PedestrianGameObject.GetComponent<Pedestrian>();
        pedestrian.PrimaryDirection = (Direction)Random.Range(0, 4);

        switch (pedestrian.PrimaryDirection)
        {
            case Direction.North:
                return new Vector3(200, 2, 28);
            case Direction.East:
                return new Vector3(28, 2, -200);
            case Direction.South:
                return new Vector3(-200, 2, 28);
            case Direction.West:
                return new Vector3(28, 2, 200);
        }
        return Vector3.zero;
    }

    public Vector3 GetCorrectVehicleRotation(GameObject addedObject)
    {
        var vehicle = addedObject.GetComponent<Vehicle>();

        switch (vehicle.PrimaryDirection)
        {
            case Direction.North:
                return new Vector3(0, 90, 0);
            case Direction.East:
                return new Vector3(0, 180, 0);
            case Direction.South:
                return new Vector3(0, -90, 0);
            case Direction.West:
                return new Vector3(0, 0, 0);
        }
        return Vector3.zero;
    }

    public void Reset()
    {
        foreach(var moveable in Moveables)
        {
            Moveables.Remove(moveable);
            GameObject.Destroy(moveable);
        }
    }

    public void RemoveObject(GameObject moveable)
    {
        Debug.Log("Out of bounds object being removed");
        var removable = Moveables.FirstOrDefault(x => x.GetInstanceID() == moveable.GetInstanceID());
        if(removable == null)
        {
            GameObject.Destroy(moveable);
            return;
        }

        Moveables.Remove(removable) ;
        GameObject.Destroy(moveable);
    }

}
