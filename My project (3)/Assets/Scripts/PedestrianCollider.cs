using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PedestrianCollider : MonoBehaviour
{
    [SerializeField] Pedestrian pedestrian;

    private void OnTriggerEnter(Collider other)
    {
        pedestrian.Collision(other);
    }
}
