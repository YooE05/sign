using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class CharacterInteractionCave : MonoBehaviour
{

    List<GameObject> enterGO = new List<GameObject>();
    List<GameObject> deletedGO = new List<GameObject>();
    [SerializeField] GameObject E;
    TextMeshProUGUI interactionDiscription;

    [SerializeField] Camera mainCam;

    Vector3 Ray_start_position;

    public float intDistance = 10f;

    private void Start()
    {
        interactionDiscription = E.GetComponent<TextMeshProUGUI>();
        Ray_start_position = new Vector3(Screen.width / 2, Screen.height / 2, 0);
    }
    void Update()
    {
        Ray ray = mainCam.ScreenPointToRay(Ray_start_position);
        RaycastHit hit;
        bool isHit = false;

        Debug.DrawRay(ray.origin, ray.direction * intDistance, Color.blue);

        if (Physics.Raycast(ray, out hit, intDistance))
        {
            IInteractable interactableObj = hit.collider.gameObject.GetComponent<IInteractable>();

            if (interactableObj != null)
            {
                isHit = true;
                if (Input.GetKeyDown(KeyCode.E))
                {
                    interactableObj.Interact();
                    interactableObj.SetDescriptiion();
                }
            }


        }
        E.SetActive(isHit);
        /*
        if (enterGO.Count > 0 )
        {
            E.SetActive(true);
        }
        else
        {
            E.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.E) && E.activeSelf)
        {

            foreach (GameObject obj in enterGO)
            {
                Interaction objInt = obj.GetComponent<Interaction>();

                if (obj.tag == "hint")
                {

                }
                deletedGO.Add(obj);
                //obj.GetComponent<MeshRenderer>().material.color = new Color(255, 255, 255);
            }
            foreach (GameObject obj in deletedGO)
            {
                if (enterGO.Contains(obj))
                {
                    enterGO.Remove(obj);
                    Destroy(obj);
                }

                //obj.GetComponent<MeshRenderer>().material.color = new Color(255, 255, 255);
            }
        }
*/
    }
    public void StopShowText()
    {
        StopCoroutine("ShowAText");
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
