using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pullBookmark : MonoBehaviour
{
    public BookMarkBool bookMarkBool;

    public Animator charaAnimator;

    //掴んでいるかの判定フラグ
    public bool grabFlg;

    //掴みモーションを開始したかの判定フラグ
    public bool grabStart = false;

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
            if (Input.GetMouseButton(0) && grabFlg == true)
            {
                //Rigidbodyを停止
                rigidbody.velocity = Vector3.zero;

                //持っているときに下に落ちないようにする
                rigidbody.constraints = RigidbodyConstraints.FreezePositionY;
                rigidbody.constraints = RigidbodyConstraints.FreezeRotation;

                //親子関係にする
                other.gameObject.transform.parent = gameObject.transform;

                /*
                Vector3 localPos = other.gameObject.transform.localPosition;
                if (other.gameObject.transform.localPosition.y < -0.3)
                {
                    localPos.y = -0.3f;
                }
                other.gameObject.transform.localPosition = localPos;
                */


                //端を掴んでいた時
                other.gameObject.transform.rotation = this.gameObject.transform.rotation;
                other.gameObject.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
                other.gameObject.transform.Rotate(0.0f, 90.0f, 0.0f);
                other.gameObject.transform.localPosition = new Vector3(0, 0.35f, 4.5f);
                
                //真ん中を掴んでいた時
                if (bookMarkBool.grabMiddle == true)
                {
                    other.gameObject.transform.rotation = this.gameObject.transform.rotation;
                    other.gameObject.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
                    other.gameObject.transform.localPosition = new Vector3(0, 0.35f, 1.5f);
                }
                

                charaAnimator.SetBool("grab", true); // アニメーション切り替え
                grabStart = true;
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

                charaAnimator.SetBool("grab", false); // アニメーション切り替え
                grabStart = false;
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

            charaAnimator.SetBool("grab", false); // アニメーション切り替え
            grabStart = false;
        }
    }
}
