using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MonsterState;

public class blockWayMonster : MonoBehaviour
{
    [SerializeField] GameObject player;
    CharacterController characterController;
    Animator playerAnimator;
    Rigidbody playerRb;

    [SerializeField] GameObject eventCamera;

    Animator monstersAnimator;

    //プレイヤーがおびえて下がっているとき
    bool isPlayerStepBack;

    //イベントが発生中か
    bool isEvent;

    //道を開けたか
    public bool hasOpenedWay;
    //赤ヒーローがいるか
    public bool existRedHero;

    //イベント発生中のプレイヤーの位置
    Vector3 inEventPlayerPosition;
    Vector3 stepBackPosition;

    [System.NonSerialized]
    public bool firstThreaten = false;

    [SerializeField]
    private GameObject offScreenArrow;

    [SerializeField] AudioClip monstersSound;

    public MonsterStateProcessor MonsterStateProcessor = new();
    public MonsterStateIdle MonsterStateIdle = new();
    public MonsterStateRun MonsterStateRun = new();
    public MonsterStateJump MonsterStateJump = new();

    void Start()
    {
        //プレイヤーとモンスターのコンポーネントの取得
        characterController = player.GetComponent<CharacterController>();
        playerAnimator = player.GetComponent<Animator>();
        monstersAnimator = GetComponent<Animator>();
        playerRb = player.GetComponent<Rigidbody>();
        //カメラを非有効に
        eventCamera.SetActive(false);

        MonsterStateProcessor.State = MonsterStateIdle;
    }

    void Update()
    {
        //イベント中
        if (isEvent == true)
        {
            offScreenArrow.SetActive(false);
            //おびえて下がっているとき
            if (isPlayerStepBack == true)
            {
                //プレイヤーの向きの設定と移動
                player.transform.rotation = Quaternion.RotateTowards(player.transform.rotation, Quaternion.Euler(0f, 180f, 0f), 900f * Time.deltaTime);
                player.transform.position = Vector3.MoveTowards(player.transform.position, stepBackPosition, 10f * Time.deltaTime);
                //プレイヤーに歩きアニメーションをさせる
                playerAnimator.SetBool("walk", true);
            }
            else
            {
                //イベント中に歩いていない時に歩きアニメーションをしないようにする
                playerAnimator.SetBool("walk", false);
            }

        }
    }

    //イベントが始まるときに呼ぶ関数
    public void StartEvent()
    {
        isEvent = true;
        playerRb.velocity = Vector3.zero;
        //プレイヤーの操作を不可にする
        characterController.Switch = true;
        characterController.canJump = false;

        //キャラクターの向きの設定
        player.transform.eulerAngles = transform.up;
        inEventPlayerPosition = new Vector3(transform.position.x - 3f, player.transform.position.y, transform.position.z - 40f);
        stepBackPosition = inEventPlayerPosition - new Vector3(0f, 0f, 15f);
        player.transform.position = inEventPlayerPosition;

        characterController.StateProcessor.State = characterController.StateIdle;
        playerAnimator.SetBool("walk", false);
        offScreenArrow.SetActive(false);
        eventCamera.SetActive(true);
    }

    public void MonstersSound()
    {
        if (existRedHero == false)
        {
            MonsterStateProcessor.State = MonsterStateJump;
        }
        else
        {
            MonsterStateProcessor.State = MonsterStateRun;
        }
    }

    public void PlayerStepBack()
    {
        isPlayerStepBack = true;
    }

    //イベントが終わるときに呼ぶ関数
    public void EndEvent()
    {
        isEvent = false;
        //キャラクターを操作可能にする
        characterController.Switch = false;
        characterController.canJump = true;

        offScreenArrow.SetActive(true);
        eventCamera.SetActive(false);
        isPlayerStepBack = false;

        MonsterStateProcessor.State = MonsterStateIdle;
    }

    public void OpenWay()
    {
        hasOpenedWay = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        //道を開けていない時
        if (hasOpenedWay == false)
        {
            if (other.gameObject == player.gameObject)
            {
                //赤ヒーローがいないなら
                if (existRedHero == false)
                {
                    monstersAnimator.SetTrigger("threaten");
                    if (firstThreaten == false)
                    {
                        firstThreaten = true;
                    }
                }
                else
                {
                    monstersAnimator.SetTrigger("openWay");
                }
            }
        }
    }
}

