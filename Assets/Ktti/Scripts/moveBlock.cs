using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveBlock : MonoBehaviour
{
    public Transform[] _pos;
    Transform pos;

    public int pos_num;

    public float speed = 0.05f;

    void Start()
    {
        pos = _pos[0];
    }

    void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, pos.position, speed);

        if (pos.position == transform.position)
        {
            pos_num++;

            if (pos_num >= _pos.Length)
            {
                pos_num = 0;
            }

            pos = _pos[pos_num];
        }
    }
}
