using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stage2GuideArrowManager : MonoBehaviour
{
    [SerializeField] //プレイヤーの矢印を格納
    private GameObject guideArrowObj;
    private guideArrow _guideArrow;

    //矢印が向くオブジェクト
    private GameObject target;

    [SerializeField]
    private float magnification;

    [SerializeField] //ラジカセのコンセント
    private GameObject radioPlug;
    [SerializeField] //コードを降りたときの判定エリア
    private GameObject detectionSlideDown;
    private detectionPlayerOn hasSlideDown;

    [SerializeField] //積み木の最初のやつ
    private GameObject firstBuildingBlock;
    //積み木に触れたかの判定をする
    private detectionPlayerOn isPlayerOnBuildingBlock;

    [SerializeField] //怪獣のイベントのところ
    private GameObject monster;
    [SerializeField]
    private GameObject monsterArrowPos;
    private blockWayMonster blockWayMonster;

    [SerializeField]
    private GameObject squirrel;
    //リスの判定エリア
    private detectionPlayerOn isPlayerOnSquirrel;

    [SerializeField] //赤ヒーロー
    private GameObject redHero;

    [SerializeField] //デスクライト
    private GameObject desklight;

    [SerializeField] //ゴール
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
