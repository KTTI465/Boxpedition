using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class drawer : MonoBehaviour
{
    [SerializeField]
    //��̒I���i�[
    public GameObject topDrawer;

    [SerializeField]
    //��̒I�̎����̕������i�[
    public GameObject topDrawerHundle;

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
        if(isGrab&&Player!=null)
        {
            //�����o���̎����̂ق�������悤�ɂ���B
            Player.transform.eulerAngles = new Vector3(0, transform.eulerAngles.y+270f, 0);
            float zMovement = Input.GetAxisRaw("Vertical");
            topDrawer.transform.Translate(zMovement, 0, 0);
            Player.transform.Translate(0, 0, zMovement);
        }
        else
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
            //�}�E�X�̉E�N���b�N�������Ƃ�
            if (Input.GetMouseButton(1))
            {
                //�v���C���[�����Ă�����̂��擾
                _rayHitObject = other.GetComponent<CharacterController>().rayHitObject.collider.gameObject;
                //�v���C���[�����Ă�����̂���̒I�̎���肾������
                if (_rayHitObject != null && _rayHitObject == topDrawerHundle)
                {
                    //�v���C���[�Ɋi�[
                    Player = other.gameObject;
                    //�v���C���[�̈ړ��X�N���v�g�𖳌��ɂ���
                    Player.GetComponent<CharacterController>().enabled = false;
                    isGrab = true;
                }
                else
                {
                    isGrab = false;
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
