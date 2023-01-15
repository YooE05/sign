using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlatformButton : MonoBehaviour, IInteractable
{
    [SerializeField] PlatformMovementController movementController;

    public bool interactOnlyInMinigame { get => false; }


    public List<Platform> platformsToMove = new List<Platform>();
    public void Interact()
    {
        if (movementController.canPressButton)
        {
            movementController.MovePlatform(platformsToMove);
        }
    }
    public void SecondInteract() { }

    public string SetDescriptiion()
    {
        if (movementController.canPressButton)
        { return "����"; }
        else
        { return ""; }
    }
}
