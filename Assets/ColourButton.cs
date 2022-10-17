using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColourButton : MonoBehaviour, IInteractable
{
    [SerializeField] ColourEnter colourEnter;
    [SerializeField] string keyToCombo;
    public void Interact()
    {
        colourEnter.AddNumber(keyToCombo);
    }
    public string SetDescriptiion()
    {
        return "זלט";
    }
}
