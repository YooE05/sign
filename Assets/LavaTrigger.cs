using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaTrigger : MonoBehaviour
{
    [SerializeField] GameObject playerGO;
    [SerializeField] Transform restartPosition;


    private void OnTriggerEnter(Collider other)
    {
     
        if (other.gameObject.tag == "Player")
        {
            playerGO.transform.position = restartPosition.position;
        }


    }
}
