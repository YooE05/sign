using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterInteract : MonoBehaviour
{
    List<GameObject> enterGO = new List<GameObject>();
    [SerializeField] GameObject UIElement;

    void Update()
    {
        if (enterGO.Count > 0)
        {
            UIElement.SetActive(true);
        }
        else
        {
            UIElement.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            foreach (GameObject obj in enterGO)
            {
                InteractionCheck objInt = obj.GetComponent<InteractionCheck>();
                obj.GetComponent<MeshRenderer>().material.color = new Color(255, 0, 0);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<InteractionCheck>() != null)
        {
            if (other.gameObject.GetComponent<InteractionCheck>().IsInteractable)
            {
                enterGO.Add(other.gameObject);
            }
        }

    }
    private void OnTriggerExit(Collider other)
    {
        enterGO.Remove(other.gameObject);
    }
}
