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

    //�ڐG�������ǂ����̔���
    [SerializeField, NonEditable]
    private bool upTrigger = false;
    //�ڐG�������ǂ����̔���
    [SerializeField, NonEditable]
    private bool downTrigger = false;

    [SerializeField,NonEditable]
    Transform player;

    [SerializeField]//�L�[�{�[�h�}�E�X����̂Ƃ��̃C���^���N�g�̉摜
    private GameObject interactImageKeyboardMouse;

    [SerializeField]//�p�b�h����̂Ƃ��̃C���^���N�g�̉摜
    private GameObject interactImageGamepad;

    private GameObject interactImage;

    // �{�^����������Ă��邩�ǂ������擾����
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
                //�p�b�h����̃C���^���N�g�̉摜��ݒ�
                interactImage = interactImageGamepad;
            }
        }
        else //�L�[�{�[�h�}�E�X����̂Ƃ�
        {
            if (interactImage != interactImageKeyboardMouse)
            {
                //�L�[�{�[�h�}�E�X����̃C���^���N�g�̉摜��ݒ�
                interactImage = interactImageKeyboardMouse;
            }
        }
    }
}
