using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class drawer : MonoBehaviour
{
    [SerializeField]
    //�I���i�[
    public GameObject Drawer;

    [SerializeField]
    //��̒I���i�[
    public GameObject topDrawer;

    //�v���C���[���i�[
    private GameObject Player;

    //�v���C���[�����Ă�����̂��i�[
    private GameObject _rayHitObject;

    private bool isGrab;
    private void Start()
    {
        isGrab = false;
    }
    private void Update()
    {
        if (isGrab && Player != null)
        {
            float zMovement = Input.GetAxisRaw("Vertical") / 80;

            //�����o�����ړ��������Ȃ��悤��
            if (topDrawer.transform.localPosition.x <= 0.27 && zMovement > 0)
            {
                zMovement = 0;
            }
            else if (topDrawer.transform.localPosition.x >= 0.7 && zMovement < 0)
            {
                zMovement = 0;
            }
            topDrawer.transform.Translate(-zMovement, 0, 0);
            Player.transform.Translate(0, 0, zMovement);
            if (Input.GetMouseButtonUp(0))
            {
                isGrab = false;
            }
        }
        else if (Player != null)
        {
            //�v���C���[�̈ړ��X�N���v�g��L���ɂ���
            Player.GetComponent<CharacterController>().enabled = true;
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
                if (other.transform.eulerAngles.y <= transform.eulerAngles.y - 75f ||
                    other.transform.eulerAngles.y >= transform.eulerAngles.y - 115f)
                {
                    //�v���C���[�Ɋi�[
                    Player = other.gameObject;
                    //�v���C���[�̈ړ��X�N���v�g�𖳌��ɂ���
                    Player.GetComponent<CharacterController>().enabled = false;
                    //�����o���̎����̂ق�������悤�ɂ���B
                    Player.transform.eulerAngles = new Vector3(0, transform.eulerAngles.y - 90f, 0);

                    //�����o���̎���ĂɐG��悤�Ȉʒu�Ɉړ��@1.5f�̓L�����N�^�[���f���ɂ���Ē����K�v
                    Player.transform.position = new Vector3(transform.position.x + 1.5f, Player.transform.position.y, Player.transform.position.z);

                    isGrab = true;
                }
            }
            //�}�E�X�̍��N���b�N�������Ƃ�
            if (Input.GetMouseButtonDown(1))
            {
                //�v���C���[�����Ă�����̂��擾
                _rayHitObject = other.GetComponent<CharacterController>().rayHitObject.collider.gameObject;

                //�v���C���[�����Ă�����̂���̒I�̎���肾������
                if (_rayHitObject != null && _rayHitObject == gameObject)
                {
                    //�v���C���[�Ɋi�[
                    Player = other.gameObject;

                    //�v���C���[�̈ړ��X�N���v�g�𖳌��ɂ���
                    Player.GetComponent<CharacterController>().enabled = false;

                    //�����o���̎����̂ق�������悤�ɂ���B
                    Player.transform.eulerAngles = new Vector3(0, transform.eulerAngles.y - 90f, 0);

                    //�����o���̎���ĂɐG��悤�Ȉʒu�Ɉړ��@1.5f�̓L�����N�^�[���f���ɂ���Ē����K�v
                    Player.transform.position = new Vector3(transform.position.x + 1.5f, Player.transform.position.y, Player.transform.position.z);

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
