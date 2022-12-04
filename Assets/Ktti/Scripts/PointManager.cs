using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointManager : MonoBehaviour
{
    //ポイント格納用変数
    [SerializeField, NonEditable]
    int score = 0;
    
    //呼び出し用変数
    static PointManager _manage;

    //外部からの呼び出し用
    public static PointManager Get()
    {
        if (_manage == null)
        {
            _manage = FindObjectOfType<PointManager>();
            if (_manage == null)
            {
                return null;
            }
        }
        return _manage;
    }

    //ポイントのセッター
    public void AddPoint(int index)
    {
        score += index;
    }

    //ポイントのゲッター
    public int GetPoint()
    {
        return score;
    }
}
