using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Compas : MonoBehaviour
{

    Vector3 vector;
    [SerializeField] Transform arrow;

    [SerializeField] bool isCaveLevel = false;

    public bool isInMaze;
    [SerializeField] List<Transform> pointsToLookAt;
    public int crntPoint = 0;

    Quaternion startRotation;

    private void Start()
    {
        startRotation = arrow.rotation;
    }

    void FixedUpdate()
    {
        if (!isCaveLevel)
        {

            vector.y = -transform.eulerAngles.y + 90;
            arrow.localEulerAngles = vector;

        }
        else
        {
            if (isInMaze)
            {
                if (crntPoint != 6)
                {
                    arrow.LookAt(pointsToLookAt[crntPoint], transform.up);
                    // Debug.Log(-arrow.localEulerAngles.y);
                    arrow.localEulerAngles = new Vector3(0, arrow.localEulerAngles.y - 45f, 0);
                }
            }
            else
            {
                arrow.localEulerAngles += Vector3.up * 5f;
            }
        }
    }

}
