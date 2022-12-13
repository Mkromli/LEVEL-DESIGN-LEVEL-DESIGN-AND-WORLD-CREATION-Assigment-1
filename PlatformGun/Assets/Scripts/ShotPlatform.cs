using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotPlatform : MonoBehaviour
{
    private Rigidbody rb;
    public float TimeBeforeDelete = 5;
        
    // Start is called before the first frame update
    void Awake()
    {
        Destroy(gameObject, TimeBeforeDelete);
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(new Vector3(9.8f * 25f,9.8f * 25f,9.8f * 25f));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
