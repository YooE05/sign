using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamerasController : MonoBehaviour
{
    // [SerializeField] Camera mainCamera;
    [SerializeField] List<Camera> cameras = new List<Camera>();
    int crntCam;

    private void Start()
    {
        crntCam = 0;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            cameras[0].enabled = false;
            for (int i = 1; i < cameras.Count; i++)
            {
                cameras[i].enabled = false;
            }
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            crntCam++;
            if (crntCam > cameras.Count - 1)
            {
                crntCam = 0;
            }

            for (int i = 0; i < cameras.Count; i++)
            {
                cameras[i].enabled = false;
            }
            cameras[crntCam].enabled = true;
        }

    }
}
