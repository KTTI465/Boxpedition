using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stage1GuideArrowManager : MonoBehaviour

{
    //�v���C���[�̖����i�[
    [SerializeField]
    private GameObject guideArrowObj;
    private guideArrow _guideArrow;

    [SerializeField]�@//�傫������Ƃ��̔{��
    private float magnification;

    //��󂪌����I�u�W�F�N�g
    private GameObject target;

    [SerializeField]�@//�ŏ��̒I
    private GameObject detectionFirstShelf;

    //�ŏ��̒I�̏�ɂ����̂𔻒肷��
    private detectionPlayerOn isPlayerOnFirstShelf;

    [SerializeField] //�傫�Ȋ��̏�̃`�F�b�N�|�C���g
    private GameObject detectionCheckPoint1;
    private detectionPlayerOn isPlayerOnCheckPoint1;

    [SerializeField]�@//�e���r�̏�̖ю���
    private GameObject ballOfWoolOnTV;
    private ballOfWool ballOfWoolEnabledAnimation;

    [SerializeField]�@//�����o���̑O�̑���
    private GameObject detectionCheckPoint2;
    private detectionPlayerOn isPlayerOnCheckPoint2;

    [SerializeField]�@//�ʂ�����݂�����i�̂ЂƂ�̒i�̑���
    private GameObject detectionScaffold;

    //�����o����̑���ɂ����̂𔻒肷��
    private detectionPlayerOn isPlayerOnScaffold;

    [SerializeField]�@//�S�[��
    private GameObject goal;

    private void Start()
    {
        _guideArrow = guideArrowObj.GetComponent<guideArrow>();
        target = detectionFirstShelf;
        ballOfWoolEnabledAnimation = ballOfWoolOnTV.GetComponent<ballOfWool>();
        isPlayerOnFirstShelf = detectionFirstShelf.GetComponent<detectionPlayerOn>();
        isPlayerOnScaffold = detectionScaffold.GetComponent<detectionPlayerOn>();
        isPlayerOnCheckPoint1 = detectionCheckPoint1.GetComponent<detectionPlayerOn>();
        isPlayerOnCheckPoint2 = detectionCheckPoint2.GetComponent<detectionPlayerOn>();
    }
    private void Update()
    {
        Debug.Log(_guideArrow.target);
        //�^�[�Q�b�g�I�u�W�F�N�g�̍X�V
        if (_guideArrow.target != target.transform)
            _guideArrow.target = target.transform;

        if (isPlayerOnScaffold.isPlayerOn)
        {
            target = goal;
            guideArrowObj.transform.localScale = _guideArrow.firstScale;
        }
        else if (isPlayerOnCheckPoint2.isPlayerOn)
        {
            target = detectionScaffold;
            guideArrowObj.transform.localScale = _guideArrow.firstScale * magnification;
        }
        else if (!ballOfWoolEnabledAnimation.enabledAnimation)
        {
            target = detectionCheckPoint2;
            guideArrowObj.transform.localScale = _guideArrow.firstScale * magnification;
        }
        //�傫�Ȋ��ɂ̂�����
        else if (isPlayerOnCheckPoint1.isPlayerOn)
        {
            target = ballOfWoolOnTV;
            guideArrowObj.transform.localScale = _guideArrow.firstScale * magnification;
        }
        else if (isPlayerOnFirstShelf.isPlayerOn)
        {
            target = detectionCheckPoint1;
        }
        else
        {
            target = detectionFirstShelf;
        }
    }
}