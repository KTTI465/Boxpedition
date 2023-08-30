using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class detectionPlayerOn : MonoBehaviour
{
    private GameObject player;

    [NonSerialized]
    public bool isPlayerOn;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        isPlayerOn = false;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            isPlayerOn = true;
        }
    }
}