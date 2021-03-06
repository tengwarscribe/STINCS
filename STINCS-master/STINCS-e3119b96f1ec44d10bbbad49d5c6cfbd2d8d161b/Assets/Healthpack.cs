﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healthpack : MonoBehaviour

{



    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Health otherProjectile = collision.gameObject.GetComponent<Health>();

            Debug.Log("I am colliding with another object!");

            if (otherProjectile.hitPoints <= 80)
            {
                otherProjectile.hitPoints += 20;
            }

            else if (otherProjectile.hitPoints > 80)
            {
                otherProjectile.hitPoints = 100;
            }

            Destroy(gameObject);
        }
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}