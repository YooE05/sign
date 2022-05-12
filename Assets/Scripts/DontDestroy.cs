using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
        if (FindObjectsOfType<AudioSource>().Length>1)
        { Destroy(this.gameObject); }
    }
}
