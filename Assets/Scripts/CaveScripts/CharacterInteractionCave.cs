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
            //Debug.Log(hit.collider.gameObject.name);
            IInteractable interactableObj = hit.collider.gameObject.GetComponent<IInteractable>();

            if (interactableObj != null)
            {

                if (!interactableObj.interactOnlyInMinigame && !GameControllerCave.journalIsActive)
                {
                    if (hit.collider.tag == "hint" && GameControllerCave.ultraIsActive || hit.collider.tag != "hint")
                    {
                        isHit = true;
                        if (Input.GetKeyDown(KeyCode.E))
                        {
                            interactableObj.Interact();
                            interactableObj.SetDescriptiion();
                        }
                    }
                }
            }
        }
        E.SetActive(isHit);
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
