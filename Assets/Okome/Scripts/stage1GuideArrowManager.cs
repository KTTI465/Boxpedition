using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stage1GuideArrowManager : MonoBehaviour

{
    //�v���C���[�̖����i�[
    [SerializeField]
    private guideArrow _guideArrow;

    //��󂪌����I�u�W�F�N�g
    private GameObject target;

    [SerializeField]�@//DVD�̒I
    private GameObject DVDShelf;

    [SerializeField]�@//�ςݖ�
    private GameObject buildingBlockObj;
    private buildingBlock _buildingBlock; 

    [SerializeField]
    private GameObject detectionBuildingBlockAreaObj;
    private detectionBuildingBlockArea _detectionBuildingBlockArea;

    //[SerializeField]  //�X�����O�V���b�g
    //private GameObject slingShot;

    [SerializeField]�@//�e���r�̏�̖ю���
    private GameObject ballOfWoolOnTV;
    private ballOfWool ballOfWoolEnabledAnimation;

    [SerializeField]�@//�����o��
    private GameObject drawer;

    [SerializeField]
    private GameObject detectionScaffold;

    //�����o����̑���ɂ����̂𔻒肷��
    private detectionPlayerOnScaffold playerOnScaffold;

    [SerializeField]�@//�S�[��
    private GameObject goal;

    [SerializeField]
    private CheckPointManager checkPointManager;

    [SerializeField]
    private GameObject checkPoint1;

    [SerializeField]
    private GameObject checkPoint2;

    private void Start()
    {
        target = DVDShelf;
        _buildingBlock = buildingBlockObj.GetComponent<buildingBlock>();
        _detectionBuildingBlockArea = detectionBuildingBlockAreaObj.GetComponent<detectionBuildingBlockArea>();
        ballOfWoolEnabledAnimation = ballOfWoolOnTV.GetComponent<ballOfWool>();
        playerOnScaffold = detectionScaffold.GetComponent<detectionPlayerOnScaffold>();
    }
    private void Update()
    {
        Debug.Log(_guideArrow.target);
        //�^�[�Q�b�g�I�u�W�F�N�g�̍X�V
        if (_guideArrow.target != target.transform)
            _guideArrow.target = target.transform;

        if (playerOnScaffold.playerOnScaffold)
        {
            target = goal;
        }
        else if(!ballOfWoolEnabledAnimation.enabledAnimation)
        {
            target = drawer;
        }
        else if(_detectionBuildingBlockArea.onBlock)
        {
            target = ballOfWoolOnTV;
        }
        else if(_buildingBlock.isGrabed)
        {
            target = detectionBuildingBlockAreaObj;
        }
        else if (checkPointManager.lastCheckPoint == checkPoint1.transform.position)
        {
            target = buildingBlockObj;
        }
        else
        {
            target = DVDShelf;
        }
    }
}

