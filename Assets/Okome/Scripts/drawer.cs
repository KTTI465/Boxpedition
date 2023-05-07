using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class drawer : MonoBehaviour
{
    [SerializeField]
    //�����o�����i�[
    public GameObject Drawer;

    //�v���C���[���i�[
    private GameObject Player;

    //�v���C���[�����Ă�����̂��i�[
    private GameObject _rayHitObject;

    public float DrawerMoveSpeed;

    public Animator charaAnimator;

    private bool isGrab;
    private void Start()
    {
        isGrab = false;
    }
    private void Update()
    {
        if (isGrab == true && Player != null)
        {
            float zMovement = Input.GetAxisRaw("Vertical") / 80* DrawerMoveSpeed;

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
            if (Input.GetMouseButtonUp(0))
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
    }

    private void OnTriggerStay(Collider other)
    {
        //���͈̔͂Ƀv���C���[����������
        if (other.gameObject.CompareTag("Player"))
        {
            //�}�E�X�̍��N���b�N�������Ƃ�
            if (Input.GetMouseButtonDown(0))
            {
                //�v���C���[�����Ă�����̂��擾
                _rayHitObject = other.GetComponent<CharacterController>().rayHitObject.collider.gameObject;

                //�v���C���[�����Ă�����̂���̒I�̎���肾������
                if (_rayHitObject != null && _rayHitObject == Drawer)
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
                }
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        //���͈̔͂���v���C���[���o����
        if (other.gameObject.CompareTag("Player"))
        {
            isGrab = false;
        }
    }
}