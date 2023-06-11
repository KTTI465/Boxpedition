using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class CheckPointManager : MonoBehaviour
{
    [SerializeField, NonEditable]
    public Vector3 lastCheckPoint;

    [SerializeField]
    List<ResPoint> checkPoints;

    [SerializeField]
    List<Transform> objectTransforms;
    
    List<int> _checkPoint;
    List<int> _objects;
    List<TransformPR> _objectTransforms;

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

        _objects = new List<int>();
        _objectTransforms = new List<TransformPR>();
        foreach (var obTransform in objectTransforms)
        {
            _objects.Add(obTransform.GetInstanceID());
            _objectTransforms.Add(new TransformPR(obTransform.position, obTransform.rotation));
        }

        lastCheckPoint = playerObject.transform.position;
    }

    public void Respawn()
    {
        ////プレイヤー位置変更箇所
        playerObject.transform.position = lastCheckPoint;
    }

    public void ObRespawn(int id)
    {
        var target = GetObjectInstansID(id);
        objectTransforms[target].GetComponent<Rigidbody>().velocity = Vector3.zero;
        objectTransforms[target].position = _objectTransforms[target].Tposition;
        objectTransforms[target].rotation = _objectTransforms[target].Trotation;
    }

    public void SetPoint(int index)
    {
        Debug.Log(GetInstanceListNum(index));
        lastCheckPoint = checkPoints[GetInstanceListNum(index)].checkPoint.position;
    }

    int GetInstanceListNum(int id)
    {
        return _checkPoint.IndexOf(id);
    }

    int GetObjectInstansID(int id)
    {
        return _objects.IndexOf(id);
    }
}

class TransformPR
{
    public Vector3 Tposition;
    public Quaternion Trotation;

    public TransformPR(Vector3 position, Quaternion rotation)
    {
        Tposition = position;
        Trotation = rotation;
    }
}
