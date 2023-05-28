using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckPointManager : MonoBehaviour
{
    [SerializeField, NonEditable]
    public Vector3 lastCheckPoint;
    [SerializeField]
    ResPoint[] _checkPoint;
    [SerializeField]
    static int _pointIndex;
    [SerializeField]
    GameObject playerObject;

    static CheckPointManager _lastmanage;

    public static CheckPointManager Get()
    {
        if (_lastmanage == null)
        {
            _lastmanage = FindObjectOfType<CheckPointManager>();
            if (_lastmanage == null)
            {
                return null;
            }
        }
        return _lastmanage;
    }

    void Awake()
    {
        StartPoint(_pointIndex);
    }

    public void Respawn()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        StartPoint(_pointIndex);
        Debug.Log($"Check Point:{_pointIndex} SPAWN");
    }

    void StartPoint(int index)
    {
        if (index >= _checkPoint.Length)
        {
            return;
        }

        ////プレイヤー位置変更箇所
    }

    public void SetPoint(int index)
    {
        _pointIndex = index;
        lastCheckPoint = _checkPoint[index].checkPoint.position;
    }
}
