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




            //another difficult way to do markers on map
            //Instantiate(mapMarker, hit.point, Quaternion.identity, transform);
            //Debug.DrawRay(Vector3.zero, hit.point, Color.red);
            // Debug.DrawRay(Vector3.zero, transform.position, Color.blue);
            /*if (hit.collider.tag == "map")
            {
                Vector3 localMapPos = hit.point - transform.position;
                Debug.Log(transform.eulerAngles.y);


                if (hit.point.x >= transform.position.x)
                {
                    Debug.Log("x1>x0");

                    if (hit.point.z > transform.position.z)
                    {
                        Debug.Log("z1>z0");
                        if (transform.eulerAngles.y >= 135 && transform.eulerAngles.y < 225)
                        { k = -1; }
                        else
                        { k = 1; }
                    }
                    else
                    {
                        Debug.Log("z1<z0");
                        if (transform.eulerAngles.y < 45 || transform.eulerAngles.y >= 315)
                        { k = 1; }
                        else
                        { k = -1; }
                    }


                }
                else
                {
                    Debug.Log("x1<x0");

                    if (hit.point.z > transform.position.z)
                    {
                        Debug.Log("z1>z0");
                        if (transform.eulerAngles.y >= 45 && transform.eulerAngles.y < 135)
                        { k = -1; }
                        else
                        { k = 1; }
                    }
                    else
                    {
                        Debug.Log("z1<z0");
                        k = -1;
                    }

                }

                mapMarker.transform.localPosition = new Vector3(k * Mathf.Sqrt(localMapPos.x * localMapPos.x + localMapPos.z * localMapPos.z), localMapPos.y, -0.57f);

            }*/
        }
    }
}
