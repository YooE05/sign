using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "NoteGame/NoteData")]
public class Note : ScriptableObject
{

    public Sprite noteSprite;
    public int rotationPos;

    public int needPlaceNum;
    public int needRotationNum;

}
