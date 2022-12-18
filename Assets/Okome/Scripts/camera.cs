using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour
{
    public GameObject playerObject;         //�ǔ� �I�u�W�F�N�g
    public Vector2 rotationSpeed;           //��]���x
    private Vector3 lastTargetPosition;     //�Ō�̒ǔ��I�u�W�F�N�g�̍��W

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

