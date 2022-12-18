using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    private float xMovement, zMovement;
    private float movementSpeed = 0.05f;
    public GameObject cam;
    private Quaternion cameraRot, characterRot;
    private float sensitivity = 1f;

    void Start()
    {
        cameraRot = cam.transform.localRotation;
        characterRot = transform.localRotation;
        cameraRot = Quaternion.Euler(0, 0, 0);
    }

    void Update()
    {
        CharacterMovement();
        CharacterRotate();
    }

    private void CharacterMovement()
    {
        xMovement = Input.GetAxisRaw("Horizontal") * movementSpeed;
        zMovement = Input.GetAxisRaw("Vertical") * movementSpeed;

        transform.Translate(xMovement, 0, zMovement);
    }

    private void CharacterRotate()
    {
        float xRot = Input.GetAxis("Mouse X") * sensitivity;
        characterRot *= Quaternion.Euler(0, xRot, 0);
        transform.localRotation = characterRot;
    }
}
