using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColourButton : MonoBehaviour, IInteractable
{
    [SerializeField] ColourEnter colourEnter;
    [SerializeField] string keyToCombo;

    public bool interactOnlyInMinigame { get => false; }
    public void Interact()
    {
        colourEnter.AddNumber(keyToCombo);
    }
    public void SecondInteract(){ }
    public string SetDescriptiion()
    {
        return "זלט";
    }
}
