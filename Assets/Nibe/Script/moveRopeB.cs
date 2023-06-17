using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.VisualScripting;

public class moveRopeB : MonoBehaviour
{
    private float xMovement, zMovement;
    private float movementSpeed = 0.1f;  //相殺用

    //接触したかどうかの判定
    private bool moveOn = false;

    //ロープを掴んでいるかどうかの判定
    private bool grabbingRope = false;

    //上っているかどうかの判定
    private bool climbing = false;
    //下っているかどうかの判定
    private bool climbingDown = false;

    //プレイヤーのrigidbody格納用変数
    new Rigidbody rigidbody;
    GameObject player;

    //プレイヤーがロープを上るスピード
    public float moveSpeed;

    //プレイヤーがロープを移動する距離
    public float ropeMoveDistance;

    //PlayerのCharacterControllerを格納
    private CharacterController characterController;

    //ロープをうごく先の位置
    private Vector3 climbPos;

    //キャラクターの位置を補正する値
    private float positionCorrection = 1.2f;

    //ロープの上から下までの距離
    private float ropeDistance;

    //Rayが当たるものが無いときのロープの長さ
    public float length;

    //ロープを掴んでいるときの前後移動の速さ
    public float forwardMoveSpeed;

    //ロープを掴んでいるときの左右移動の速さ
    public float sideMoveSpeed;

    //下の位置
    private Vector3 underPosition;

    [SerializeField]//ロープを掴んでいる判定をするColliderを格納
    private CapsuleCollider grabRopeCollider;

    [SerializeField]//ロープにつかめる判定をするColliderを格納
    private CapsuleCollider moveOnCollider;

    //ロープを掴んだ時の距離を格納する
    private float grabDist;

    [SerializeField] //Playerのジャンプ用のrayをとめるオブジェクトを格納
    private GameObject rayStopper;

    private GameObject _rayHitObject;

    [SerializeField]//キーボードマウス操作のときのインタラクトの画像
    private GameObject interactImageKeyboardMouse;

    [SerializeField]//パッド操作のときのインタラクトの画像
    private GameObject interactImageGamepad;

    private GameObject interactImage;

    [SerializeField]//キーボードマウス操作のときの上るの画像
    private GameObject climbUpImageKeyboardMouse;

    [SerializeField]//パッド操作のときの上るの画像
    private GameObject climbUpImageGamepad;

    private GameObject climbUpImage;

    [SerializeField]//キーボードマウス操作のときの下りるの画像
    private GameObject climbDownImageKeyboardMouse;

    [SerializeField]//パッド操作のときの下りるの画像
    private GameObject climbDownImageGamepad;

    private GameObject climbDownImage;

    // ×ボタンが押されているかどうかを取得する
    bool ps4X = false;
    // yボタンが押されているかどうかを取得する
    bool ps4Y = false;

    [SerializeField]
    private Animator charaAnimator;

    void Start()
    {
        //プレイヤーを見つける
        player = GameObject.FindGameObjectWithTag("Player");
        rigidbody = player.gameObject.GetComponent<Rigidbody>();
        characterController = player.gameObject.GetComponent<CharacterController>();

        //下の位置と下までの距離を設定
        if(length > 0)
        {
            //Rayが当たるものが無いときのunderPositionの指定
            underPosition = transform.position - (transform.up * length) + (transform.up * positionCorrection);
        }
        else if(length == 0 && Physics.Raycast(transform.position, transform.up * -1f, out RaycastHit hit))
        {
            underPosition = hit.point;
        }
        ropeDistance = Vector3.Distance(transform.position, underPosition);

        //Colliderの大きさや位置を指定
        moveOnCollider.center = - new Vector3(0, transform.up.y * (ropeDistance / 2), 0);
        grabRopeCollider.center = - new Vector3(0, transform.up.y * (ropeDistance / 2), 0);
        moveOnCollider.height = ropeDistance + moveOnCollider.radius * 1.5f;
        grabRopeCollider.height = ropeDistance;

        //レイヤ―の変更
        gameObject.layer = LayerMask.NameToLayer("IgnoreCameraRay");
    }

