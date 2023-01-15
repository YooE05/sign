using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotePlace : MonoBehaviour, IInteractable
{
    [SerializeField] NoteGameController noteController;
    [SerializeField] PlacedNote appropriateNote;
    public int placeNum;
    public bool interactOnlyInMinigame { get=>true;  }
    public void Interact()
    {
        //включить карточку и передать ей данные текущей выбранной из инвентаря
        if (noteController.crntPickNote != null&& !appropriateNote.gameObject.activeSelf)
        {
            Debug.Log("note Placed");
            appropriateNote.crntNoteData = noteController.crntPickNote;
            appropriateNote.SetUpView();
            appropriateNote.gameObject.SetActive(true);
            noteController.crntPickNote = null;

            //добавить карточку в список зедействованных
            noteController.allPlacedNotes.Add(appropriateNote.crntNoteData);

            //проверяем правильно ли она расположена изначально
            appropriateNote.RotateNoteProperly();

            //вытащить карточку из инвентаря
            noteController.playerInv.PullNote(appropriateNote.crntNoteData);

            if (appropriateNote.crntNoteData.needPlaceNum == placeNum)
            {
                noteController.countOfCorrect++;

            }
        }
        else
        {

            Debug.Log("no any Note");
        }

    }

    public void ClearData()
    {
        //убрать карточку из списка зедействованных
        noteController.allPlacedNotes.Remove(appropriateNote.crntNoteData);

        //вернуть карточку в инвентарь
        noteController.playerInv.AddNoteFromMinigame(appropriateNote.crntNoteData);

        //отключить карточку
        //appropriateNote = null;
        appropriateNote.gameObject.SetActive(false);

    }

    public void SecondInteract() { }
    public string SetDescriptiion()
    {
        return "жми";
    }



}
