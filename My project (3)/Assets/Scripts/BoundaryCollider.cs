using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundaryCollider : MonoBehaviour
{
    [SerializeField] EnvironmentManaager environmentManaager;

    public void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<GameObject>(out var moveable))
        {
            environmentManaager.RemoveObject(moveable);
        }
    }
}
