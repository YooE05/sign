using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlatformMovementController : MonoBehaviour
{
    [SerializeField] Camera puzzleCamera;
    [SerializeField] Camera mainCamera;

    [SerializeField] int gridX, gridY;//последняя координата в сетке, т.е. -1 от фактического количества

    [SerializeField] float unit;

    public bool canPressButton;
    [SerializeField] bool moveByX;

    private void Awake()
    {
        canPressButton = true;
    }
    private void Update()
    {
        if (countOfStoppedPl == countOfAllPl)
        {
            
            countOfAllPl = -1;


            Invoke("ReturnMovementToPlayer", 0.5f);


        }
    }

    void ReturnMovementToPlayer()
    {
        canPressButton = true;
        //вернуть камеру и движение игроку
        CharacterWalking.canMove = true;
        CharacterSight.canLook = true;
        puzzleCamera.enabled = false;
        mainCamera.enabled = true;
    }

    public int countOfStoppedPl = 0;
    public int countOfAllPl = -1;
    public void MovePlatform(List<Platform> platforms)
    {
        countOfStoppedPl = 0;
        countOfAllPl = platforms.Count;
        canPressButton = false;
        for (int i = 0; i < platforms.Count; i++)
        {
            MoveOn(platforms[i], platforms[i].offsetX);
        }

    }

    void MoveOn(Platform pl, float offset)
    {

        int compareEnd = pl.direction ? gridX : 0;
        offset = pl.direction ? offset : -offset;

        if ((pl.crntGridPosition.x + offset > compareEnd && compareEnd != 0) || (pl.crntGridPosition.x + offset < compareEnd && compareEnd == 0))
        {
            float off = compareEnd - pl.crntGridPosition.x;
            MoveItOMG(pl, off, () =>
             {
                 pl.direction = !pl.direction;

                 float remainOffset = Mathf.Abs(offset) - Mathf.Abs(off);
                 MoveOn(pl, remainOffset);
             });

        }
        else
        {
            MoveItOMG(pl, offset, () => { countOfStoppedPl++; });
        }


    }
    void MoveItOMG(Platform pl, float off, Action endCoroutine)
    {
        StartCoroutine(MoveTheFcknPlatformAlready(pl, off, () => { endCoroutine.Invoke(); }));
        // pl.gameObject.transform.localPosition = pl.gameObject.transform.localPosition + new Vector3(off, 0f, 0f) * unit;
        //pl.crntGridPosition.x += off;
    }

    [SerializeField] float delay;

    IEnumerator MoveTheFcknPlatformAlready(Platform pl, float off, Action endCoroutine)
    {
        //сменить камеру и остановить движение игроку

        CharacterWalking.canMove = false;
        CharacterSight.canLook = false;
        puzzleCamera.enabled = true;
        mainCamera.enabled = false;
        float remainTime = 0f;
        Vector3 startPos = pl.gameObject.transform.localPosition;
        Vector3 endPos;
        if (moveByX)
        {
            endPos = pl.gameObject.transform.localPosition + new Vector3(off, 0f, 0f) * unit;
        }
        else
        {
            endPos = pl.gameObject.transform.localPosition + new Vector3(0f, 0f, off) * unit;
        }


        while (remainTime < delay * Mathf.Abs(off))
        {

            remainTime += Time.deltaTime;

            pl.gameObject.transform.localPosition = Vector3.Lerp(startPos, endPos, remainTime / (delay * Mathf.Abs(off)));

            yield return null;


        }

        pl.gameObject.transform.localPosition = endPos;

        pl.crntGridPosition.x += off;
        endCoroutine.Invoke();

    }
}
