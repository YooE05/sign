using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    public bool interactOnlyInMinigame { get;  }
    public void Interact();

    public void SecondInteract();
    public string SetDescriptiion();
}
