using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class drawer : MonoBehaviour
{
    [SerializeField]
    //引き出しを格納
    public GameObject Drawer;

    //プレイヤーを格納
    private GameObject Player;

    //プレイヤーが見ているものを格納
    private GameObject _rayHitObject;

    public float DrawerMoveSpeed;

    public Animator charaAnimator;

    private bool isGrab;
    private void Start()
    {
        isGrab = false;
    }
    private void Update()
    {
        if (isGrab == true && Player != null)
        {
            float zMovement = Input.GetAxisRaw("Vertical") / 80* DrawerMoveSpeed;

            //引き出しが移動しすぎないように
            if (Drawer.transform.localPosition.x <= 0.41 && zMovement > 0)
            {
                zMovement = 0;
            }
            else if (Drawer.transform.localPosition.x >= 1 && zMovement < 0)
            {
                zMovement = 0;
            }
            Drawer.transform.Translate(-zMovement, 0, 0);
            Player.transform.Translate(0, 0, zMovement);
            if (Input.GetMouseButtonUp(0))
            {
                isGrab = false;
                //プレイヤーの移動スクリプトを有効にする
                Player.GetComponent<CharacterController>().enabled = true;
            }

            charaAnimator.SetBool("pull", true); // アニメーション切り替え
        }
        else if (Player != null)
        {
            //プレイヤーの移動スクリプトを有効にする
            Player.GetComponent<CharacterController>().enabled = true;

            charaAnimator.SetBool("pull", false); // アニメーション切り替え
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
                //プレイヤーが見ているものを取得
                _rayHitObject = other.GetComponent<CharacterController>().rayHitObject.collider.gameObject;

                //プレイヤーが見ているものが上の棚の取っ手だった時
                if (_rayHitObject != null && _rayHitObject == Drawer)
                {
                    //プレイヤーに格納
                    Player = other.gameObject;

                    //プレイヤーの移動スクリプトを無効にする
                    Player.GetComponent<CharacterController>().enabled = false;

                    //引き出しの取っ手のほうを見るようにする。
                    Player.transform.eulerAngles = new Vector3(0, transform.eulerAngles.y - 90f, 0);

                    //引き出しの取ってに触るような位置に移動　キャラクターモデルによって調整必要
                    Player.transform.position = transform.right * -0.1f + Player.transform.position;

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