using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class scenes : MonoBehaviour
{

    public void ChangeScene(int numberScenes)
    {
        SceneManager.LoadScene(numberScenes);
    }
    public void quit()
    {
        Application.Quit();
    }


}
