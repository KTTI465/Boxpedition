using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class drawer : MonoBehaviour
{
    [SerializeField]
    //引き出しを格納
    public GameObject Drawer;

    //プレイヤーを格納
    private GameObject Player;

    //プレイヤーが見ているものを格納
    private List<GameObject> _interactGameObjectsList = new List<GameObject>();

    public float DrawerMoveSpeed;

    public Animator charaAnimator;

    private bool isGrab;

    [SerializeField]//キーボードマウス操作のときのインタラクトの画像
    private GameObject interactImageKeyboardMouse;

    [SerializeField]//パッド操作のときのインタラクトの画像
    private GameObject interactImageGamepad;

    private GameObject interactImage;

    //マウスのボタンが押されたことを判別する
    private bool isPressedMouseButton0;

    // 〇ボタンが押されているかどうかを取得する
    bool ps4O = false;
    private void Start()
    {
        isGrab = false;
    }
    private void Update()
    {
        ImageChange();

        if (Gamepad.current != null)
        {
            if (Gamepad.current.buttonEast.wasPressedThisFrame && ps4O == false)
            {
                if(isGrab == false)
                {
                    ps4O = true;
                    Invoke("ResetPS4O", 0.5f);
                }
                else
                {
                    isGrab = false;

                    //プレイヤーの移動スクリプトを有効にする
                    Player.GetComponent<CharacterController>().enabled = true;

                    charaAnimator.SetBool("pull", false); // アニメーション切り替え

                    isPressedMouseButton0 = true;
                }
            }
        }

        /*
        if (isGrab == true && Player != null)
        {
            float zMovement = Input.GetAxisRaw("Vertical") / 80 * DrawerMoveSpeed;

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

            
            if (Input.GetMouseButtonDown(0) || ps4O)
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
        */
    }

    private void OnTriggerStay(Collider other)
    {
        //一定の範囲にプレイヤーが入った時
        if (other.gameObject.CompareTag("Player"))
        {
            //プレイヤーが見ているものを取得
            _interactGameObjectsList = other.GetComponent<CharacterController>().InteractGameObjectsList;
            if (_interactGameObjectsList != null && _interactGameObjectsList.Contains(gameObject))
            {
                interactImage.SetActive(true);
                
                if ((Input.GetMouseButtonDown(0) || ps4O) && isPressedMouseButton0 == false)
                {
                    if (isGrab == false)
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

                        charaAnimator.SetBool("pull", true); // アニメーション切り替え

                        isPressedMouseButton0 = true;

                        ps4O = false;
                    }
                }

                if (isGrab == true && Player != null)
                {
                    float zMovement = Input.GetAxisRaw("Vertical") / 80 * DrawerMoveSpeed;

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
                }
            }
            else
            {
                interactImage.SetActive(false);
            }

            if (Input.GetMouseButtonUp(0))
            {
                isPressedMouseButton0 = false;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        //一定の範囲からプレイヤーが出た時
        if (other.gameObject.CompareTag("Player"))
        {
            isGrab = false;

            //プレイヤーの移動スクリプトを有効にする
            Player.GetComponent<CharacterController>().enabled = true;

            charaAnimator.SetBool("pull", false); // アニメーション切り替え

            interactImage.SetActive(false);

            isPressedMouseButton0 = false;
        }
    }

    void ResetPS4O()
    {
        ps4O = false;
    }

    void ImageChange()
    {
        //パッド操作のとき
        if (Gamepad.current != null)
        {
            if (interactImage != interactImageGamepad)
            {
                //パッド操作のインタラクトの画像を設定
                interactImage = interactImageGamepad;
            }
        }
        else //キーボードマウス操作のとき
        {
            if (interactImage != interactImageKeyboardMouse)
            {
                //キーボードマウス操作のインタラクトの画像を設定
                interactImage = interactImageKeyboardMouse;
            }
        }
    }
}