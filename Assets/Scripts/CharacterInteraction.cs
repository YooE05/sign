using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharacterInteraction : MonoBehaviour
{

    List<GameObject> enterGO = new List<GameObject>();
    List<GameObject> deletedGO = new List<GameObject>();
    [SerializeField] GameObject E;
    [SerializeField] GameObject endBaner;
    [SerializeField] GameObject sun;
    [SerializeField] GameObject caveRock;
    [SerializeField] GameObject trailerAreaCollider;
    [SerializeField] GameObject[] hints;
    [SerializeField] GameObject[] stikers;
    public bool compassAdded = false;
    [SerializeField] bool canCheckHint = false;
    [SerializeField] bool hasHintInList = false;
    int currentStickers;

    [SerializeField] Material firstSky;
    [SerializeField] Material secondSky;
    Coroutine crntCorSun;
    [SerializeField] AudioSource endSound;
    Vector3 Ray_start_position;

    bool lastHintChecked = true;
    private void Start()
    {
        crntCorSun = StartCoroutine(sunRotate(Quaternion.Euler(22, 35, 0), 60f));
        Ray_start_position = new Vector3(Screen.width / 2, Screen.height / 2, 0);
    }
    void Update()
    {
        Ray ray = FindObjectOfType<GameController>().cam.ScreenPointToRay(Ray_start_position);

        if (currentStickers >= 3)
        {
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Debug.Log(hit.collider.tag);
                if (hit.collider.tag == "hint")
                { lastHintChecked = true; }
                else { lastHintChecked = false; }
            }

        }

        if (enterGO.Count > 0 && (GameController.ultraIsActive && hasHintInList && lastHintChecked || !hasHintInList))
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
                if (obj.tag == "Finish")
                {
                    StartCoroutine(EndGame());
                }
                if (obj.tag == "compass")
                {
                    compassAdded = true;
                    trailerAreaCollider.SetActive(false);
                    //StopShowText();

                    //FindObjectOfType<GameController>().StopShowingText();
                    FindObjectOfType<GameController>().ShowText("Press 'M' to use the map and 'V' to use the visor", 3f);
                }
                if (obj.tag == "hint")
                {
                    GotNextHint();
                    hasHintInList = false;
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


    }
    public void StopShowText()
    {
        StopCoroutine("ShowAText");
    }

    private void GotNextHint()
    {
        currentStickers++;
        if (currentStickers < 4)
        {
            hints[currentStickers].SetActive(true);
        }
        else
        { caveRock.SetActive(false); }

        stikers[currentStickers].SetActive(true);
        if (currentStickers == 1)
        {
            FindObjectOfType<GameController>().ShowText("Another symbol.. What does it mean", 5f);
        }
        if (currentStickers == 2)
        {
            FindObjectOfType<GameController>().ShowText("Weird", 3f);
        }

        if (currentStickers == 3)
        {
            FindObjectOfType<GameController>().ShowText("Hmm..", 3f);
            //увеличить размер коллайдера камеры
            gameObject.GetComponent<BoxCollider>().center = new Vector3(0, 0, 36.1f);
            gameObject.GetComponent<BoxCollider>().size = new Vector3(0, 0, 72.6f);
        }
        if (currentStickers == 4)
        {
            FindObjectOfType<GameController>().ShowText("The next one", 3f);
            //уменьшить размер коллайдера камеры
            gameObject.GetComponent<BoxCollider>().center = new Vector3(0, 0, 1.2f);
            gameObject.GetComponent<BoxCollider>().size = new Vector3(0, 0, 1.035f);
        }
        StopCoroutine(crntCorSun);
        crntCorSun = StartCoroutine(sunRotate(Quaternion.Euler(18 + currentStickers * 37, 35, 0), 60f));
        //if(crntCorSky!=null)
        RenderSettings.skybox.Lerp(firstSky, secondSky, currentStickers * 0.2f);
        /*{StopCoroutine(crntCorSky); }
        crntCorSky =StartCoroutine(skuboxChange(10f));*/
        //звук карандаша
        gameObject.GetComponent<AudioSource>().Play();
        //текст
        //начать менять скайбокс
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Interaction>() != null)
        {
            if (other.gameObject.GetComponent<Interaction>().IsInteractable)
            {
                enterGO.Add(other.gameObject);
                if (other.gameObject.tag == "hint")
                { hasHintInList = true; }
            }
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "hint")
        { hasHintInList = false; }
        enterGO.Remove(other.gameObject);
    }

    IEnumerator sunRotate(Quaternion targetRotation, float duration)
    {
        float time = 0;
        Quaternion startRotation = sun.transform.localRotation;

        while (time < duration)
        {
            sun.transform.localRotation = Quaternion.Lerp(startRotation, targetRotation, time / duration);

            time += Time.deltaTime;
            yield return null;
        }

        sun.transform.localRotation = targetRotation;
    }
    IEnumerator skyboxChange(float duration)
    {
        float delta = 0.02f;
        float k = 0;
        while (k < 1)
        {
            RenderSettings.skybox.Lerp(firstSky, firstSky, currentStickers * 0.2f * k);
            k += delta;
            yield return new WaitForSeconds(delta);
        }
        RenderSettings.skybox.Lerp(firstSky, firstSky, 0f);

        /* float time =0f;
         do
         {
             RenderSettings.skybox.Lerp(firstSky, firstSky, currentStickers * 0.2f * (time / duration));
             time += Time.deltaTime;
             yield return null;
         }
         while (time < duration);*/

        /* float state = ;
         //float delta =  / duration/100;
         float time = 0;
         while (time < state)
         {
             RenderSettings.skybox.Lerp(firstSky, secondSky, time);

             // time += Time.deltaTime;
             // state += delta;
             time += 0.001f;
              yield return new WaitForSeconds( 0.001f);
         }*/

        //RenderSettings.skybox.Lerp(firstSky, secondSky, currentStickers * 0.2f);
    }


    IEnumerator EndGame()
    { //звук конца
        endSound.Play();
        //показать банер
        endBaner.SetActive(true);

        yield return new WaitForSeconds(5f);
        Cursor.visible = true;
        SceneManager.LoadScene(0);
    }
}
