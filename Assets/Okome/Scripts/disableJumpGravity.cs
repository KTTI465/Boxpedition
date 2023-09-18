using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class disableJumpGravity : MonoBehaviour
{
    private GameObject player;
    CharacterController characterController;

    [System.NonSerialized]
    public bool isOn = false;

    [System.NonSerialized]
    public bool isOnSecond = false;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        characterController = player.GetComponent<CharacterController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            if (isOn == true)
            {
                isOnSecond = true;
            }
            else
                isOn = true;
        }
    }
}

