using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartNoteGame : MonoBehaviour, IInteractable
{
    [SerializeField] NoteGameController noteGameController;

    bool isEntered = false;
    public bool interactOnlyInMinigame
    {
        get => isEntered;

    }
    public void Interact()
    {
        SwitchGameView();
    }

    public void SwitchGameView()
    {
        isEntered = !isEntered;
        noteGameController.EnterMinigame();
       
    }

    public void SecondInteract() { }
    public string SetDescriptiion() { return "взаимодействовать"; }

}
