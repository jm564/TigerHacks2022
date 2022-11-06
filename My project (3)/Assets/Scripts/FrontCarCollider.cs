using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrontCarCollider : MonoBehaviour
{
    [SerializeField] Vehicle vehicle;

    private void OnTriggerEnter(Collider other)
    {
        vehicle.FrontCollision(other);
    }

    private void OnTriggerExit(Collider other)
    {
        vehicle.FrontCollisionCleared(other);
    }
}
