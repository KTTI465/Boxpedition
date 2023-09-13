using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class guideArrow : MonoBehaviour
{
    //目的のオブジェクトを格納
    [System.NonSerialized]
    public Transform target;

    [SerializeField]
    private GameObject cameraObj;

    [SerializeField]　//矢印の最大振幅
    private float maxAmplitude;

    [SerializeField]　//矢印の動きの速さ
    private float arrowSpeed;

    [SerializeField]　//目標と矢印の最小振幅時の、最短距離
    private float minDistArrowFromTarget;

    [SerializeField]　//目標オブジェクトからの高さの座標
    private float heightFromTarget;

    //周期
    private float period;

    //直線と目標オブジェクトの垂線の足の座標
    private Vector3 perpendicularCoordinates;

    //矢印の最初の大きさ
    [System.NonSerialized]
    public Vector3 firstScale;

    private void Start()
    {
        firstScale = transform.localScale;
    }
    private void Update()
    {
        if (target != null)
        {
            //矢印が目的のオブジェクトのほうを向くようにする
            transform.LookAt(target);

            //矢印の動きを設定
            period = Mathf.PingPong(Time.time * arrowSpeed, maxAmplitude);
            //（目標オブジェクトの上とカメラを結ぶ）直線と目標オブジェクトの垂線の足を求める
            perpendicularCoordinates = PerpendicularFootPoint(cameraObj.transform.position,
                target.position + (Vector3.up * heightFromTarget), target.position);
            //矢印の座標を設定
            transform.position = perpendicularCoordinates - (transform.forward
                * (minDistArrowFromTarget + period));

        }
    }
    Vector3 PerpendicularFootPoint(Vector3 a, Vector3 b, Vector3 p)
    {
        return a + Vector3.Project(p - a, b - a);
    }
}



