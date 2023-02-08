using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour
{
    public GameObject playerObject;         //追尾 オブジェクト
    public Vector2 rotationSpeed;           //回転速度
    private Vector3 lastTargetPosition;     //最後の追尾オブジェクトの座標

    void Update()
    {
        Rotate();
    }

    void Rotate()
    {
        float rotY = Input.GetAxis("Mouse Y") * rotationSpeed.y;

        //transform.RotateAround(playerObject.transform.position, Vector3.up, newAngle.x);
        if (transform.forward.y * -1f > 0.8f && rotY < 0)
        {
            rotY = 0;
        }
        if (transform.forward.y * -1f < -0.8f && rotY > 0)
        {
            rotY = 0;
        }
        transform.RotateAround(playerObject.transform.position, transform.right, -rotY);
    }

}

