using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveBlock : MonoBehaviour
{
    void Start()
    {
    }

    void Update()
    {
        transform.Rotate(0f, 0.1f, 0f);
    }
}
