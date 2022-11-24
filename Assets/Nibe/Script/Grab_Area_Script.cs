using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Grab_Area_Script : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(0))
        {
        }
        else
        {
            //たまに判定バグで親子関係解除されないため、強制敵に親子関係解除する
            //(この場合重力は復活しないかも？)
            this.gameObject.transform.DetachChildren();
        }
    }

    private void OnTriggerStay(Collider collision)
    {
        //P (push & pull) _ Object の処理

        if (collision.gameObject.CompareTag("P_Object"))
        {
            Rigidbody rigidbody = collision.gameObject.GetComponent<Rigidbody>();

            // 左ボタンが押されていたら物体を親子関係にする
            if (Input.GetMouseButton(0))
            {
                //Rigidbodyを停止
                rigidbody.velocity = Vector3.zero;

                //重力を停止させる
                rigidbody.isKinematic = true;

                //親子関係にする
                collision.gameObject.transform.parent = this.gameObject.transform;
            }
            else
            {
                //重力を復活させる
                rigidbody.isKinematic = false;

                //親子関係を解除
                collision.gameObject.transform.parent = null;
            }
        }
    }
}
