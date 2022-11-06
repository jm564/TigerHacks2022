using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntersectionCollider : MonoBehaviour
{
    [SerializeField] IntersectionManager intersectionManager;

    private void OnTriggerEnter(Collider other)
    {
        intersectionManager.VehicleEntered(other);
    }
}
