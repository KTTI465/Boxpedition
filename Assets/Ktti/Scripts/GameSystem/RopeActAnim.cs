using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeActAnim : MonoBehaviour
{
    [SerializeField]
    Transform up_transform;
    [SerializeField]
    Transform down_transform;

    [SerializeField]
    warpRope warpRope;
    [SerializeField]
    Animator animator;

    CharacterController characterController;

    [SerializeField, NonEditable]
    bool grabRope = false;

    [SerializeField]
    GameObject climbInteractImage;

    void Start()
    {
        characterController = warpRope.player.gameObject.GetComponent<CharacterController>();
    }

    void Update()
    {
        
    }
}
