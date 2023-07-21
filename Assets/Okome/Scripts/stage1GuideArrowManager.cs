using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stage1GuideArrowManager : MonoBehaviour

{
    //プレイヤーの矢印を格納
    [SerializeField]
    private guideArrow _guideArrow;

    //矢印が向くオブジェクト
    private GameObject target;

    [SerializeField]　//DVDの棚
    private GameObject DVDShelf;

    [SerializeField]　//積み木
    private GameObject buildingBlockObj;
    private buildingBlock _buildingBlock; 

    [SerializeField]
    private GameObject detectionBuildingBlockAreaObj;
    private detectionBuildingBlockArea _detectionBuildingBlockArea;

    //[SerializeField]  //スリングショット
    //private GameObject slingShot;

    [SerializeField]　//テレビの上の毛糸玉
    private GameObject ballOfWoolOnTV;
    private ballOfWool ballOfWoolEnabledAnimation;

    [SerializeField]　//引き出し
    private GameObject drawer;

    [SerializeField]
    private GameObject detectionScaffold;

    //引き出し上の足場にきたのを判定する
    private detectionPlayerOnScaffold playerOnScaffold;

    [SerializeField]　//ゴール
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
        //ターゲットオブジェクトの更新
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

