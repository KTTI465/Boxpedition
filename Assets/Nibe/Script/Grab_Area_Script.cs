using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Grab_Area_Script : MonoBehaviour
{
    //掴んでいるかの判定フラグ
    public bool grabFlg = false;

    //投げた後に掴む判定にならないようにするフラグ
    bool firstFlg = false;

    //掴んだオブジェクトのrigidbody格納用変数
    new Rigidbody rigidbody;

    //投げる力
    public float power = 10f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
        }
        else
        {
            //たまに判定バグで親子関係解除されないため、強制敵に親子関係解除する
            //(この場合重力は復活しないかも？)
            this.gameObject.transform.DetachChildren();

            //強制的に判定をtrueにする
            grabFlg = false;
        }

        //右クリックで投げる
        if (Input.GetMouseButtonDown(1) && grabFlg)
        {
            //投げる方向計算
            var vec = (transform.position - transform.parent.position) + new Vector3(0f, 2f, 0f);
            //物理演算を有効に
            rigidbody.isKinematic = false;
            //投げる方向にaddforce
            rigidbody.AddForce(vec.normalized * power, ForceMode.VelocityChange);
            //フラグリセット
            grabFlg = false;

            firstFlg = true;
        }
    }

    private void OnTriggerStay(Collider collision)
    {
        //P (push & pull) _ Object の処理

        if (collision.gameObject.CompareTag("P_Object"))
        {
            rigidbody = collision.gameObject.GetComponent<Rigidbody>();

            // 左ボタンが押されていたら物体を親子関係にする
            if (Input.GetMouseButton(0))
            {
                //投げるボタンを押したときに掴まないようにする
                if (!firstFlg)
                {
                    //Rigidbodyを停止
                    rigidbody.velocity = Vector3.zero;

                    //重力を停止させる
                    rigidbody.isKinematic = true;

                    //親子関係にする
                    collision.gameObject.transform.parent = this.gameObject.transform;

                    //判定をtrueにする
                    grabFlg = true;
                }
                else
                {
                    //物理演算有効
                    rigidbody.isKinematic = false;
                    //親オブジェクトの解除
                    collision.gameObject.transform.parent = null;
                }
            }
            else
            {
                //重力を復活させる
                rigidbody.isKinematic = false;

                //親子関係を解除
                collision.gameObject.transform.parent = null;

                //掴む判定をfalseにする
                grabFlg = false;

                //投げる判定をfalseにする
                firstFlg = false;
            }
        }
    }
}
