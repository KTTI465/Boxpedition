using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.VisualScripting;

public class camera : MonoBehaviour
{
    public GameObject playerObject;         //追尾 オブジェクト
    public Vector2 rotationSpeed;           //回転速度

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

        // ゲームパッドが接続されていないとき
        if (Gamepad.current == null)
        {
            transform.RotateAround(playerObject.transform.position, transform.right, -rotY * cal);
        }
        else
        {
            // 右スティックの入力を受け取る
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

