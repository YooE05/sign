using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public Vector2 startGridPosition;
    public Vector2 crntGridPosition;
    public int offsetX, offsetY;
    public Vector2 correctPosition;
    public bool isCorrectPos;

    public bool direction;//0- left 1 - right

    private void Awake()
    {
        direction = true;//Random.Range(0, 2)>0;
    }

    private void Start()
    {
        crntGridPosition = startGridPosition;
    }

}
