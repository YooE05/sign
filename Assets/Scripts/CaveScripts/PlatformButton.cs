using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlatformButton : MonoBehaviour, IInteractable
{
    PlatformMovementController movementController;
    private void Awake()
    {
        movementController = FindObjectOfType<PlatformMovementController>();
    }


    public List<Platform> platformsToMove = new List<Platform>();
    public void Interact()
    {
        if (movementController.canPressButton)
        {
            movementController.MovePlatform(platformsToMove);
        }
    }
    public string SetDescriptiion()
    {
        if (movementController.canPressButton)
        { return "Жать"; }
        else
        { return ""; }
    }
}
