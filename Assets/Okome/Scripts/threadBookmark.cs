using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class threadBookmark : MonoBehaviour
{
    [SerializeField]
    private LineRenderer line = null;

    [SerializeField]
    private Transform pivot = null;


    public void Update()
    {
        // ヒモ部分の始点と終点の座標を更新
        line.SetPosition(0, this.transform.position);
        line.SetPosition(1, this.pivot.position);
    }
}
