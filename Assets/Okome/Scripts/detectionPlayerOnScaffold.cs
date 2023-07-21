using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class detectionPlayerOnScaffold : MonoBehaviour
{
    [SerializeField]
    private GameObject player;

    [NonSerialized]
    public bool playerOnScaffold;

    private void Start()
    {
        playerOnScaffold = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerOnScaffold = true;
        }
    }
}