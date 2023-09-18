using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class disableJumpAreaManager : MonoBehaviour
{
    [SerializeField]
    private disableJumpGravity upperArea;

    [SerializeField]
    private disableJumpGravity underArea;

    CharacterController characterController;
    private void Start()
    {
        characterController = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController>();
    }

    private void Update()
    {

        if (upperArea.isOn && underArea.isOn)
        {
            if (upperArea.isOnSecond == true && characterController.isGround == true)
            {
                characterController.isUsingJumpGravity = true;
                upperArea.isOn = false;
                underArea.isOn = false;
                upperArea.isOnSecond = false;
            }
            else
                characterController.isUsingJumpGravity = false;
        }
        else if ((upperArea.isOn == false || underArea.isOn == false) && characterController.isGround == true)
        {
            characterController.isUsingJumpGravity = true;
        }
    }
}

