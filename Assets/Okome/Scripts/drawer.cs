using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class drawer : MonoBehaviour
{
    [SerializeField]
    //上の棚を格納
    public GameObject topDrawer;

    [SerializeField]
    //上の棚の取っ手の部分を格納
    public GameObject topDrawerHundle;

    //プレイヤーを格納
    private GameObject Player;

    //プレイヤーが見ているものを格納
    private GameObject _rayHitObject;

    private bool isGrab;
    private void Start()
    {
        isGrab = false;
    }
    private void Update()
    {
        if(isGrab&&Player!=null)
        {
            //引き出しの取っ手のほうを見るようにする。
            Player.transform.eulerAngles = new Vector3(0, transform.eulerAngles.y+270f, 0);
            float zMovement = Input.GetAxisRaw("Vertical");
            topDrawer.transform.Translate(zMovement, 0, 0);
            Player.transform.Translate(0, 0, zMovement);
        }
        else
        {
            //プレイヤーの移動スクリプトを有効にする
            Player.GetComponent<CharacterController>().enabled = true;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        //一定の範囲にプレイヤーが入った時
        if (other.gameObject.CompareTag("Player"))
        {
            //マウスの右クリックをしたとき
            if (Input.GetMouseButton(1))
            {
                //プレイヤーが見ているものを取得
                _rayHitObject = other.GetComponent<CharacterController>().rayHitObject.collider.gameObject;
                //プレイヤーが見ているものが上の棚の取っ手だった時
                if (_rayHitObject != null && _rayHitObject == topDrawerHundle)
                {
                    //プレイヤーに格納
                    Player = other.gameObject;
                    //プレイヤーの移動スクリプトを無効にする
                    Player.GetComponent<CharacterController>().enabled = false;
                    isGrab = true;
                }
                else
                {
                    isGrab = false;
                }
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        //一定の範囲からプレイヤーが出た時
        if (other.gameObject.CompareTag("Player"))
        {
            isGrab = false;
        }
    }
}
