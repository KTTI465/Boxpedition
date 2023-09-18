using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stage2GuideArrowManager : MonoBehaviour
{
    [SerializeField] //�v���C���[�̖����i�[
    private GameObject guideArrowObj;
    private guideArrow _guideArrow;

    //��󂪌����I�u�W�F�N�g
    private GameObject target;

    [SerializeField]
    private float magnification;

    [SerializeField] //���W�J�Z�̃R���Z���g
    private GameObject radioPlug;
    [SerializeField] //�R�[�h���~�肽�Ƃ��̔���G���A
    private GameObject detectionSlideDown;
    private detectionPlayerOn hasSlideDown;

    [SerializeField] //�ςݖ؂̍ŏ��̂��
    private GameObject firstBuildingBlock;
    //�ςݖ؂ɐG�ꂽ���̔��������
    private detectionPlayerOn isPlayerOnBuildingBlock;

    [SerializeField] //���b�̃C�x���g�̂Ƃ���
    private GameObject monster;
    [SerializeField]
    private GameObject monsterArrowPos;
    private blockWayMonster blockWayMonster;

    [SerializeField]
    private GameObject squirrel;
    //���X�̔���G���A
    private detectionPlayerOn isPlayerOnSquirrel;

    [SerializeField] //�ԃq�[���[
    private GameObject redHero;

    [SerializeField] //�f�X�N���C�g
    private GameObject desklight;

    [SerializeField] //�S�[��
    private GameObject goal;

    private void Start()
    {
        _guideArrow = guideArrowObj.GetComponent<guideArrow>();
        target = radioPlug;
        hasSlideDown = detectionSlideDown.GetComponent<detectionPlayerOn>();
        isPlayerOnBuildingBlock = firstBuildingBlock.GetComponent<detectionPlayerOn>();
        blockWayMonster = monster.GetComponent<blockWayMonster>();
        hasSlideDown = detectionSlideDown.GetComponent<detectionPlayerOn>();
        isPlayerOnSquirrel = squirrel.GetComponent<detectionPlayerOn>();
    }

    private void Update()
    {
        if (_guideArrow.target != target.transform)
            _guideArrow.target = target.transform;

        if (blockWayMonster.hasOpenedWay)
        {
            target = goal;
            guideArrowObj.transform.localScale = _guideArrow.firstScale * magnification;
        }
        else if (blockWayMonster.existRedHero == true)
        {
            target = desklight;
            guideArrowObj.transform.localScale = _guideArrow.firstScale;
        }
        else if(isPlayerOnSquirrel.isPlayerOn)
        {
            target = redHero;
            guideArrowObj.transform.localScale = _guideArrow.firstScale;
        }
        else if (blockWayMonster.firstThreaten == true)
        {
            target = squirrel;
            guideArrowObj.transform.localScale = _guideArrow.firstScale * magnification;
        }
        else if (isPlayerOnBuildingBlock.isPlayerOn)
        {
            target = monsterArrowPos;
            guideArrowObj.transform.localScale = _guideArrow.firstScale * magnification;
        }
        else if (hasSlideDown.isPlayerOn == true)
        {
            target = firstBuildingBlock;
            guideArrowObj.transform.localScale = _guideArrow.firstScale * magnification;
        }
        else
        {
            target = radioPlug;
            guideArrowObj.transform.localScale = _guideArrow.firstScale;
        }
    }
}
