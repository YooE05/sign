using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] GameObject map;
    [SerializeField] GameObject journal;

    bool mapIsOn = false;

    public Camera cam;
    public float zoomMultiplier = 2;
    public float defaultFov = 90;
    public float zoomDuration = 2;

    private void Start()
    {
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.M))
        {
            map.SetActive(mapIsOn);
            mapIsOn = !mapIsOn;
        }

        if (Input.GetMouseButton(1))
        {
            ZoomCamera(defaultFov / zoomMultiplier);
        }
        else if (cam.fieldOfView != defaultFov)
        {
            ZoomCamera(defaultFov);
        }

    }

    void ZoomCamera(float target)
    {
        float angle = Mathf.Abs((defaultFov / zoomMultiplier) - defaultFov);
        cam.fieldOfView = Mathf.MoveTowards(cam.fieldOfView, target, angle / zoomDuration * Time.deltaTime);
    }
}
