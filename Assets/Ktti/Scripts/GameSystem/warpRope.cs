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
    public Fadeinout fadeinout;

    //接触したかどうかの判定
    [SerializeField]
    private bool upTrigger = true;
    //接触したかどうかの判定
    [SerializeField, NonEditable]
    private bool downTrigger = false;

    [SerializeField, NonEditable]
    public Transform player;

    [SerializeField]
    private GameObject interactImageUp;
    [SerializeField]
    private GameObject interactImageDown;

    [SerializeField]
    RopeActAnim anim;

    // ボタンが押されているかどうかを取得する
    bool ps4O = false;

    [NonEditable]
    public bool enable = true;

    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        GetPS4O();

        if (enable)
        {
            if (upTrigger)
            {
                interactImageDown.gameObject.SetActive(true);
                if (Input.GetMouseButtonDown(0) || ps4O)
                {
                    anim.DownAnim();
                }
            }
            else
            {
                interactImageDown.gameObject.SetActive(false);
            }
            if (downTrigger)
            {
                interactImageUp.gameObject.SetActive(true);
                if (Input.GetMouseButtonDown(0) || ps4O)
                {
                    anim.UpAnim();
                }
            }
            else
            {
                interactImageUp.gameObject.SetActive(false);
            }
        }
    }


    public void SetRopeUp(bool flg)
    {
        if (enable)
        {
            upTrigger = flg;
        }
    }

    public void SetRopeDown(bool flg)
    {
        if (enable)
        {
            downTrigger = flg;
        }
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
}
