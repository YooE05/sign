using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class JournalController : MonoBehaviour
{
    [SerializeField] Inventory inventory;

    [SerializeField] List<Image> places = new List<Image>();
    [SerializeField] List<NoteInWorld> jNotes = new List<NoteInWorld>();

    int pageCount;
    int crntPage;

    bool justOpen;

    private void Start()
    {
        crntPage = 1;
        justOpen = false;
        //SetUpCrntPage();
    }

    private void Update()
    {
        if (GameControllerCave.journalIsActive)
        {
            if (!justOpen)
            {
                justOpen = true;
                SetUpCrntPage();
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                SetUpNewPage(false);
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                SetUpNewPage(true);
            }
        }
        else
        {
            justOpen = false;
        }
       // Debug.Log(GameControllerCave.journalIsActive);

    }

    public void AddFindedNote(NoteInWorld newNote)
    {
        jNotes.Add(newNote);
        pageCount = jNotes.Count % 4 == 0 ? jNotes.Count / 4 : jNotes.Count / 4 + 1;
        SetUpCrntPage();
    }
    public void AddMinigameNote(Note newNote)
    {
        if (jNotes.FindAll(w => w.crntNoteData == newNote).Count > 0)
        {
            jNotes.FindAll(w => w.crntNoteData == newNote)[0].inInventory = true;
        }
        SetUpCrntPage();
    }

    public void PullFromInventory(Note newNote)
    {
        if (jNotes.FindAll(w => w.crntNoteData == newNote).Count > 0)
        {
            jNotes.FindAll(w => w.crntNoteData == newNote)[0].inInventory = false;
        }
    }

    void SetUpNewPage(bool toNextPage)
    {
        //звук перелистывания
        if (toNextPage)
        {
            crntPage++;
        }
        else
        {
            crntPage--;
        }
        crntPage = Mathf.Clamp(crntPage, 1, pageCount);
        SetUpCrntPage();
    }

    void SetUpCrntPage()
    {
        //crntPage = Mathf.Clamp(crntPage, 1, pageCount);
        for (int i = 0; i < 4; i++)
        {
            if (jNotes.Count > i + (crntPage - 1) * 4)
            {
                places[i].sprite = jNotes[i + (crntPage - 1) * 4].GetSprite();
                places[i].color = new Color(1, 1, 1, 1);
            }
            else
            {
                places[i].sprite = null;
                places[i].color = new Color(1, 1, 1, 0);
            }
        }
    }


}
