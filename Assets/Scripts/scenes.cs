using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class scenes : MonoBehaviour
{

    public void ChangeScene(int numberScenes)
    {
        if(numberScenes==0)
        { PauseMenu.gameIsPaused = false; }
        SceneManager.LoadScene(numberScenes);
    }
    public void quit()
    {
        Application.Quit();
    }


}
