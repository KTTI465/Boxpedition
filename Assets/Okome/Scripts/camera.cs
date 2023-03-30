using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        float rotY = Input.GetAxis("Mouse Y") * rotationSpeed.y;
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

