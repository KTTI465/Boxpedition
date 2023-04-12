using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class drawer : MonoBehaviour
{
    [SerializeField]
    //棚を格納
    public GameObject Drawer;

    [SerializeField]
    //上の棚を格納
    public GameObject topDrawer;

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
        if (isGrab && Player != null)
        {
            float zMovement = Input.GetAxisRaw("Vertical") / 80;

            //引き出しが移動しすぎないように
            if (topDrawer.transform.localPosition.x <= 0.27 && zMovement > 0)
            {
                zMovement = 0;
            }
            else if (topDrawer.transform.localPosition.x >= 0.7 && zMovement < 0)
            {
                zMovement = 0;
            }
            topDrawer.transform.Translate(-zMovement, 0, 0);
            Player.transform.Translate(0, 0, zMovement);
            if (Input.GetMouseButtonUp(0))
            {
                isGrab = false;
            }
        }
        else if (Player != null)
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
            //マウスの左クリックをしたとき
            if (Input.GetMouseButtonDown(0))
            {
                if (other.transform.eulerAngles.y <= transform.eulerAngles.y - 75f ||
                    other.transform.eulerAngles.y >= transform.eulerAngles.y - 115f)
                {
                    //プレイヤーに格納
                    Player = other.gameObject;
                    //プレイヤーの移動スクリプトを無効にする
                    Player.GetComponent<CharacterController>().enabled = false;
                    //引き出しの取っ手のほうを見るようにする。
                    Player.transform.eulerAngles = new Vector3(0, transform.eulerAngles.y - 90f, 0);

                    //引き出しの取ってに触るような位置に移動　1.5fはキャラクターモデルによって調整必要
                    Player.transform.position = new Vector3(transform.position.x + 1.5f, Player.transform.position.y, Player.transform.position.z);

                    isGrab = true;
                }
            }
            //マウスの左クリックをしたとき
            if (Input.GetMouseButtonDown(1))
            {
                //プレイヤーが見ているものを取得
                _rayHitObject = other.GetComponent<CharacterController>().rayHitObject.collider.gameObject;

                //プレイヤーが見ているものが上の棚の取っ手だった時
                if (_rayHitObject != null && _rayHitObject == gameObject)
                {
                    //プレイヤーに格納
                    Player = other.gameObject;

                    //プレイヤーの移動スクリプトを無効にする
                    Player.GetComponent<CharacterController>().enabled = false;

                    //引き出しの取っ手のほうを見るようにする。
                    Player.transform.eulerAngles = new Vector3(0, transform.eulerAngles.y - 90f, 0);

                    //引き出しの取ってに触るような位置に移動　1.5fはキャラクターモデルによって調整必要
                    Player.transform.position = new Vector3(transform.position.x + 1.5f, Player.transform.position.y, Player.transform.position.z);

                    isGrab = true;
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
