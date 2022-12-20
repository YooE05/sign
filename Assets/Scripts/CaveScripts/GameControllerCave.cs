using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameControllerCave : MonoBehaviour
{
    [SerializeField] GameObject mapAndCompass;
    [SerializeField] GameObject ultraviolet;
    [SerializeField] GameObject player;



    bool mapIsActive = false;
    static public bool ultraIsActive = false;
    bool isCursorOn = false;
    bool firstMapCheck = true;

    static public bool journalIsActive = false;
    [SerializeField] GameObject journalGO;


    public Camera cam;

   // public CharacterWalking playerMovement;
    public CharacterSight playerLook;
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
        if (!PauseMenu.gameIsPaused)
        {
            if (Input.GetKeyDown(KeyCode.M) && !ultraIsActive)
            {
                mapIsActive = !mapIsActive;
                mapAndCompass.SetActive(mapIsActive);
                if (firstMapCheck)
                {
                    firstMapCheck = false;
                    StopShowingText();
                    ShowText("Hmm, compass seems broken..", 5f);
                }
            }
            if (Input.GetKeyDown(KeyCode.V) && !mapIsActive)
            {
                ultraIsActive = !ultraIsActive;
                ultraviolet.SetActive(ultraIsActive);
            }

            if (Input.GetKeyDown(KeyCode.J) )
            {
                //звук открытия журнала

                journalIsActive = !journalIsActive;
                journalGO.SetActive(journalIsActive);
                //отключить передвижение и осмотр
                CharacterWalking.canMove = !journalIsActive;
                CharacterSight.canLook = !journalIsActive;
            }
        }
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
