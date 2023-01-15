using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mazeCheckpoint : MonoBehaviour
{
    [SerializeField] int nextIndex;
    [SerializeField] Compas compass;
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            compass.crntPoint = nextIndex;
        }
    }
}
