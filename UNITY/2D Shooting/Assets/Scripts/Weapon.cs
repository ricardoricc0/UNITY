﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Shot()
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }
}
