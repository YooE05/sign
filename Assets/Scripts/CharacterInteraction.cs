using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterInteraction : MonoBehaviour
{

    List<GameObject> enterGO = new List<GameObject>();
    List<GameObject> deletedGO = new List<GameObject>();
    [SerializeField] GameObject E;

    void Update()
    {
        if (enterGO.Count > 0)
        {
            E.SetActive(true);
        }
        else
        {
            E.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {

            foreach (GameObject obj in enterGO)
            {
                Interaction objInt = obj.GetComponent<Interaction>();
                deletedGO.Add(obj);
               //obj.GetComponent<MeshRenderer>().material.color = new Color(255, 255, 255);
            }
            foreach (GameObject obj in deletedGO)
            {
                if(enterGO.Contains(obj))
                { enterGO.Remove(obj);
                Destroy(obj); }
                  
                //obj.GetComponent<MeshRenderer>().material.color = new Color(255, 255, 255);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Interaction>() != null)
        {
            if (other.gameObject.GetComponent<Interaction>().IsInteractable)
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
