using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;
using System;
using System.Linq;

public class IntersectionAgent : Agent
{
    [SerializeField] public IntersectionManager intersectionManager;
    [SerializeField] public EnvironmentManaager environmentManaager;
    [SerializeField] public float EmergencyVehicleTimeInIntersection = 3;
    [SerializeField] public float BusVehicleTimeInIntersection = 3;
    [SerializeField] public float PedestrianVehicleTimeInIntersection = 3;
    [SerializeField] public float VehicleTimeInIntersection = 3;


    public override void OnEpisodeBegin()
    {
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        intersectionManager.Vehicles.ForEach(x => sensor.AddObservation((int) x.PrimaryDirection));
        intersectionManager.Vehicles.ForEach(x => sensor.AddObservation((int) x.IntersectionDirection));
        intersectionManager.Vehicles.ForEach(x => sensor.AddObservation((int) x.VehicleType));

        intersectionManager.Vehicles.ForEach(x => sensor.AddObservation((int)x.PrimaryDirection));
        intersectionManager.Vehicles.ForEach(x => sensor.AddObservation((int)x.IntersectionDirection));
        intersectionManager.Vehicles.ForEach(x => sensor.AddObservation((int)x.VehicleType));

        sensor.AddObservation(intersectionManager.TimeSinceLastLightChange);
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        var discreteActions = actions.DiscreteActions;
        if(intersectionManager.IsReadyForNextLightStatus)
            intersectionManager.SetCurrentLightState(discreteActions[0]);
        SetActionReward();
    }

    private void SetActionReward()
    {
        var emergencyVehicles = intersectionManager.Vehicles.Where(x => x.VehicleType == VehicleType.EmergencyVehicle);
        if (emergencyVehicles.Any(x => x.TimeInIntersection > EmergencyVehicleTimeInIntersection))
            SetReward(-0.3f);
        else if (emergencyVehicles.Any(x => x.TimeInIntersection < EmergencyVehicleTimeInIntersection))
            SetReward(0.3f);

        var pedestrians = intersectionManager.Pedestrians;
        if (pedestrians.Any(x => x.TimeInIntersection > PedestrianVehicleTimeInIntersection))
            SetReward(-0.3f);
        else if (pedestrians.Any(x => x.TimeInIntersection < PedestrianVehicleTimeInIntersection))
            SetReward(0.3f);

        var busses = intersectionManager.Vehicles.Where(x => x.VehicleType == VehicleType.Bus);
        if (busses.Any(x => x.TimeInIntersection > BusVehicleTimeInIntersection))
            SetReward(-0.3f);
        else if (emergencyVehicles.Any(x => x.TimeInIntersection < BusVehicleTimeInIntersection))
            SetReward(0.3f);

        var cars = intersectionManager.Vehicles.Where(x => x.VehicleType == VehicleType.Car);
        if (cars.Any(x => x.TimeInIntersection > VehicleTimeInIntersection))
            SetReward(-0.1f);
        else if (cars.Any(x => x.TimeInIntersection < VehicleTimeInIntersection))
            SetReward(0.1f);

        if (intersectionManager.TimeSinceLastLightChange > 10)
            SetReward(-0.1f);
    }
}
