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

    [SerializeField, NonEditable]
    public Transform player;

    [SerializeField]//�L�[�{�[�h�}�E�X����̂Ƃ��̃C���^���N�g�̉摜
    private GameObject interactImageKeyboardMouse;

    [SerializeField]//�p�b�h����̂Ƃ��̃C���^���N�g�̉摜
    private GameObject interactImageGamepad;

    private GameObject interactImage;

    // �{�^����������Ă��邩�ǂ������擾����
    bool ps4O = false;

    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        GetPS4O();
        ImageChange();

        if (upTrigger)
        {
            if (Input.GetMouseButtonDown(0) || ps4O)
            {
                fadeinout.fadeout = true;
                player.position = downPosition.position;
            }
        }
        if (downTrigger)
        {
            if (Input.GetMouseButtonDown(0) || ps4O)
            {
                fadeinout.fadeout = true;
                player.position = upPosition.position;
            }
        }
    }


    public void SetRopeUp(bool flg)
    {
        upTrigger = flg;
        interactImage.SetActive(flg);
    }

    public void SetRopeDown(bool flg)
    {
        downTrigger = flg;
        interactImage.SetActive(flg);
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
