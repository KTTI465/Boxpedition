using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class pullBookmark : MonoBehaviour
{
    public BookMarkBool bookMarkBool;

    public Animator charaAnimator;

    //�͂�ł��邩�̔���t���O
    public bool grabFlg;

    //�͂݃��[�V�������J�n�������̔���t���O
    public bool grabStart = false;

    //�͂񂾃I�u�W�F�N�g��rigidbody�i�[�p�ϐ�
    new Rigidbody rigidbody;

    [SerializeField]//�L�[�{�[�h�}�E�X����̂Ƃ��̃C���^���N�g�̉摜
    private GameObject interactImageKeyboardMouse;

    [SerializeField]//�p�b�h����̂Ƃ��̃C���^���N�g�̉摜
    private GameObject interactImageGamepad;

    private GameObject interactImage;

    // �Z�{�^����������Ă��邩�ǂ������擾����
    bool ps4O = false;

    private void Start()
    {
        grabFlg = false;
    }
    // Update is called once per frame
    void Update()
    {
        GetPS4O();
        ImageChange();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == ("bookmark"))
        {
            //�����true�ɂ���
            grabFlg = true;
            rigidbody = other.gameObject.GetComponent<Rigidbody>();
            interactImage.SetActive(true);
            Debug.Log("a");
            // ���{�^����������Ă����畨�̂�e�q�֌W�ɂ���
            if ((Input.GetMouseButton(0) || ps4O) && grabFlg == true)
            {
                interactImage.SetActive(false);
                //Rigidbody���~
                rigidbody.velocity = Vector3.zero;

                //�����Ă���Ƃ��ɉ��ɗ����Ȃ��悤�ɂ���
                rigidbody.constraints = RigidbodyConstraints.FreezePositionY;
                rigidbody.constraints = RigidbodyConstraints.FreezeRotation;

                //�e�q�֌W�ɂ���
                other.gameObject.transform.parent = gameObject.transform;

                /*
                Vector3 localPos = other.gameObject.transform.localPosition;
                if (other.gameObject.transform.localPosition.y < -0.3)
                {
                    localPos.y = -0.3f;
                }
                other.gameObject.transform.localPosition = localPos;
                */


                //�[��͂�ł�����
                other.gameObject.transform.rotation = this.gameObject.transform.rotation;
                other.gameObject.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
                other.gameObject.transform.Rotate(0.0f, 90.0f, 0.0f);
                other.gameObject.transform.localPosition = new Vector3(0, 0.35f, 4.5f);

                //�^�񒆂�͂�ł�����
                if (bookMarkBool.grabMiddle == true)
                {
                    other.gameObject.transform.rotation = this.gameObject.transform.rotation;
                    other.gameObject.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
                    other.gameObject.transform.localPosition = new Vector3(0, 0.35f, 1.5f);
                }


                charaAnimator.SetBool("grab", true); // �A�j���[�V�����؂�ւ�
                grabStart = true;
            }
            else
            {

                //�e�q�֌W������
                other.gameObject.transform.parent = null;

                //���ɗ�����悤�ɂ���
                rigidbody.constraints = RigidbodyConstraints.None;
                rigidbody.constraints = RigidbodyConstraints.FreezeRotation;

                //�͂ޔ����false�ɂ���
                grabFlg = false;

                charaAnimator.SetBool("grab", false); // �A�j���[�V�����؂�ւ�
                grabStart = false;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == ("bookmark"))
        {
            //�x�Ǝ�̋����������Ȃ����痣���悤�ɂ���         
            grabFlg = false;
            other.gameObject.transform.parent = null;

            charaAnimator.SetBool("grab", false); // �A�j���[�V�����؂�ւ�
            grabStart = false;
            interactImage.SetActive(false);
        }
    }

    void GetPS4O()
    {
        if (Gamepad.current != null)
        {
            if (Gamepad.current.buttonEast.isPressed)
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
