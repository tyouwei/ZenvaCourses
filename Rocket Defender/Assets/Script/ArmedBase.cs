using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmedBase : Base
{
    [SerializeField]
    private Rocket rocket;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Fire();
        }
    }
    void Fire()
    {
        Rocket rocketInstance = Instantiate(rocket);
        rocketInstance.Launch(Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position, 5f);
    }
}
