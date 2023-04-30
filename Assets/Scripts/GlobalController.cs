using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalController : MonoBehaviour
{
    public float musicVolume = 1f;
    public float sfxVolume = 1f;

    void Start()
    {
        if (GameObject.FindGameObjectsWithTag("Singleton").Length > 1)
            Destroy(gameObject);
        DontDestroyOnLoad(this.gameObject);
    }
}
