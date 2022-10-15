using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    [SerializeField] GameObject mapAndCompass;
    [SerializeField] GameObject map;
    [SerializeField] GameObject ultraviolet;
    [SerializeField] GameObject player;
    [SerializeField] MapFunctions mapFunctions;

    float mapAnimationDelay = 0.6f;

    [SerializeField] Vector3 endMapPosition;
    [SerializeField] Vector3 startMapPosition;


    bool mapIsActive = false;
    static public bool ultraIsActive = false;
    bool isCursorOn = false;
    bool firstMapCheck = true;
    public Camera cam;

    public CharacterWalking playerMovement;
    public CharacterSight playerLook;
    [SerializeField] CharacterInteraction characterInteraction;
    [SerializeField] CharacterInteractionCave characterInteractionCave;

    public Coroutine textCoroutine;
    [SerializeField] TextMeshProUGUI infoText;

    bool isCaveLevel;

    private void Start()
    {

        infoText.text = "";
        infoText.color = new Color(infoText.color.r, infoText.color.g, infoText.color.b, 0f);
        mapIsActive = mapAndCompass.activeSelf;
        SetUpCursor();
        mapFunctions.cam = cam;
    }

    private void SetUpCursor()
    {
        //Cursor.lockState = CursorLockMode.Confined;
        // Cursor.SetCursor(cursorSprite, Vector2.zero, CursorMode.Auto);
        Cursor.visible = false;
    }


    bool haveCompass;
    void Update()
    {
        haveCompass = isCaveLevel ? true : characterInteraction.compassAdded;

        if (!PauseMenu.gameIsPaused)
        {
            if (Input.GetKeyDown(KeyCode.M) && haveCompass && !ultraIsActive)
            {
                mapIsActive = !mapIsActive;
                mapAndCompass.SetActive(mapIsActive);
                if (firstMapCheck)
                {
                    firstMapCheck = false;
                    StopShowingText();
                    ShowText("Hold right mouse button to zoom a map, left to put a marker", 5f);
                }
            }
            if (Input.GetKeyDown(KeyCode.V) && haveCompass && !mapIsActive)
            {
                ultraIsActive = !ultraIsActive;
                ultraviolet.SetActive(ultraIsActive);
            }

            if (mapIsActive)
            {
                if (Input.GetKeyDown(KeyCode.Mouse1))
                {

                    StopAllCoroutines();
                    Invoke("KillMe",3f);
                    // Cursor.lockState = CursorLockMode.Confined;

                    playerMovement.canMove = false;
                    playerLook.canLook = false;

                    isCursorOn = true;
                    mapFunctions.mapIsOpen = true;
                    // Cursor.visible = true;

                    StartCoroutine(rotateCamera(Quaternion.Euler(0f, 0f, 0f), mapAnimationDelay));

                    StartCoroutine(translateMap(endMapPosition, mapAnimationDelay));
                }
                if (Input.GetKeyUp(KeyCode.Mouse1))
                {
                    playerMovement.canMove = true;
                    playerLook.canLook = true;
                    isCursorOn = false;
                    mapFunctions.mapIsOpen = false;
                    StopAllCoroutines();
                    Invoke("KillMe", 5f);
                    // Cursor.visible = false;
                    StartCoroutine(translateMap(startMapPosition, mapAnimationDelay));

                }
            }
        }

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

    public void ShowText(string text, float timeToWait)
    {
        textCoroutine = StartCoroutine(ShowTextCoroutine(text, timeToWait));
    }
    public void StopShowingText()
    {
        StopAllCoroutines();
        //StopCoroutine(textCoroutine);
    }

    void KillMe()
    {
        infoText.color = new Color(infoText.color.r, infoText.color.g, infoText.color.b, 0f);
    }
    IEnumerator ShowTextCoroutine(string text, float timeToWait)
    {
        //показать текст I need to find my compass first
        // infoText.color =  new Color(infoText.color.r, infoText.color.g, infoText.color.b, 0f);
        infoText.text = text;
        float time = infoText.color.a, duration = 1f;
        float A;
        while (time < duration)
        {
            A = Mathf.Lerp(0f, 1f, time / duration);
            infoText.color = new Color(infoText.color.r, infoText.color.g, infoText.color.b, A); ;
            time += Time.deltaTime;
            yield return null;
        }
        infoText.color = new Color(infoText.color.r, infoText.color.g, infoText.color.b, 1f);
        yield return new WaitForSeconds(timeToWait);
        time = 1f;
        duration = 1f;
        while (time >= 0f)
        {
            A = Mathf.Lerp(0f, 1f, time / duration);
            infoText.color = new Color(infoText.color.r, infoText.color.g, infoText.color.b, A); ;
            time -= Time.deltaTime;
            yield return null;
        }
        infoText.color = new Color(infoText.color.r, infoText.color.g, infoText.color.b, 0f);
        //скрыть текст
        infoText.text = "";
        Debug.Log("end;");
        //infoText.color = new Color(infoText.color.r, infoText.color.g, infoText.color.b, 1f);
    }

}
