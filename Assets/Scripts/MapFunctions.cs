using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapFunctions : MonoBehaviour
{
    [SerializeField] GameObject mapMarker;
    [SerializeField] GameObject playerMarker;
    [SerializeField] float markerDeleteRadius;
    [SerializeField] GameObject WalkZone;
    [SerializeField] GameObject map;

    [HideInInspector] public bool mapIsOpen = false;

    [HideInInspector] public Camera cam;

    void Update()
    {

        if (mapIsOpen)
        {
            if (Input.GetMouseButtonDown(0))
            {
                SetMarker();
            }
        }
        //Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        //Debug.DrawRay(ray.origin, ray.direction, Color.red);
        float x = map.transform.localScale.x / 2 * (transform.position.x / WalkZone.transform.localScale.x * 2);
        float y = map.transform.localScale.y / 2 * (transform.position.z / WalkZone.transform.localScale.z * 2);

        playerMarker.transform.localPosition = new Vector3(-y, x, -0.7f);

    }

    private void SetMarker()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);


        if (Physics.Raycast(ray, out RaycastHit hit))
        {

            Debug.DrawRay(ray.origin, ray.direction, Color.red);
            if (hit.collider.tag == "map")
            {
                if (Vector3.Distance(mapMarker.transform.position, hit.point) > markerDeleteRadius)
                {
                    mapMarker.SetActive(true);
                    mapMarker.transform.position = hit.point;
                }
                else
                {
                    mapMarker.SetActive(false);
                }
            }

        }
    }
}