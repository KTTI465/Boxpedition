using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class warpRope : MonoBehaviour
{
    [SerializeField]
    Transform upPosition;
    [SerializeField] 
    Transform downPosition;

    [SerializeField]
    Fadeinout fadeinout;

    //接触したかどうかの判定
    [SerializeField, NonEditable]
    private bool upTrigger = false;
    //接触したかどうかの判定
    [SerializeField, NonEditable]
    private bool downTrigger = false;

    [SerializeField,NonEditable]
    Transform player;

    [SerializeField]//キーボードマウス操作のときのインタラクトの画像
    private GameObject interactImageKeyboardMouse;

    [SerializeField]//パッド操作のときのインタラクトの画像
    private GameObject interactImageGamepad;

    private GameObject interactImage;

    // ボタンが押されているかどうかを取得する
    bool ps4O = false;

    [SerializeField, NonEditable]
    bool isIntaractRope = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        GetPS4O();
        ImageChange();

        if (isIntaractRope)
        {
            if (upTrigger)
            {
                interactImage.SetActive(true);
                if (Input.GetMouseButtonDown(0) || ps4O)
                {
                    fadeinout.fadeout = true;
                    player.position = downPosition.position;
                }
            }
            else if (downTrigger)
            {
                interactImage.SetActive(true);
                if (Input.GetMouseButtonDown(0) || ps4O)
                {
                    fadeinout.fadeout = true;
                    player.position = upPosition.position;
                }
            }
            else
            {
                interactImage.SetActive(false);
            }
        }
    }

    public void SetRopeUp(bool flg)
    {
        upTrigger = flg;
    }

    public void SetRopeDown(bool flg)
    {
        downTrigger = flg;
    }

    public void SetIntaractActive(bool flg)
    {
        isIntaractRope = flg;
    }

    void GetPS4O()
    {
        if (Gamepad.current != null)
        {
            if (Gamepad.current.buttonEast.wasPressedThisFrame)
            {
                ps4O = true;
            }
            else
            {
                ps4O = false;
            }
        }
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
