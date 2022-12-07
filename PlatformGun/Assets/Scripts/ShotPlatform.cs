using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotPlatform : MonoBehaviour
{

    public float TimeBeforeDelete = 5; 
    // Start is called before the first frame update
    void Awake()
    {
        Destroy(gameObject, TimeBeforeDelete);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
