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
    // Start is called before the first frame update
    void Start()
    {
        cameraRot = cam.transform.localRotation;
        characterRot = transform.localRotation;
        cameraRot = Quaternion.Euler(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        CharacterMovement();
        ViewController();
    }

    private void CharacterMovement()
    {
        xMovement = Input.GetAxisRaw("Horizontal") * movementSpeed;
        zMovement = Input.GetAxisRaw("Vertical") * movementSpeed;

        transform.Translate(xMovement, 0, zMovement);
    }

    private void ViewController()
    {
        float xRot = Input.GetAxis("Mouse X") * sensitivity;
        float yRot = Input.GetAxis("Mouse Y") * sensitivity;

        cameraRot *= Quaternion.Euler(-yRot, 0, 0);
        characterRot *= Quaternion.Euler(0, xRot, 0);

        cameraRot = ClampRotation(cameraRot);

        cam.transform.localRotation = cameraRot;
        transform.localRotation = characterRot;
    }

    private Quaternion ClampRotation(Quaternion q)
    {
        q.x /= q.w;
        q.y /= q.w;
        q.z /= q.w;
        q.w = 1f;

        float angleX = Mathf.Atan(q.x) * Mathf.Rad2Deg * 2f;
        angleX = Mathf.Clamp(angleX, -90f, 90f);
        q.x = Mathf.Tan(angleX * Mathf.Deg2Rad * 0.5f);

        return q;
    }
}
