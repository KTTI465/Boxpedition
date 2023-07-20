using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class drawer : MonoBehaviour
{
    [SerializeField]
    //�����o�����i�[
    public GameObject Drawer;

    //�v���C���[���i�[
    private GameObject Player;

    //�v���C���[�����Ă�����̂��i�[
    private List<GameObject> _interactGameObjectsList = new List<GameObject>();

    public float DrawerMoveSpeed;

    public Animator charaAnimator;

    private bool isGrab;

    [SerializeField]//�L�[�{�[�h�}�E�X����̂Ƃ��̃C���^���N�g�̉摜
    private GameObject interactImageKeyboardMouse;

    [SerializeField]//�p�b�h����̂Ƃ��̃C���^���N�g�̉摜
    private GameObject interactImageGamepad;

    private GameObject interactImage;

    //�}�E�X�̃{�^���������ꂽ���Ƃ𔻕ʂ���
    private bool isPressedMouseButton0;

    // �Z�{�^����������Ă��邩�ǂ������擾����
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

                    //�v���C���[�̈ړ��X�N���v�g��L���ɂ���
                    Player.GetComponent<CharacterController>().enabled = true;

                    charaAnimator.SetBool("pull", false); // �A�j���[�V�����؂�ւ�

                    isPressedMouseButton0 = true;
                }
            }
        }

        /*
        if (isGrab == true && Player != null)
        {
            float zMovement = Input.GetAxisRaw("Vertical") / 80 * DrawerMoveSpeed;

            //�����o�����ړ��������Ȃ��悤��
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
                //�v���C���[�̈ړ��X�N���v�g��L���ɂ���
                Player.GetComponent<CharacterController>().enabled = true;
            }
            

            charaAnimator.SetBool("pull", true); // �A�j���[�V�����؂�ւ�
        }
        else if (Player != null)
        {
            //�v���C���[�̈ړ��X�N���v�g��L���ɂ���
            Player.GetComponent<CharacterController>().enabled = true;

            charaAnimator.SetBool("pull", false); // �A�j���[�V�����؂�ւ�
        }
        */
    }

    private void OnTriggerStay(Collider other)
    {
        //���͈̔͂Ƀv���C���[����������
        if (other.gameObject.CompareTag("Player"))
        {
            //�v���C���[�����Ă�����̂��擾
            _interactGameObjectsList = other.GetComponent<CharacterController>().InteractGameObjectsList;
            if (_interactGameObjectsList != null && _interactGameObjectsList.Contains(gameObject))
            {
                interactImage.SetActive(true);
                
                if ((Input.GetMouseButtonDown(0) || ps4O) && isPressedMouseButton0 == false)
                {
                    if (isGrab == false)
                    {
                        //�v���C���[�Ɋi�[
                        Player = other.gameObject;

                        //�v���C���[�̈ړ��X�N���v�g�𖳌��ɂ���
                        Player.GetComponent<CharacterController>().enabled = false;

                        //�����o���̎����̂ق�������悤�ɂ���B
                        Player.transform.eulerAngles = new Vector3(0, transform.eulerAngles.y - 90f, 0);

                        //�����o���̎���ĂɐG��悤�Ȉʒu�Ɉړ��@�L�����N�^�[���f���ɂ���Ē����K�v
                        Player.transform.position = transform.right * -0.1f + Player.transform.position;

                        isGrab = true;

                        charaAnimator.SetBool("pull", true); // �A�j���[�V�����؂�ւ�

                        isPressedMouseButton0 = true;

                        ps4O = false;
                    }
                }

                if (isGrab == true && Player != null)
                {
                    float zMovement = Input.GetAxisRaw("Vertical") / 80 * DrawerMoveSpeed;

                    //�����o�����ړ��������Ȃ��悤��
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
        //���͈̔͂���v���C���[���o����
        if (other.gameObject.CompareTag("Player"))
        {
            isGrab = false;

            //�v���C���[�̈ړ��X�N���v�g��L���ɂ���
            Player.GetComponent<CharacterController>().enabled = true;

            charaAnimator.SetBool("pull", false); // �A�j���[�V�����؂�ւ�

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