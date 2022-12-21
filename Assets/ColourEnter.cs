using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColourEnter : MonoBehaviour
{
    [SerializeField] int comboLenth;
    [SerializeField] int countOfColors;
    [SerializeField] string fullCombo; //сделать генерацию рандомной
    string rightCombo;
    string crntCombo;
    [SerializeField] float blickDelay;
    [SerializeField] Animator mushroomsAnimator;
    [SerializeField] Animator doorAnimator;

    int stageNumber;
    bool isFirstBlick;

    private void Start()
    {
        isFirstBlick = true;
        stageNumber = 0;
        crntCombo = "";
        rightCombo = fullCombo[0].ToString();
        StartCoroutine(ComboBlick(1));
    }

   /* void GenerateCombo()
    {

    }*/
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

            Debug.Log("открывай, медведь пришёл");

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
        // Debug.Log("давай по новой");
       
        //закрыть двери
        //пусть комбо заново
        ResetCombo();
        isFirstBlick = true;
        stageNumber = 0;
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
