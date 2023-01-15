using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoteInWorld : MonoBehaviour, IInteractable
{
    [SerializeField] NoteGameController noteController;
    public Note crntNoteData;

    public Sprite firstImage;
    public Sprite secondImage;
   public bool inInventory;

    public bool interactOnlyInMinigame { get => false; }

    public Sprite GetSprite()
    {
        if (inInventory)
        {
            return firstImage;
        }
        else
        {
            return secondImage;
        }

    }

    public void Interact()
    {
        inInventory = true;
        //снять карточку, поместить обратно в инвентарь
        noteController.playerInv.AddNoteFromWorld(this);
          gameObject.tag = "nonInteract";
       

      //  gameObject.SetActive(false);
    }
    public void Awake()
    {
        //  gameObject.GetComponentInChildren<Image>().sprite = crntNoteData.noteSprite;
    }
    public void SecondInteract() { }

    public string SetDescriptiion()
    {
        return "жми";
    }
}