    // Update is called once per frame
    void Update()
    {
        GetPS4XY();
        ImageChange();

        if (grabbingRope == true)
        {
            //キャラクターを操作できないように
            characterController.enabled = false;

            if (grabDist == 0)
            {
                //掴んだ時点での距離を測り、それ以上離れたらロープを離すようにする
                grabDist = Vector2.Distance(new Vector2(transform.position.x, transform.position.z),
                    new Vector2(player.transform.position.x, player.transform.position.z));
            }        
            moveOnCollider.radius = grabDist -0.5f;

            if(interactImage == true)
            {
                interactImage.SetActive(false);
            }
            //重力を停止させる
            rigidbody.isKinematic = true;

            //ジャンプ用のレイをとめるオブジェクトの位置を設定
            rayStopper.transform.position = player.transform.position - player.transform.up * 1.3f;

            charaAnimator.SetBool("climb", false); // アニメーション切り替え



            //上の制限位置より下にいるとき
            if (player.transform.position.y < transform.position.y + positionCorrection)
            {
                //ロープで動いていないとき
                if (climbing == false && climbingDown == false)
                {
                    //上るの画像を表示
                    climbUpImage.SetActive(true);

                    //上るボタンを押すと上る
                    if (Input.GetKeyDown(KeyCode.Space) || ps4X)
                    {
                        climbPos = new Vector3(player.transform.position.x, player.transform.position.y + ropeMoveDistance, player.transform.position.z);
                        climbing = true;
                    } 
                }
                else
                {
                    //上るの画像を非表示
                    climbUpImage.SetActive(false);
                }
            }
            //上の制限位置より上にいるとき上っていかないようにした
            else if (player.transform.position.y >= transform.position.y + positionCorrection &&
                climbing == true)
            {             
                climbPos = player.transform.position;
                characterController.enabled = true;
                climbing = false;
            }

            //下の制限位置より上にいるとき
            if (player.transform.position.y > underPosition.y + positionCorrection)
            {
                //ロープで動いていないとき
                if (climbing == false && climbingDown == false)
                {
                    //下るの画像を表示
                    climbDownImage.SetActive(true);

                    //下りるボタンを押すと下りる
                    if (Input.GetKeyDown(KeyCode.LeftShift) || ps4Y)
                    {
                        climbPos = new Vector3(player.transform.position.x, player.transform.position.y - ropeMoveDistance, player.transform.position.z);
                        climbingDown = true;
                    }
                }
                else
                {
                    //下るの画像を非表示
                    climbDownImage.SetActive(false);
                }
            }
            //下の制限位置より下にいるときは下りていかないようにした
            else if (player.transform.position.y <= underPosition.y + positionCorrection &&
                climbingDown == true)
            {
                climbPos = player.transform.position;
                characterController.enabled = true;
                climbingDown = false;
            }

            //登っているときまたは降りているとき
            if (climbing || climbingDown)
            {
                //移動位置と同じ位置になったら移動中のboolをfalseにする
                if (player.transform.position == climbPos)
                {
                    climbing = false;
                    climbingDown = false;
                }
                else
                {
                    //移動先の位置と同じ位置で無いときは移動をする
                    player.transform.position = Vector3.MoveTowards(player.transform.position, climbPos, moveSpeed * Time.deltaTime);

                    if (climbing)
                    {
                        charaAnimator.SetBool("climb", true); // アニメーション切り替え
                    }
                }
            }
            else
            {
                Debug.Log("W");
                //登っていないときまたは降りていないときはロープで操作できるように
                float xMovement = Input.GetAxisRaw("Horizontal") * -forwardMoveSpeed;
                float zMovement = Input.GetAxisRaw("Vertical");
                if(Vector2.Distance(new Vector2(transform.position.x, transform.position.z),
                    new Vector2(player.transform.position.x, player.transform.position.z)) <= 1f)
                {
                    zMovement = Mathf.Clamp(zMovement, -1, 0);
                }
                player.transform.position = player.transform.position + (player.transform.forward * (zMovement/(10 * sideMoveSpeed)));
                player.transform.LookAt(new Vector3(transform.position.x, player.transform.position.y, transform.position.z));
                player.transform.RotateAround(new Vector3(transform.position.x,player.transform.position.y, transform.position.z), transform.up, xMovement);
            }
        }
        else
        {
            //キャラクターがロープを掴んでいない時は
            //ジャンプ用のレイをとめるオブジェクトの位置を一番上に設定
            if(gameObject.transform.root == true)
            rayStopper.transform.position = new Vector3(transform.position.x, transform.position.y - 0.1f, transform.position.z);
            //キャラクターを操作できるようにする
           // characterController.enabled = true;

            //掴むことができる範囲を元に戻す
            moveOnCollider.radius = 3.0f;
            grabDist = 0;
        }

        if (moveOn == true)  //登る
        {
            if (characterController.rayHitObject != null &&
                characterController.rayHitObject == gameObject &&
                (Input.GetKeyDown(KeyCode.Space) || ps4X))
            {
                if (grabbingRope == false)
                {
                    grabbingRope = true;


                    //Rigidbodyを停止
                    rigidbody.velocity = Vector3.zero;

                    charaAnimator.SetBool("climbStay", true); // アニメーション切り替え
                }
            }
        }
        else if (moveOn == false && grabbingRope)
        {
            //重力を復活させる
            rigidbody.isKinematic = false;
            grabbingRope = false;
            interactImage.SetActive(false);
            climbUpImage.SetActive(false);
            climbDownImage.SetActive(false);

            charaAnimator.SetBool("climbStay", false); // アニメーション切り替え
        }
        //CharacterMovement();  //相殺
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            //掴むことができるように
            moveOn = true;
            interactImage.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.tag == "Player")
        {
            moveOn = false;
            interactImage.SetActive(false);
            climbUpImage.SetActive(false);
            climbDownImage.SetActive(false);
        }
    }

    private void CharacterMovement()
    {
        xMovement = Input.GetAxisRaw("Horizontal") * movementSpeed;
        zMovement = Input.GetAxisRaw("Vertical") * movementSpeed;

        player.transform.Translate(-xMovement, 0, -zMovement);  //相殺するために逆向きに力加える
    }

    void GetPS4XY()
    {
        if (Gamepad.current != null)
        {
            if (Gamepad.current.buttonSouth.isPressed)
            {
                ps4X = true;
            }
            else
            {
                ps4X = false;
            }

            if (Gamepad.current.buttonWest.isPressed)
            {
                ps4Y = true;
            }
            else
            {
                ps4Y = false;
            }
        }
    }

    void ImageChange()
    {
        if (Gamepad.current != null) //パッド操作のとき
        {
            //パッド操作のインタラクトの画像を設定
            if (interactImage != interactImageGamepad)
            {
                interactImage = interactImageGamepad;
            }
            //パッド操作のロープを上るの画像を設定
            if (climbUpImage != interactImageGamepad)
            {
                interactImage = interactImageGamepad;
            }
            //パッド操作のロープを下りるの画像を設定
            if (climbDownImage != interactImageGamepad)
            {
                interactImage = interactImageGamepad;
            }
        }
        else //キーボードマウス操作のとき
        {
            //キーボードマウス操作のインタラクトの画像を設定
            if (interactImage != interactImageKeyboardMouse)
            { 
                interactImage = interactImageKeyboardMouse;
            }
            //キーボードマウス操作のロープを上るの画像を設定
            if (climbUpImage !=  climbUpImageGamepad)
            {
                climbUpImage = climbUpImageKeyboardMouse;
            }
            //キーボードマウス操作のロープを下りるの画像を設定
            if (climbDownImage != climbDownImageKeyboardMouse)
            {
                climbDownImage = climbDownImageKeyboardMouse;
            }
        }
    }
}