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

    private GameObject Parent;
    private Vector3 Position;
    private RaycastHit Hit;
    private float Distance;
    public LayerMask Mask;
    private bool avoidWall;

    void Start()
    {
        Parent = transform.root.gameObject;

        Position = transform.localPosition;

        Distance = Vector3.Distance(Parent.transform.position, transform.position);

    }
    void Update()
    { 
        Rotate();
        AvoidWall();
    }

    private void FixedUpdate()
    {


    }

    void Rotate()
    {
        //float cal = PlayerPrefs.GetFloat("Sensi");
        float cal = 1f;
        transform.LookAt(Parent.transform);
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

    void AvoidWall()
    {
        if (avoidWall == false)
        {
            Position = transform.localPosition;
        }

        if (Physics.SphereCast(Parent.transform.position, 1, (transform.position - Parent.transform.position).normalized, out Hit, Distance, Mask))
        {      
            transform.position = Parent.transform.position + (transform.position - Parent.transform.position).normalized * Hit.distance;
            avoidWall = true;
        }
        else
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, Position, 2);
            if (avoidWall == true)
            {
                avoidWall = false;
            }
        }
    }
}



