using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class NoteGameController : MonoBehaviour
{
    bool gameIsOpen;
    [SerializeField] Camera mainCam;
    [SerializeField] Camera minigameCam;

    [SerializeField] GameObject exitRock;

    public List<Note> allPlacedNotes = new List<Note>();
    int countOfPlaces = 7;

    public Inventory playerInv;

    public Note crntPickNote;

    RaycastHit hit;

    public int countOfCorrect;

    void Start()
    {
        countOfCorrect = 0;
        gameIsOpen = false;
    }

    public void EnterMinigame()
    {

        gameIsOpen = !gameIsOpen;
        if (gameIsOpen)
        {
            CharacterWalking.canMove = false;
            mainCam.enabled = false;
            mainCam.gameObject.GetComponent<CharacterSight>().enabled = false;
            minigameCam.gameObject.SetActive(true);
            Cursor.visible = true;

            //Открыть инвентарь
            playerInv.gameObject.SetActive(true);

            //выключить коллайдер для входа в миниигру
            gameObject.GetComponent<Collider>().enabled = false;
        }
        else
        {
            CharacterWalking.canMove = true;
            mainCam.enabled = true;
            mainCam.gameObject.GetComponent<CharacterSight>().enabled = true;
            minigameCam.gameObject.SetActive(false);
            Cursor.visible = false;
            playerInv.gameObject.SetActive(false);

            //включить коллайдер для входа в миниигру
            gameObject.GetComponent<Collider>().enabled = true;
        }
    }

    private void Update()
    {

        if (gameIsOpen)
        {

            Ray ray = minigameCam.ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(ray.origin, ray.direction, Color.red);
            if (Physics.Raycast(ray, out hit))
            {
                IInteractable interactableObj = hit.collider.gameObject.GetComponent<IInteractable>();

                if (interactableObj != null)
                {

                    if (Input.GetKeyDown(KeyCode.Mouse0))
                    {
                        interactableObj.Interact();
                    }
                    if (Input.GetKeyDown(KeyCode.Mouse1))
                    {
                        interactableObj.SecondInteract();
                    }

                }


            }
        }

    }
    public void CheckCorrectPositions()
    {
        if (countOfCorrect == countOfPlaces)
        {
            int k = 0;
            for (int i = 0; i < allPlacedNotes.Count; i++)
            {
                if (allPlacedNotes[i].rotationPos == allPlacedNotes[i].needRotationNum)
                { k++; }
            }

            if (k == countOfPlaces)
            {
                EndMinigame();
            }
        }
    }

    void EndMinigame()
    {
        Debug.Log("Молодец, так держать.");

        //звук смещения камней
        exitRock.SetActive(false);
    }

    public void GetNoteFromInventory(int noteInd)
    {
        crntPickNote = playerInv.SelectNote(noteInd);
    }

}




