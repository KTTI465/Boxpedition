using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class plug : MonoBehaviour
{
    //���[�v�̎q�v�f���i�[
    GameObject[] plugRope;
    //�q�v�f�̍��v
    int plugRopeCount;
    //�q�v�f���ꂼ�ꂪ���Ԗڂ������߂鐔��
    int plugRopeIndex;

    //�v���C���[���i�[
    [SerializeField] GameObject player;
    //�v���C���[��CharacterController,Rigidbody,Animator���i�[
    CharacterController characterController;
    Rigidbody playerRb;
    Animator playerAnimator;

    //�͂߂邩�̔���
    bool canGrab;
    //�����Ă���Ƃ��̔���
    bool isSlideDown;
    //����X�s�[�h
    float speed = 20f;

    LineRenderer line;
    //�R���g���[���[�̃{�^���̔���
    bool ps4O;
    [SerializeField]//�C���^���N�g�̉摜���i�[
    private GameObject interactImage;

    [SerializeField]//�p��̃C���^���N�g�̉摜���i�[
    private GameObject interactImageEnglish;

    // Start is called before the first frame update
    void Start()
    {
        characterController = player.GetComponent<CharacterController>();
        playerRb = player.GetComponent<Rigidbody>();
        playerAnimator = player.GetComponent<Animator>();

        canGrab = false;
        isSlideDown = false;
        interactImage.SetActive(false);

        plugRopeIndex = 0;
        plugRopeCount = transform.childCount;
        plugRope = new GameObject[plugRopeCount];

        line = GetComponent<LineRenderer>();
        line.positionCount = plugRopeCount;
        foreach (Transform rope in transform)
        {
            //�q�I�u�W�F�N�g�����ԂɊi�[���Č����ڂ��\���ɂ��Ă���
            plugRope[plugRopeIndex++] = rope.gameObject;
            rope.GetComponent<MeshRenderer>().enabled = false;
        }
        //���[�v��͂񂾎��Ɉړ�����q�I�u�W�F�N�g�i6�Ԗڂ̂Ƃ��납�犊��n�߂�悤�ɂȂ��Ă���j
        plugRopeIndex = 6;
        //�������Z���Ƃ߂邽�߂̃R���[�`���in�b��,�j
        StartCoroutine(StopPhysics(10.0f, plugRope));


        if (PlayerPrefs.GetString("Language") == "English")
        {
            interactImage = interactImageEnglish;
        }
    }

    // Update is called once per frame
    void Update()
    {
        GetPS4O();

        //����\������
        int idx = 0;
        foreach (GameObject rope in plugRope)
        {
            line.SetPosition(idx, rope.transform.position);
            idx++;
        }

        //�͂߂�Ƃ��Ƀ{�^���������ƒ͂�
        if (canGrab == true)
        {
            if (Input.GetMouseButtonDown(0) || ps4O)
            {
                isSlideDown = true;
                interactImage.SetActive(false);
                playerRb.isKinematic = true;
                playerRb.useGravity = false;

                //�A�j���[�V�����̐ݒ�
                playerAnimator.SetBool("jump", false);
                playerAnimator.SetBool("jump2", false);
                playerAnimator.SetBool("climbStay", true);

                //�L�����N�^�[�̑�����ł��Ȃ��悤�ɂ���
                if (characterController.enabled == true)
                {
                    characterController.enabled = false;
                }
            }
        }
        //�����Ă���Ƃ�
        if (isSlideDown == true)
        {
            //�A�j���[�V�����̐ݒ�
            playerAnimator.SetBool("jump", false);
            playerAnimator.SetBool("jump2", false);
            playerAnimator.SetBool("climbStay", true);

            //�L�����N�^�[�̌����̐ݒ�
            player.transform.eulerAngles = transform.up * 180f;
            //�ړI�̎q�I�u�W�F�N�g�̈ʒu��ݒ肵�Ă��̈ʒu�Ɍ������Ċ����Ă���
            Vector3 nextPos = plugRope[plugRopeIndex].transform.position - new Vector3(0.0f, 3.0f, 0.0f);
            player.transform.position = Vector3.MoveTowards(player.transform.position, nextPos, speed * Time.deltaTime);
            //�ړI�̎q�I�u�W�F�N�g�ɓ��B�����Ƃ�
            if (player.transform.position == nextPos)
            {
                //�Ō�̎q�I�u�W�F�N�g�ɓ��B�����Ƃ�
                if (plugRopeIndex == plugRopeCount - 1)
                {
                    isSlideDown = false;
                    player.GetComponent<Rigidbody>().isKinematic = false;
                    player.GetComponent<Rigidbody>().useGravity = true;
                    playerAnimator.SetBool("climbStay", false);
                    if (characterController.enabled == false)
                    {
                        characterController.enabled = true;
                    }
                }
                //���̎q�I�u�W�F�N�g�̐����ɂ���
                plugRopeIndex++;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            //�͂ނ��Ƃ��ł���悤�ɂ��āA�C���^���N�g�̉摜��\������
            canGrab = true;
            interactImage.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            //�͂ނ��Ƃ��ł��Ȃ��悤�ɂ��āA�C���^���N�g�̉摜���\���ɂ���
            canGrab = false;
            interactImage.SetActive(false);
        }
    }

    IEnumerator StopPhysics(float time, GameObject[] plugRope)
    {
        //��莞�Ԃ������烍�[�v�̕������Z���Ƃ߂�
        yield return new WaitForSecondsRealtime(time);
        foreach (GameObject rope in plugRope)
        {
            rope.GetComponent<Rigidbody>().isKinematic = true;
            rope.GetComponent<Rigidbody>().useGravity = false;
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
