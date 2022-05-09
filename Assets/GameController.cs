using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] GameObject mapAndCompass;
    [SerializeField] GameObject map;
    [SerializeField] GameObject player;
    [SerializeField] MapFunctions mapFunctions;

    float mapAnimationDelay = 0.6f;

    [SerializeField] Vector3 endMapPosition;
    [SerializeField] Vector3 startMapPosition;

    bool mapIsActive = false;
    bool isCursorOn = false;

    public Camera cam;
    public float zoomMultiplier = 2;
    public float defaultFov = 90;
    public float zoomDuration = 2;

    // public FirstPersonMovement playerMovement;
    //public FirstPersonLook playerLook;
    public CharacterWalking playerMovement;
    public CharacterSight playerLook;

    //[SerializeField] Texture2D cursorSprite;

    private void Start()
    {
        mapIsActive = mapAndCompass.activeSelf;
        SetUoCursor();
        mapFunctions.cam = cam;
    }

    private void SetUoCursor()
    {
        //Cursor.lockState = CursorLockMode.Confined;
        // Cursor.SetCursor(cursorSprite, Vector2.zero, CursorMode.Auto);
        Cursor.visible = false;
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.M))
        {
            mapIsActive = !mapIsActive;
            mapAndCompass.SetActive(mapIsActive);

        }

        /* if (!mapIsActive)
         {
             if (Input.GetMouseButton(1))
             {
                 ZoomCamera(defaultFov / zoomMultiplier);
             }
             else if (cam.fieldOfView != defaultFov)
             {
                 ZoomCamera(defaultFov);
             }
         }
         else*/
        if (mapIsActive)
        {

            if (Input.GetKey(KeyCode.Mouse1))
            {
                if (playerMovement.canMove)
                { StopAllCoroutines(); }

                Cursor.lockState = CursorLockMode.Confined;

                playerMovement.canMove = false;
                playerLook.canLook = false;

                isCursorOn = true;
                mapFunctions.mapIsOpen = true;

                StartCoroutine(rotateCamera(Quaternion.Euler(0f, 0f, 0f), mapAnimationDelay));

                StartCoroutine(translateMap(endMapPosition, mapAnimationDelay));
            }
            if (Input.GetKeyUp(KeyCode.Mouse1))
            {
                Cursor.lockState = CursorLockMode.Locked;
                playerMovement.canMove = true;
                playerLook.canLook = true;
                isCursorOn = false;
                mapFunctions.mapIsOpen = false;
                StopAllCoroutines();

                StartCoroutine(translateMap(startMapPosition, mapAnimationDelay));
            }


        }

        /*
            void ZoomMap(float target)
            {

                float distCovered = (Time.time - startTime) * speed;

                // Fraction of journey completed equals current distance divided by total distance.
                float fractionOfJourney = distCovered / journeyLength;

                // Set our position as a fraction of the distance between the markers.
                transform.position = Vector3.Lerp(startMapPosition, openMapPosition, fractionOfJourney);
            }
        */
    }
    IEnumerator translateMap(Vector3 targetPosition, float duration)
    {
        Cursor.visible = isCursorOn;

        float time = 0;
        Vector3 startPosition = map.transform.localPosition;
        while (time < duration)
        {
            map.transform.localPosition = Vector3.Lerp(startPosition, targetPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        map.transform.localPosition = targetPosition;
    }
    IEnumerator rotateCamera(Quaternion targetRotation, float duration)
    {
        float time = 0;
        Quaternion startRotation = cam.gameObject.transform.localRotation;

        while (time < duration)
        {
            cam.gameObject.transform.localRotation = Quaternion.Lerp(startRotation, targetRotation, time / duration);

            time += Time.deltaTime;
            yield return null;
        }


        playerLook.velocity = new Vector2(playerLook.velocity.x, 0);

        cam.gameObject.transform.localRotation = targetRotation;
    }


    void ZoomCamera(float target)
    {
        float angle = Mathf.Abs((defaultFov / zoomMultiplier) - defaultFov);
        cam.fieldOfView = Mathf.MoveTowards(cam.fieldOfView, target, angle / zoomDuration * Time.deltaTime);
    }

}
