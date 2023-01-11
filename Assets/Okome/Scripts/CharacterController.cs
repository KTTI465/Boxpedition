using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    private Rigidbody rb;
    private float xMovement, zMovement;
    private float movementSpeed = 0.05f;
    public GameObject cam;
    private Quaternion cameraRot, characterRot;
    private float sensitivity = 1f;
    public float jumpPower;
    private float jumpDistance = 1.5f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cameraRot = cam.transform.localRotation;
        characterRot = transform.localRotation;
        cameraRot = Quaternion.Euler(0, 0, 0);
    }

    void Update()
    {
        CharacterMovement();
        CharacterRotate();
        CharacterJump();
    }

    private void CharacterMovement()
    {
        xMovement = Input.GetAxisRaw("Horizontal") * movementSpeed;
        zMovement = Input.GetAxisRaw("Vertical") * movementSpeed;

        transform.Translate(xMovement, 0, zMovement);
    }

    private void CharacterRotate()
    {
        //�}�E�X�̉������̓����~ sensitivity�ŉ������̉�]�������Ă���B
        float xRot = Input.GetAxis("Mouse X") * sensitivity;
        characterRot *= Quaternion.Euler(0, xRot, 0);
        transform.localRotation = characterRot;
    }

    private void CharacterJump()
    {
        //�L�����N�^�[�̒��S���牺�����Ƀ��C���o���B
        Vector3 jumpRay = transform.position + new Vector3(0.0f, 0.0f, 0.0f);
        Ray ray = new Ray(jumpRay, Vector3.down);
        //�L�����N�^�[�ƒn�ʂ����ȉ��̋����̎���true��Ԃ��B
        bool isGround = Physics.Raycast(ray, jumpDistance);
        if (Input.GetKeyDown(KeyCode.Space) && isGround == true)
        {
            rb.velocity = Vector3.up * jumpPower;
        }
    }
}
