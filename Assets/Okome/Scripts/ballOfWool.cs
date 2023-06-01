using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using Unity.VisualScripting;
using UnityEngine.UI;
using static UnityEditor.PlayerSettings;
using ballOfWoolState;

public class ballOfWool : MonoBehaviour
{
    [SerializeField]
    public GameObject animationCamara;

    private Animator animator;
    private bool enabledAnimation;

    private GameObject Player;

    private GameObject _rayHitObject;

    public Animator charaAnimator;

    private Vector3 ropePos;

    public GameObject rope;
    // �Z�{�^����������Ă��邩�ǂ������擾����
    bool ps4O = false;

    [SerializeField]//�L�[�{�[�h�}�E�X����̂Ƃ��̃C���^���N�g�̉摜
    private GameObject interactImageKeyboardMouse;

    [SerializeField]//�p�b�h����̂Ƃ��̃C���^���N�g�̉摜
    private GameObject interactImageGamepad;

    private GameObject interactImage;

    private string _preStateName;
    public ballOfWoolStateProcessor StateProcessor { get; set; } = new ballOfWoolStateProcessor();
    public ballOfWoolStateIdle StateIdle { get; set; } = new ballOfWoolStateIdle();
    public ballOfWoolStateAnimation StateAnimation { get; set; } = new ballOfWoolStateAnimation();
    void Start()
    {
        animator = GetComponent<Animator>();
        enabledAnimation = true;

        StateProcessor.State = StateIdle;
        StateIdle.ExecAction = Idle;
        StateAnimation.ExecAction = Animation;
    }

    void Update()
    {
        //�X�e�[�g�̒l���ύX���ꂽ����s�������s��
        if (StateProcessor.State.GetStateName() != _preStateName)
        {
            _preStateName = StateProcessor.State.GetStateName();
            StateProcessor.Execute();
        }
        ImageChange();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (enabledAnimation == true)
            {
                _rayHitObject = other.GetComponent<CharacterController>().rayHitObject;

                if (_rayHitObject != null && _rayHitObject == gameObject)
                {
                    GetPS4O();
                    interactImage.SetActive(true);

                    if (Input.GetMouseButton(0) || ps4O)
                    {
                        charaAnimator.SetBool("grab", true); // �A�j���[�V�����؂�ւ�
                        Player = other.gameObject;
                        animator.SetTrigger("rollBallOfWool");
                        enabledAnimation = false;
                    }
                }
                else
                {
                    interactImage.SetActive(false);
                }
            }
            else
            {
                interactImage.SetActive(false);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            interactImage.SetActive(false);
        }
    }

    public void StartAnimation()
    {
        StateProcessor.State = StateAnimation;
        rope.SetActive(true);
        Player.GetComponent<CharacterController>().enabled = false;
        animationCamara.GetComponent<Camera>().depth = 1;
    }
    public void SetRopePos()
    {
        ropePos = transform.position;
    }

    public void EndAnimation()
    {
        StateProcessor.State = StateIdle;
        Player.GetComponent<CharacterController>().enabled = true;
        Destroy(animationCamara);
        gameObject.layer = LayerMask.NameToLayer("IgnoreCameraRay");
        charaAnimator.SetBool("grab", false); // �A�j���[�V�����؂�ւ�
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

    public void Idle()
    {
        //Debug.Log("ballOfWoolState��Idle�ɏ�ԑJ�ڂ��܂����B");
    }
    public void Animation()
    {
        //Debug.Log("ballOfWoolState��Animation�ɏ�ԑJ�ڂ��܂����B");
    }
}
