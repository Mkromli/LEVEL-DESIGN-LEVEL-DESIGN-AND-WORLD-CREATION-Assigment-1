using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGun : MonoBehaviour
{
    public Transform projectileSpawnPoint;
    public GameObject projectilePrefab;
    public float projectileSpeed = 2; 
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            var projectile = Instantiate(projectilePrefab, projectileSpawnPoint.position, projectileSpawnPoint.rotation);
            projectile.GetComponent<Rigidbody>().velocity = projectileSpawnPoint.forward * projectileSpeed;
        }
            
    }
}
