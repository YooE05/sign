using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [SerializeField] List<Note> takenNotes = new List<Note>();
    [SerializeField] List<GameObject> invCells = new List<GameObject>();
    [SerializeField] JournalController journal;


    private void Start()
    {
        FillCells();
    }
    public void AddNoteFromWorld(NoteInWorld newNote)
    {
        if (newNote.crntNoteData != null)
        {
            takenNotes.Add(newNote.crntNoteData);
            FillCells();
        }

        journal.AddFindedNote(newNote);

    }

    public void AddNoteFromMinigame(Note newNote)
    {
        takenNotes.Add(newNote);
        journal.AddMinigameNote(newNote);
        FillCells();
    }

    public Note SelectNote(int index)
    {
        if (index <takenNotes.Count && takenNotes.Count>0)
        {
            return takenNotes[index];
        }
        return null;
    }

    public void PullNote(Note removingNote)
    {
        int index = takenNotes.LastIndexOf(removingNote);
        //переключить в журнале спрайт картинки на - не в инвентаре
        journal.PullFromInventory(removingNote);
        if (takenNotes[index] != null)
        {
            takenNotes.Remove(takenNotes[index]);
        }
        FillCells();
    }

    void FillCells()
    {
        for (int i = 0; i < invCells.Count; i++)
        {
            if (i < takenNotes.Count)
            { invCells[i].GetComponent<Image>().sprite = takenNotes[i].noteSprite; }
            else
            {
                invCells[i].GetComponent<Image>().sprite = null;
            }

        }
    }



}
