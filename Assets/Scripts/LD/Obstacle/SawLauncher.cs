using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawLauncher : MonoBehaviour
{
    public GameObject prefabSmallSaw;
    private GameObject currentSaw;

    public void SpawnSaw()
    {
        currentSaw = (GameObject)Instantiate(prefabSmallSaw, transform.position, Quaternion.identity);
    }

    public void ShootSaw()
    {
        if (currentSaw != null)
        {
            currentSaw.GetComponent<ShootedSawController>().Shoot(transform.right);
            currentSaw = null;
        }
    }
}
