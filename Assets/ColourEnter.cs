using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ColourEnter : MonoBehaviour
{
    [SerializeField] int comboLenth;
    [SerializeField] int countOfColors;
    [SerializeField] string fullCombo; //������� ��������� ���������
    string rightCombo;
    string crntCombo;
    [SerializeField] float blickDelay;
    [SerializeField] Animator mushroomsAnimator;
    [SerializeField] Animator doorAnimator;

    int stageNumber;
    bool isFirstBlick;

    [SerializeField] bool secondPuzzle;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.C)&& secondPuzzle)
        {
         
            stageNumber = 10;
            doorAnimator.SetInteger("OpenedDoor", stageNumber);
        }
    }

    private void Start()
    {

        isFirstBlick = true;
        stageNumber = 0;
        crntCombo = "";
        RegenerateFullCombo();
        rightCombo = fullCombo[0].ToString();
        StartCoroutine(ComboBlick(1));
    }

    void RegenerateFullCombo()
    {
        fullCombo = "";
        int rand = GetRandColourNum();
        for (int i = 0; i < comboLenth; i++)
        {
            fullCombo += rand;
            rand = GetRandColourNum();
            if (fullCombo.Length >= 2)
            {
                
                do { rand = GetRandColourNum(); }
                while (rand == Convert.ToInt32(fullCombo[i - 1].ToString())&& (Convert.ToInt32(fullCombo[i - 1].ToString()) == Convert.ToInt32(fullCombo[i].ToString())));

            }
        }


    }
    int GetRandColourNum()
    {
        return UnityEngine.Random.Range(1, countOfColors + 1);
    }
    void CheckUp()
    {
        if (crntCombo == rightCombo)
        {
            if (stageNumber == 0)
            {
                isFirstBlick = false;
            }
            stageNumber++;
            doorAnimator.SetInteger("OpenedDoor", stageNumber);

            Debug.Log("��������, ������� ������");

            ResetCombo();
            if (rightCombo == fullCombo)
            { }
            else
            {
                rightCombo = fullCombo.Substring(0, rightCombo.Length + 1);
                StartCoroutine(ComboBlick(rightCombo.Length));
            }
        }
        else
        {
            for (int i = 0; i < crntCombo.Length; i++)
            {
                if (crntCombo[i] != rightCombo[i]) { RestartGame(); }
            }

        }
    }

    public void AddNumber(string number)
    {
        crntCombo += number;
        CheckUp();
    }

    void ResetCombo()
    {
        crntCombo = "";
    }

    void RestartGame()
    {
        // Debug.Log("����� �� �����");

        //������� �����
        //����� ����� ������
        ResetCombo();
        isFirstBlick = true;
        stageNumber = 0;
        RegenerateFullCombo();
        rightCombo = fullCombo[0].ToString();
        StopAllCoroutines();
        StartCoroutine(ComboBlick(1));

    }

    IEnumerator ComboBlick(int countOfPassed)
    {
        string tempCombo = rightCombo.Substring(0, countOfPassed);
        string tempAnim = "";
        yield return new WaitForSeconds(3f);
        if (tempCombo.Length == 1)
        {
            while (isFirstBlick)
            {
                tempAnim = tempCombo[0].ToString();
                mushroomsAnimator.Play(tempAnim);
                yield return new WaitForSeconds(1.4f);
            }
            mushroomsAnimator.StopPlayback();
        }
        else
        {
            while (tempCombo.Length > 0)
            {
                tempAnim = tempCombo[0].ToString();
                tempCombo = tempCombo.Remove(0, 1);
                mushroomsAnimator.Play(tempAnim);

                yield return new WaitForSeconds(1.4f + blickDelay);
            }
        }
        if (rightCombo == fullCombo)
        { }
        else
        { //rightCombo = fullCombo.Substring(0, countOfPassed + 1); }



            //yield return new WaitForSeconds(2f);
        }

    }
}
