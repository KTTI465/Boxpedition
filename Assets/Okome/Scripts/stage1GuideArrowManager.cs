using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stage1GuideArrowManager : MonoBehaviour

{
    //プレイヤーの矢印を格納
    [SerializeField]
    private GameObject guideArrowObj;
    private guideArrow _guideArrow;

    [SerializeField]　//大きくするときの倍率
    private float magnification;

    //矢印が向くオブジェクト
    private GameObject target;

    [SerializeField]　//最初の棚
    private GameObject detectionFirstShelf;

    //最初の棚の上にきたのを判定する
    private detectionPlayerOn isPlayerOnFirstShelf;

    [SerializeField]　//テレビの上の毛糸玉
    private GameObject ballOfWoolOnTV;
    private ballOfWool ballOfWoolEnabledAnimation;

    [SerializeField]　//ぬいぐるみがある段のひとつ上の段の足場
    private GameObject detectionScaffold;

    //引き出し上の足場にきたのを判定する
    private detectionPlayerOn isPlayerOnScaffold;

    [SerializeField]　//ゴール
    private GameObject goal;

    [SerializeField]
    private CheckPointManager checkPointManager;

    [SerializeField]　//大きな机の上のチェックポイント
    private GameObject checkPoint1;

    [SerializeField]　//引き出しの前の足場
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
        //ターゲットオブジェクトの更新
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
        //大きな机にのった時
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