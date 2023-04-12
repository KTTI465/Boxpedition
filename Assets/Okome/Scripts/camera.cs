using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.VisualScripting;

public class camera : MonoBehaviour
{
    public GameObject playerObject;         //�ǔ� �I�u�W�F�N�g
    public Vector2 rotationSpeed;           //��]���x

    void Update()
    {
        Rotate();
    }

    void Rotate()
    {
        //float cal = PlayerPrefs.GetFloat("Sensi");
        float cal = 1f;

        float rotY = Input.GetAxis("Mouse Y") * rotationSpeed.y;

        if (transform.forward.y * -1f > 0.8f && rotY < 0)
        {
            rotY = 0;
        }
        if (transform.forward.y * -1f < -0.8f && rotY > 0)
        {
            rotY = 0;
        }

        // �Q�[���p�b�h���ڑ�����Ă��Ȃ��Ƃ�
        if (Gamepad.current == null)
        {
            transform.RotateAround(playerObject.transform.position, transform.right, -rotY * cal);
        }
        else
        {
            // �E�X�e�B�b�N�̓��͂��󂯎��
            var v = Gamepad.current.rightStick.ReadValue();

            if (transform.forward.y * -1f > 0.8f && v.y < 0)
            {
                v.y = 0;
            }
            if (transform.forward.y * -1f < -0.8f && v.y > 0)
            {
                v.y = 0;
            }

            if (rotY == 0)
            {
                transform.RotateAround(playerObject.transform.position, transform.right, -v.y * 0.5f * cal);
            }
            else
            {
                transform.RotateAround(playerObject.transform.position, transform.right, -rotY * cal);
            }
        }
    }

}

