using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class buildingBlock : MonoBehaviour
{
    [SerializeField]
    public GameObject player;

    public Animator charaAnimator;

    new Rigidbody rigidbody;

    [SerializeField]
    private float grabPosZ;

    [NonSerialized] //掴まれているかの判定
    public bool isGrabed;

    //プレイヤーが見ているものを格納
    private CharacterController _characterController;

    [SerializeField]//キーボードマウス操作のときのインタラクトの画像
    private GameObject interactImageKeyboardMouse;

    [SerializeField]//パッド操作のときのインタラクトの画像
    private GameObject interactImageGamepad;

    private GameObject interactImage;

    //コリダー内に入っているときの判定
    private bool playerTriggerStay;

    // 〇ボタンが押されているかどうかを取得する
    bool ps4O = false;

    //マウスのボタンが押されたことを判別する
    private bool isPressedMouseButton0;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();

        isGrabed = false;
        ps4O = false;
        isPressedMouseButton0 = false;

        //プレイヤーが見ているものを取得
        // _interactGameObjectsList = new List<GameObject>();
        // _interactGameObjectsList = player.GetComponent<CharacterController>();
        _characterController = player.GetComponent<CharacterController>();

        interactImage = interactImageGamepad;
    }

    private void Update()
    {
        ImageChange();

        if (playerTriggerStay)
        {
            //プレイヤーが見ているものを取得
            if (_characterController.InteractGameObjectsList != null &&
                _characterController.InteractGameObjectsList.Contains(gameObject))
            {
                interactImage.SetActive(true);

                if (isGrabed == true)
                {
                    if ((Gamepad.current != null && Gamepad.current.buttonEast.wasPressedThisFrame && ps4O == false)) //||
                           // (Mouse.current != null && Input.GetMouseButtonDown(0) && isPressedMouseButton0 == false))
                    {
                        
                        ps4O = true;
                        isPressedMouseButton0 = true;
                        StartCoroutine(ResetButtons());
                        ReleaseBlock();
                    }
                }
                else
                {
                    if ((Gamepad.current != null && Gamepad.current.buttonEast.wasPressedThisFrame && ps4O == false))// ||
                            //(Mouse.current != null && Input.GetMouseButtonDown(0) && isPressedMouseButton0 == false))
                    {
                       
                        ps4O = true;
                        isPressedMouseButton0 = true;
                        StartCoroutine(ResetButtons());
                        GrabBlock();
                    }
                }
            }
            else
            {
                interactImage.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //一定の範囲にプレイヤーが入った時
        if (other.gameObject.CompareTag("Player"))
        {
            playerTriggerStay = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && rigidbody.isKinematic == false)
        {
            charaAnimator.SetBool("pull", false); // アニメーション切り替え
            playerTriggerStay = false;
            interactImage.SetActive(false);
            isGrabed = false;
        }
    }

    void GrabBlock()
    {
        rigidbody.isKinematic = true;
        transform.SetParent(player.transform);
        transform.localRotation = Quaternion.identity;
        transform.localPosition = new Vector3(0, 0.35f, grabPosZ);
        charaAnimator.SetBool("pull", true);
         isGrabed = true;
    }


    void ReleaseBlock()
    {
        rigidbody.isKinematic = false;
        transform.SetParent(null);
        charaAnimator.SetBool("pull", false);
        isGrabed = false;
    }

    IEnumerator ResetButtons()
    {
        yield return new WaitForSeconds(0.5f);

        ps4O = false;
        isPressedMouseButton0 = false;
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

