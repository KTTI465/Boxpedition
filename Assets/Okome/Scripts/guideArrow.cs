using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class guideArrow : MonoBehaviour
{
    //目的のオブジェクトを格納
    public Transform target;

    [SerializeField]
    private GameObject player;

    //プレイヤーと矢印の距離
    public float meshOnLength = 2f;

    //矢印の高さ
    [SerializeField]
    private float height;

    private MeshRenderer _meshRenderer;
    private void Start()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
    }
    private void Update()
    {
        if (target != null)
        {
            //矢印が目的のオブジェクトのほうを向くようにする
            transform.LookAt(target);
        }

        //矢印の位置をプレイヤーの上に配置
        transform.position = player.transform.position + new Vector3(0.0f, height, 0.0f);

        //プレイヤーと目的のオブジェクトの距離が近くなったときに矢印を非表示にする
        if (Vector3.Distance(player.transform.position, target.transform.position) <= meshOnLength || target == null)
        {
            _meshRenderer.enabled = false;
        }
        else if (_meshRenderer.enabled == false)
        {
            _meshRenderer.enabled = true;
        }
    }
}
