using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Compas : MonoBehaviour
{

    Vector3 vector;
    [SerializeField] Transform arrow;

    [SerializeField] bool isCaveLevel=false;

    void FixedUpdate()
    {
        if (!isCaveLevel)
        {
            vector.y = -transform.eulerAngles.y + 90;
            arrow.localEulerAngles = vector;
        }
        else
        {
            arrow.localEulerAngles += Vector3.up*5f;
        }
    }

}
