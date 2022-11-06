using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class IntersectionManager : MonoBehaviour
{
    [SerializeField] public List<Pedestrian> Pedestrians;
    [SerializeField] public List<Vehicle> Vehicles;

    [SerializeField] public List<State> AllowedPaths;

    [SerializeField] public TrafficLight NBoundTrafficLight;
    [SerializeField] public TrafficLight EBoundTrafficLight;
    [SerializeField] public TrafficLight SBoundTrafficLight;
    [SerializeField] public TrafficLight WBoundTrafficLight;

    public bool IsReadyForNextLightStatus = true;
    public float TimeSinceLastLightChange;

    public IntersectionManager()
    {
        AllowedPaths = new List<State>();
    }

    public void Update()
    {
        foreach (var vehicle in Vehicles)
        {
            vehicle.TimeInIntersection += Time.deltaTime;
        }

        foreach (var pedestrian in Pedestrians)
        {
            pedestrian.TimeInIntersection += Time.deltaTime;
        }

        TimeSinceLastLightChange += Time.deltaTime;

        if (TimeSinceLastLightChange > 5)
        {
            AllowedPaths = new List<State>();

            EBoundTrafficLight.StraightLightToState = LightState.Red;
            EBoundTrafficLight.TurnLightToState = LightState.Red;

            WBoundTrafficLight.StraightLightToState = LightState.Red;
            WBoundTrafficLight.TurnLightToState = LightState.Red;


            NBoundTrafficLight.StraightLightToState = LightState.Red;
            NBoundTrafficLight.TurnLightToState = LightState.Red;

            SBoundTrafficLight.StraightLightToState = LightState.Red;
            SBoundTrafficLight.TurnLightToState = LightState.Red;

            if (!Vehicles.Any(x => x.CanMove) && !Pedestrians.Any(x => x.CanMove))
            {
                IsReadyForNextLightStatus = true;
                TimeSinceLastLightChange = 0;
            }
        }
    }

    public void VehicleEntered(Collider collider)
    {
        if(collider.gameObject.TryGetComponent<Vehicle>(out var vehicle))
        {
            if (vehicle.IntersectionPassed)
            {
                if (Vehicles.RemoveAll(x => x.gameObject.GetInstanceID() == vehicle.gameObject.GetInstanceID()) != 0)
                {
                    return;
                }
            }
            else if (!vehicle.IntersectionPassed && !vehicle.IntersectionEntered)
            {
                vehicle.IntersectionEntered = true;
                vehicle.CanMove = false;
                vehicle.SetCurrentTrafficLightState(AllowedPaths);
                Vehicles.Add(vehicle);
            }
        }

        if (collider.gameObject.TryGetComponent<Pedestrian>(out var pedestrian))
        {
            if(pedestrian.IntersectionPassed)
            {
                if (Pedestrians.RemoveAll(x => x.gameObject.GetInstanceID() == pedestrian.gameObject.GetInstanceID()) != 0)
                {
                    return;
                }
            }
            else if(!pedestrian.IntersectionPassed && !pedestrian.IntersectionEntered)
            {
                pedestrian.IntersectionEntered = true;
                pedestrian.CanMove = false;
                pedestrian.SetCurrentTrafficLightState(AllowedPaths);
                Pedestrians.Add(pedestrian);
            }
        }
    }

    public void SetCurrentLightState(int state)
    {
        switch(state)
        {
            case 1:
                AllowedPaths = new List<State>() { StateConstants.EN, StateConstants.EW, StateConstants.ES};
                EBoundTrafficLight.StraightLightToState = LightState.Green;
                EBoundTrafficLight.TurnLightToState = LightState.Green;

                WBoundTrafficLight.StraightLightToState = LightState.Red;
                WBoundTrafficLight.TurnLightToState = LightState.Red;

                NBoundTrafficLight.StraightLightToState = LightState.Red;
                NBoundTrafficLight.TurnLightToState = LightState.Red;

                SBoundTrafficLight.StraightLightToState = LightState.Red;
                SBoundTrafficLight.TurnLightToState = LightState.Red;
                TimeSinceLastLightChange = 0;
                IsReadyForNextLightStatus = false;
                break;

            case 2:
                AllowedPaths = new List<State>() { StateConstants.NE, StateConstants.NS, StateConstants.NW};
                EBoundTrafficLight.StraightLightToState = LightState.Red;
                EBoundTrafficLight.TurnLightToState = LightState.Red;

                WBoundTrafficLight.StraightLightToState = LightState.Red;
                WBoundTrafficLight.TurnLightToState = LightState.Red;

                NBoundTrafficLight.StraightLightToState = LightState.Green;
                NBoundTrafficLight.TurnLightToState = LightState.Green;

                SBoundTrafficLight.StraightLightToState = LightState.Red;
                SBoundTrafficLight.TurnLightToState = LightState.Red;
                TimeSinceLastLightChange = 0;
                IsReadyForNextLightStatus = false;
                break;

            case 3:
                AllowedPaths = new List<State>() { StateConstants.WE, StateConstants.WN, StateConstants.WS };
                EBoundTrafficLight.StraightLightToState = LightState.Red;
                EBoundTrafficLight.TurnLightToState = LightState.Red;

                WBoundTrafficLight.StraightLightToState = LightState.Green;
                WBoundTrafficLight.TurnLightToState = LightState.Green;

                NBoundTrafficLight.StraightLightToState = LightState.Red;
                NBoundTrafficLight.TurnLightToState = LightState.Red;

                SBoundTrafficLight.StraightLightToState = LightState.Red;
                SBoundTrafficLight.TurnLightToState = LightState.Red;
                TimeSinceLastLightChange = 0;
                IsReadyForNextLightStatus = false;
                break;

            case 4:
                AllowedPaths = new List<State>() { StateConstants.SN, StateConstants.SE, StateConstants.SW };
                EBoundTrafficLight.StraightLightToState = LightState.Red;
                EBoundTrafficLight.TurnLightToState = LightState.Red;

                WBoundTrafficLight.StraightLightToState = LightState.Red;
                WBoundTrafficLight.TurnLightToState = LightState.Red;

                NBoundTrafficLight.StraightLightToState = LightState.Red;
                NBoundTrafficLight.TurnLightToState = LightState.Red;

                SBoundTrafficLight.StraightLightToState = LightState.Green;
                SBoundTrafficLight.TurnLightToState = LightState.Green;
                TimeSinceLastLightChange = 0;
                IsReadyForNextLightStatus = false;
                break;

            case 5:
                AllowedPaths = new List<State>() { StateConstants.PN, StateConstants.PS, StateConstants.PW, StateConstants.PE};
                EBoundTrafficLight.StraightLightToState = LightState.Red;
                EBoundTrafficLight.TurnLightToState = LightState.Red;

                WBoundTrafficLight.StraightLightToState = LightState.Red;
                WBoundTrafficLight.TurnLightToState = LightState.Red;


                NBoundTrafficLight.StraightLightToState = LightState.Red;
                NBoundTrafficLight.TurnLightToState = LightState.Red;

                SBoundTrafficLight.StraightLightToState = LightState.Red;
                SBoundTrafficLight.TurnLightToState = LightState.Red;
                TimeSinceLastLightChange = 0;
                IsReadyForNextLightStatus = false;
                break;
        }

        foreach(var moveable in Vehicles)
        {
            moveable.SetCurrentTrafficLightState(AllowedPaths);
        }

        foreach (var moveable in Pedestrians)
        {
            moveable.SetCurrentTrafficLightState(AllowedPaths);
        }
    }

    public void Reset()
    {
        Vehicles = new List<Vehicle>();
        Pedestrians = new List<Pedestrian>();
        AllowedPaths = new List<State>();
    }
}
