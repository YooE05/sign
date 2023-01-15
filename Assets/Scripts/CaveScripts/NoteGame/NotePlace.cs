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
        //�������� �������� � �������� �� ������ ������� ��������� �� ���������
        if (noteController.crntPickNote != null&& !appropriateNote.gameObject.activeSelf)
        {
            Debug.Log("note Placed");
            appropriateNote.crntNoteData = noteController.crntPickNote;
            appropriateNote.SetUpView();
            appropriateNote.gameObject.SetActive(true);
            noteController.crntPickNote = null;

            //�������� �������� � ������ ���������������
            noteController.allPlacedNotes.Add(appropriateNote.crntNoteData);

            //��������� ��������� �� ��� ����������� ����������
            appropriateNote.RotateNoteProperly();

            //�������� �������� �� ���������
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
        //������ �������� �� ������ ���������������
        noteController.allPlacedNotes.Remove(appropriateNote.crntNoteData);

        //������� �������� � ���������
        noteController.playerInv.AddNoteFromMinigame(appropriateNote.crntNoteData);

        //��������� ��������
        //appropriateNote = null;
        appropriateNote.gameObject.SetActive(false);

    }

    public void SecondInteract() { }
    public string SetDescriptiion()
    {
        return "���";
    }



}
