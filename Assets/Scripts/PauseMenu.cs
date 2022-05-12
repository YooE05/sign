using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool gameIsPaused = false;
    public GameObject[] pauseMenuUI;
    public GameObject aim;
    public CharacterSight characterSight;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }
   public void Resume()
    {
        foreach (var UIPanel in pauseMenuUI)
        {
            UIPanel.SetActive(false);
        }

        //PauseMenuUI.SetActive(false);
        aim.SetActive(true);
        Time.timeScale = 1f;
        gameIsPaused =false;
        characterSight.canLook = true;
        Cursor.visible = false;
    }
    void Pause()
    {
        pauseMenuUI[0].SetActive(true);
        aim.SetActive(false);
        characterSight.canLook = false;
        Time.timeScale = 0f;
        gameIsPaused = true;
        Cursor.visible = true;
    }
}
