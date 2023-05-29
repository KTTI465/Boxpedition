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
    List<ResPoint> checkPoints;
    
    List<int> _checkPoint;

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

    void Start()
    {
        _checkPoint = new List<int>();
        foreach (var point in checkPoints)
        {
            _checkPoint.Add(point.GetID());
        }


        lastCheckPoint = playerObject.transform.position;
    }

    public void Respawn()
    {
        ChengePoint();
        //Debug.Log($"Check Point:{_pointIndex} SPAWN");
    }

    void ChengePoint()
    {
        ////プレイヤー位置変更箇所
        playerObject.transform.position = lastCheckPoint;
    }

    public void SetPoint(int index)
    {
        _pointIndex = index;
        Debug.Log(GetInstanceListNum(index));
        lastCheckPoint = checkPoints[GetInstanceListNum(index)].checkPoint.position;
    }

    int GetInstanceListNum(int id)
    {
        return _checkPoint.IndexOf(id);
    }
}
