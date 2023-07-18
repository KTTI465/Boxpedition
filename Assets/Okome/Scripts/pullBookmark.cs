using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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

    [SerializeField]//キーボードマウス操作のときのインタラクトの画像
    private GameObject interactImageKeyboardMouse;

    [SerializeField]//パッド操作のときのインタラクトの画像
    private GameObject interactImageGamepad;

    private GameObject interactImage;

    private bool middleBool = false;

    // 〇ボタンが押されているかどうかを取得する
    bool ps4O = false;

    private void Start()
    {
        grabFlg = false;
    }
    // Update is called once per frame
    void Update()
    {
        ImageChange();

        if(grabStart == false)
        {
            middleBool = bookMarkBool.grabMiddle;
        }

        if (Gamepad.current != null)
        {
            if (Gamepad.current.buttonEast.wasPressedThisFrame && ps4O == false)
            {
                ps4O = true;
                Invoke("ResetPS4O", 0.1f);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == ("bookmark"))
        {
            //判定をtrueにする
            grabFlg = true;
            rigidbody = other.gameObject.GetComponent<Rigidbody>();
            interactImage.SetActive(true);

            
            // もう一度ボタンを押したときに離す
            if ((Input.GetMouseButtonDown(0) || ps4O) && grabStart == true)
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

                ps4O = false;
            }           

            // 左ボタンが押されていたら物体を親子関係にする
            if (((Input.GetMouseButtonDown(0) || ps4O) && grabFlg == true && grabStart == false) || grabStart == true)
            {
                //親子関係にする
                other.gameObject.transform.parent = gameObject.transform;

                if (middleBool == true)
                {
                    //真ん中を掴んでいた時
                    other.gameObject.transform.rotation = this.gameObject.transform.rotation;
                    other.gameObject.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
                    other.gameObject.transform.localPosition = new Vector3(0, 0.35f, 1.5f);
                }
                else
                {
                    //端を掴んでいた時
                    other.gameObject.transform.rotation = this.gameObject.transform.rotation;
                    other.gameObject.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
                    other.gameObject.transform.Rotate(0.0f, 90.0f, 0.0f);
                    other.gameObject.transform.localPosition = new Vector3(0, 0.35f, 4.4f);
                }

                interactImage.SetActive(false);
                //Rigidbodyを停止
                rigidbody.velocity = Vector3.zero;

                //持っているときに下に落ちないようにする
                rigidbody.constraints = RigidbodyConstraints.FreezePositionY;
                rigidbody.constraints = RigidbodyConstraints.FreezeRotation;

                /*
                Vector3 localPos = other.gameObject.transform.localPosition;
                if (other.gameObject.transform.localPosition.y < -0.3)
                {
                    localPos.y = -0.3f;
                }
                other.gameObject.transform.localPosition = localPos;
                */

                charaAnimator.SetBool("grab", true); // アニメーション切り替え
                grabStart = true;

                ps4O = false;
            }
            else
            {
                /*
                //親子関係を解除
                other.gameObject.transform.parent = null;

                //下に落ちるようにする
                rigidbody.constraints = RigidbodyConstraints.None;
                rigidbody.constraints = RigidbodyConstraints.FreezeRotation;

                //掴む判定をfalseにする
                grabFlg = false;

                charaAnimator.SetBool("grab", false); // アニメーション切り替え
                grabStart = false;
                */
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
            interactImage.SetActive(false);
        }
    }

    void ResetPS4O()
    {
        ps4O = false;
    }

    void ImageChange()
    {
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
