using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlacedNote : MonoBehaviour, IInteractable
{
    [SerializeField] NoteGameController noteController;
    [SerializeField] NotePlace appropriatePlace;
    public Note crntNoteData;
    public bool interactOnlyInMinigame { get => true; }
    public void Interact()
    {
        if (crntNoteData.needPlaceNum == appropriatePlace.placeNum)
        {
            noteController.countOfCorrect--;
        }

        //сн€ть карточку, поместить обратно в инвентарь
        appropriatePlace.ClearData();

    }
    public void SetUpView()
    {
        gameObject.GetComponentInChildren<Image>().sprite = crntNoteData.noteSprite;
    }
    public void SecondInteract()
    {
        //повернуть карточку в зависимости от текущего поворота
        crntNoteData.rotationPos++;
        if (crntNoteData.rotationPos == 4)
        {
            crntNoteData.rotationPos = 0;
        }

        RotateNoteProperly();
    }

    public void RotateNoteProperly()
    {
        transform.rotation = Quaternion.Euler(Vector3.zero);
        transform.Rotate(Vector3.right, crntNoteData.rotationPos * 90f);
        transform.rotation = Quaternion.Euler(new Vector3(transform.localEulerAngles.x, -transform.localEulerAngles.y , transform.localEulerAngles.z));
       
        noteController.CheckCorrectPositions();
    }
    public string SetDescriptiion()
    {
        return "жми";
    }
}
