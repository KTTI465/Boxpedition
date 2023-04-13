using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pullBookmark : MonoBehaviour
{
    //掴んでいるかの判定フラグ
    public bool grabFlg;

    //掴んだオブジェクトのrigidbody格納用変数
    new Rigidbody rigidbody;

    private void Start()
    {
        grabFlg = false;
    }
    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == ("bookmark"))
        {
            //判定をtrueにする
            grabFlg = true;
            rigidbody = other.gameObject.GetComponent<Rigidbody>();

            // 左ボタンが押されていたら物体を親子関係にする
            if (Input.GetMouseButton(1) && grabFlg == true)
            {
                //Rigidbodyを停止
                rigidbody.velocity = Vector3.zero;

                //持っているときに下に落ちないようにする
                rigidbody.constraints = RigidbodyConstraints.FreezePositionY;
                rigidbody.constraints = RigidbodyConstraints.FreezeRotation;

                //親子関係にする
                other.gameObject.transform.parent = gameObject.transform;

                Vector3 localPos = other.gameObject.transform.localPosition;
                if (other.gameObject.transform.localPosition.y < -0.3)
                {
                    localPos.y = -0.3f;
                }
                other.gameObject.transform.localPosition = localPos;
            }
            else
            {

                //親子関係を解除
                other.gameObject.transform.parent = null;

                //下に落ちるようにする
                rigidbody.constraints = RigidbodyConstraints.None;
                rigidbody.constraints = RigidbodyConstraints.FreezeRotation;

                //掴む判定をfalseにする
                grabFlg = false;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == ("bookmark"))
        {
            //栞と手の距離が遠くなったら離すようにする         
            grabFlg = false;
            other.gameObject.transform.parent = null;
        }
    }
}
