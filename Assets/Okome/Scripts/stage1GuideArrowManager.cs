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

    [SerializeField]�@//�e���r�̏�̖ю���
    private GameObject ballOfWoolOnTV;
    private ballOfWool ballOfWoolEnabledAnimation;

    [SerializeField]�@//�ʂ�����݂�����i�̂ЂƂ�̒i�̑���
    private GameObject detectionScaffold;

    //�����o����̑���ɂ����̂𔻒肷��
    private detectionPlayerOn isPlayerOnScaffold;

    [SerializeField]�@//�S�[��
    private GameObject goal;

    [SerializeField]
    private CheckPointManager checkPointManager;

    [SerializeField]�@//�傫�Ȋ��̏�̃`�F�b�N�|�C���g
    private GameObject checkPoint1;

    [SerializeField]�@//�����o���̑O�̑���
    private GameObject checkPoint2;



    private void Start()
    {
        _guideArrow = guideArrowObj.GetComponent<guideArrow>();
        target = detectionFirstShelf;
        ballOfWoolEnabledAnimation = ballOfWoolOnTV.GetComponent<ballOfWool>();
        isPlayerOnFirstShelf = detectionFirstShelf.GetComponent<detectionPlayerOn>();
        isPlayerOnScaffold = detectionScaffold.GetComponent<detectionPlayerOn>();
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
        else if (checkPointManager.lastCheckPoint == checkPoint2.transform.position)
        {
            target = detectionScaffold;
            guideArrowObj.transform.localScale = _guideArrow.firstScale * magnification;
        }
        else if (!ballOfWoolEnabledAnimation.enabledAnimation)
        {
            target = checkPoint2;
            guideArrowObj.transform.localScale = _guideArrow.firstScale * magnification;
        }
        //�傫�Ȋ��ɂ̂�����
        else if (checkPointManager.lastCheckPoint == checkPoint1.transform.position)
        {
            target = ballOfWoolOnTV;
            guideArrowObj.transform.localScale = _guideArrow.firstScale * magnification;
        }
        else if (isPlayerOnFirstShelf.isPlayerOn)
        {
            target = checkPoint1;
        }
        else
        {
            target = detectionFirstShelf;
        }
    }
}