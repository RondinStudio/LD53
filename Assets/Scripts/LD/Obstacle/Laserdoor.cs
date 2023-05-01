using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laserdoor : MonoBehaviour
{

    public GameObject laserTilemap;
    public GameObject laserCollisionBox;

    public void SetEnabled(bool enabled)
    {
        laserTilemap.SetActive(enabled);
        laserCollisionBox.SetActive(enabled);
    }

    // Start is called before the first frame update
    void Start()
    {
        this.SetEnabled(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
