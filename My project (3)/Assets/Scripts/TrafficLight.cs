using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LightState
{
    Green,
    Yellow,
    Red
}

public class TrafficLight : MonoBehaviour
{
    [SerializeField] public Material GreenMaterial;
    [SerializeField] public Material YellowMaterial;
    [SerializeField] public Material RedMaterial;

    [SerializeField] public MeshRenderer StraightLight;
    [SerializeField] public MeshRenderer TurnLight;
    [SerializeField] public MeshRenderer PedestrianLight;

    [SerializeField] public LightState StraightLightState;
    [SerializeField] public LightState StraightLightToState;
    [SerializeField] public float StraightLightTimeDifference;

    [SerializeField] public LightState TurnLightState;
    [SerializeField] public LightState TurnLightToState;
    [SerializeField] public float TurnLightTimeDifference;

    public void Start()
    {
        StraightLightState = LightState.Red;
        TurnLightState = LightState.Red;
        StraightLightToState = LightState.Red;
        TurnLightToState = LightState.Red;
    }

    public void Update()
    {
        StraightLightTimeDifference += Time.deltaTime;
        TurnLightTimeDifference += Time.deltaTime;

        if (StraightLightTimeDifference > 5)
        {
            UpdateStraightLightSetting();
            StraightLightTimeDifference = 0;
        }

        if(TurnLightTimeDifference > 5)
        {
            UpdateTurnLightSetting();
            TurnLightTimeDifference = 0;
        }
    }

    private void UpdateStraightLightSetting()
    {
        if (StraightLightState < StraightLightToState)
        {
            StraightLightState++;
        }
        else if (StraightLightState > StraightLightToState)
        {
            StraightLightState = LightState.Green;
        }

        switch (StraightLightState)
        {
            case LightState.Green:
                StraightLight.material = GreenMaterial;
                break;

            case LightState.Yellow:
                StraightLight.material = YellowMaterial;
                break;

            case LightState.Red:
                StraightLight.material = RedMaterial;
                break;
        }
    }

    private void UpdateTurnLightSetting()
    {
        if (TurnLightState < TurnLightToState)
        {
            TurnLightState++;
        }
        else if (TurnLightState > TurnLightToState)
        {
            TurnLightState = LightState.Green;
        }

        switch (TurnLightState)
        {
            case LightState.Green:
                TurnLight.material = GreenMaterial;
                break;

            case LightState.Yellow:
                TurnLight.material = YellowMaterial;
                break;

            case LightState.Red:
                TurnLight.material = RedMaterial;
                break;
        }
    }
}
