using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeStart : MonoBehaviour
{
    [SerializeField] Animator doorAnimator;

    [SerializeField] Compas compass;

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag=="Player")
        {
            StartMazePuzzle();
        }
    }

    void StartMazePuzzle()
    {
        //пугающий звук
        //звук закрытия двери

        //закрыть дверь
        doorAnimator.SetBool("doorIsOpen",false);
        GetComponent<Collider>().enabled = false;
        //сменить освещение
        RenderSettings.ambientLight = Color.black;
        RenderSettings.reflectionIntensity = 0;
        //вывести текст
        FindObjectOfType<GameControllerCave>().ShowText("what the..", 1f);
        // FindObjectOfType<GameControllerCave>().ShowText("", 2f);
        //сменить режим для компаса
        compass.isInMaze = true;


    }



}
