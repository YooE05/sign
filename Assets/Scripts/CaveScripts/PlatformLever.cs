using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlatformLever : MonoBehaviour, IInteractable
{
    [SerializeField] PlatformButton button;

    [Header("Stage1")]
    [SerializeField] List<Platform> firstStPlatforms = new List<Platform>();
    [SerializeField] int[] firstPlOffsets;

    [Header("Stage2")]
    [SerializeField] List<Platform> secStPlatforms = new List<Platform>();
    [SerializeField] int[] secondPlOffsets;


    bool isFirstStage;

    private void Awake()
    {
        isFirstStage = true;

        for (int i = 0; i < firstStPlatforms.Count; i++)
        {
            firstStPlatforms[i].offsetX = firstPlOffsets[i];
        }

        button.platformsToMove = firstStPlatforms;
    }


    public void Interact()
    {
        if (isFirstStage)
        {
            for (int i = 0; i < secStPlatforms.Count; i++)
            {
                secStPlatforms[i].offsetX = secondPlOffsets[i];
            }
            button.platformsToMove = secStPlatforms;
        }
        else
        {
            for (int i = 0; i < firstStPlatforms.Count; i++)
            {
                firstStPlatforms[i].offsetX = firstPlOffsets[i];
            }
            button.platformsToMove = firstStPlatforms;
        }
        isFirstStage = !isFirstStage;

    }
    public string SetDescriptiion()
    {
        return "Потянуть";
    }
}
