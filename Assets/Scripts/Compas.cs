using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Compas : MonoBehaviour
{

    Vector3 vector;
    [SerializeField] Transform arrow;

    void FixedUpdate()
    {
        vector.z = transform.eulerAngles.y;
        arrow.localEulerAngles = vector; 
    }

}
