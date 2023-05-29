using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResPoint : MonoBehaviour
{
    public Transform checkPoint;

    [SerializeField, NonEditable]
    int index_p;

    void Awake()
    {
        index_p = GetInstanceID();
    }

    public int GetID()
    {
        return index_p;
    }
}
