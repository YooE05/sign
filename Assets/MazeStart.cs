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
        //�������� ����
        //���� �������� �����

        //������� �����
        doorAnimator.SetBool("doorIsOpen",false);
        GetComponent<Collider>().enabled = false;
        //������� ���������
        RenderSettings.ambientLight = Color.black;
        RenderSettings.reflectionIntensity = 0;
        //������� �����
        FindObjectOfType<GameControllerCave>().ShowText("what the..", 1f);
        // FindObjectOfType<GameControllerCave>().ShowText("", 2f);
        //������� ����� ��� �������
        compass.isInMaze = true;


    }



}
