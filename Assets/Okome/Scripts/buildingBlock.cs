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

    [NonSerialized] //�͂܂�Ă��邩�̔���
    public bool isGrabed;

    //�v���C���[�����Ă�����̂��i�[
    private CharacterController _characterController;

    [SerializeField]//�L�[�{�[�h�}�E�X����̂Ƃ��̃C���^���N�g�̉摜
    private GameObject interactImageKeyboardMouse;

    [SerializeField]//�p�b�h����̂Ƃ��̃C���^���N�g�̉摜
    private GameObject interactImageGamepad;

    private GameObject interactImage;

    //�R���_�[���ɓ����Ă���Ƃ��̔���
    private bool playerTriggerStay;

    // �Z�{�^����������Ă��邩�ǂ������擾����
    bool ps4O = false;

    //�}�E�X�̃{�^���������ꂽ���Ƃ𔻕ʂ���
    private bool isPressedMouseButton0;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();

        isGrabed = false;
        ps4O = false;
        isPressedMouseButton0 = false;

        //�v���C���[�����Ă�����̂��擾
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
            //�v���C���[�����Ă�����̂��擾
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
        //���͈̔͂Ƀv���C���[����������
        if (other.gameObject.CompareTag("Player"))
        {
            playerTriggerStay = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && rigidbody.isKinematic == false)
        {
            charaAnimator.SetBool("pull", false); // �A�j���[�V�����؂�ւ�
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
        //�p�b�h����̂Ƃ�
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

