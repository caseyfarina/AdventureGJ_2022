using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class autoFireProjectile : MonoBehaviour
{
    public GameObject projectile;
    public float fireRate = 3f;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("autoFire", fireRate,fireRate);
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    void autoFire()
    {
        Instantiate(projectile, transform.position, transform.rotation);
    }
}
