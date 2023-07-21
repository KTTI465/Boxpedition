using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.VisualScripting;
using CharacterState;
using UnityEngine.UIElements;

public class CharacterController : MonoBehaviour
{
    private Rigidbody rb;
    private float xMovement, zMovement;
    private float movementSpeed = 0.1f;
    [SerializeField] private Camera playerCam;
    public GameObject cam;
    private Quaternion cameraRot, characterRot;
    private float sensitivity = 1f;

    //生成するためのboxのPrefabを格納するための変数
    [SerializeField]
    private GameObject box;

    //Playerの子オブジェクトになっているboxを格納するための変数
    private GameObject connectingBox;

    //地面にRayが付いているかの判定
    public bool isGround;

    //1回目のジャンプするときの力を指定するための変数
    public float firstJumpPower;

    //2回目のジャンプするときの力を指定するための変数
    public float secondJumpPower;

    //Raycastの長さを格納するための変数
    private float jumpDistance;

    //ジャンプをしたかを判定する
    private bool jumped = false;

    //二段ジャンプをしたかを判定する
    public bool doubleJumped = false;

    //connectingBoxの上にPlayerがくるよう位置を調整するための変数
    //connectingBoxとPlayerの大きさで次第で調整が必要
    private float enterBoxMove = 0.1f;

    public List<GameObject> InteractGameObjectsList = new List<GameObject>();

    public LayerMask layerMask;

    private string _preStateName;

    public CharacterStateProcessor StateProcessor { get; set; } = new CharacterStateProcessor();
    public CharacterStateIdle StateIdle { get; set; } = new CharacterStateIdle();
    public CharacterStateMove StateMove { get; set; } = new CharacterStateMove();
    public CharacterStateMoveGlass StateMoveGlass { get; set; } = new CharacterStateMoveGlass();
    public CharacterStateMoveBook StateMoveBook { get; set; } = new CharacterStateMoveBook();
    public CharacterStateJump1 StateJump1 { get; set; } = new CharacterStateJump1();
    public CharacterStateJump2 StateJump2 { get; set; } = new CharacterStateJump2();
    public CharacterStateTrampolineSmallJump StateTrampSmall { get; set; } = new CharacterStateTrampolineSmallJump();
    public CharacterStateTrampolineBigJump StateTrampBig { get; set; } = new CharacterStateTrampolineBigJump();


    [SerializeField]
    private Animator charaAnimator;

    bool jumpAnim = false;
    private string comparetarget;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cameraRot = cam.transform.localRotation;
        characterRot = transform.localRotation;
        cameraRot = Quaternion.Euler(0, 0, 0);


        StateIdle.ExecAction = Idle;
        StateMove.ExecAction = Move;
        StateMoveGlass.ExecAction = MoveGlass;
        StateMoveBook.ExecAction = MoveBook;
        StateJump1.ExecAction = Jump1;
        StateJump2.ExecAction = Jump2;
        StateTrampSmall.ExecAction = TrampSmall;
        StateTrampBig.ExecAction = TrampBig;
        StateProcessor.State = StateIdle;

        //connectingBoxが無かったときに呼び出す
        if (connectingBox == null)
        {
            //connectingBox としてboxをPlayerと同じ位置と向きで生成
            connectingBox = Instantiate(box, new Vector3(transform.position.x, transform.position.y + 1.15f, transform.position.z), transform.rotation);

            //PlayerがconnectingBox の上に来るように移動
            transform.position = new Vector3(transform.position.x, transform.position.y + enterBoxMove, transform.position.z);

            //このオブジェクトをconnectingBox の親オブジェクトにする
            connectingBox.transform.parent = gameObject.transform;

        }
    }
    void OnCollisionEnter(Collision col)
    {
        this.comparetarget = col.gameObject.tag;
    }

    void Update()
    {
        if (xMovement != 0 || zMovement != 0)
        {
            if (!jumped || !doubleJumped)
            {
                //追加 nakajima
                if (this.comparetarget != null)
                {
                    if (comparetarget == "glass")
                    {
                        StateProcessor.State = StateMoveGlass;
                    }
                    else if (comparetarget == "book")
                    {
                        StateProcessor.State = StateMoveBook;
                    }
                    else if (comparetarget == "Untagged")
                    {
                        StateProcessor.State = StateMove;
                    }
                }
            }
        }
        else if (xMovement == 0 && zMovement == 0)
        {
            if (!jumped || !doubleJumped) StateProcessor.State = StateIdle;
        }
        CharacterJump();

        //ステートの値が変更されたら実行処理を行う
        if (StateProcessor.State.GetStateName() != _preStateName)
        {
            _preStateName = StateProcessor.State.GetStateName();
            StateProcessor.Execute();
        }
    }

    private void FixedUpdate()
    {
        CharacterMovement();
        //CharacterRotate();
    }

    private void CharacterMovement()
    {
        //xMovement = Input.GetAxisRaw("Horizontal") * movementSpeed;
        //zMovement = Input.GetAxisRaw("Vertical") * movementSpeed;
        //transform.Translate(xMovement, 0, zMovement);

        xMovement = Input.GetAxisRaw("Horizontal");
        zMovement = Input.GetAxisRaw("Vertical");

        if (Input.GetAxisRaw("Horizontal") == 0 && Input.GetAxisRaw("Vertical") == 0)
        {
            charaAnimator.SetBool("walk", false); // アニメーション切り替え
        }
        else
        {
            Vector3 position = new Vector3(transform.position.x + xMovement, transform.position.y, transform.position.z + zMovement);
            Vector3 diff = position - transform.position;

            //ベクトルの大きさが0.01以上の時に向きを変える処理をする
            if (diff.magnitude > 0.01f)
            {
                transform.rotation = Quaternion.LookRotation(diff); //向きを変更する
            }

            transform.Rotate(new Vector3(0, cam.transform.localEulerAngles.y, 0)); //カメラの角度を足す

            transform.Translate(0, 0, movementSpeed);
            charaAnimator.SetBool("walk", true); // アニメーション切り替え
        }
    }

    private void CharacterRotate()
    {
        float cal = PlayerPrefs.GetFloat("Sensi");  //マウス感度を取得してる（重要）
        //float cal = 1f;

        //マウスの横方向の動き× sensitivityで横方向の回転をさせている。
        float xRot = Input.GetAxis("Mouse X") * sensitivity;

        // ゲームパッドが接続されていないとき
        if (Gamepad.current == null)
        {
            characterRot *= Quaternion.Euler(0, xRot * 2.0f * cal, 0);
        }
        else
        {
            // 右スティックの入力を受け取る
            var v = Gamepad.current.rightStick.ReadValue();

            if (xRot == 0)
            {
                characterRot *= Quaternion.Euler(0, v.x * 2.0f * cal, 0);
            }
            else
            {
                characterRot *= Quaternion.Euler(0, xRot * 2.0f * cal, 0);
            }
        }

        transform.localRotation = characterRot;
    }

    private void OnDrawGizmos()
    {
        float radius = transform.lossyScale.x * 0.8f;
        Gizmos.DrawWireSphere(transform.position + transform.up * -jumpDistance, radius);
    }

    private void CharacterJump()
    {
        //connectingBoxがあるとき
        if (connectingBox)
        {
            //connectingBoxがあることも加味してのPlayerが地面についているかを判定するRayの長さ　
            //値は変更する必要あり（今は埋め込みで実装できていないのでこの値）
            //箱とPlayerの大きさ次第でも調整が必要
            jumpDistance = 0.7f;

            float radius = transform.lossyScale.x * 0.8f;

            isGround = Physics.SphereCast(transform.position, radius, Vector3.up * -1f, out var hits, jumpDistance, layerMask);
            
            //isGround = Physics.Raycast(transform.position, Vector3.up * -1f, jumpDistance, layerMask);
        }
        else
        {
            //connectingBoxが無いときにPlayerが地面についているかを判定するRayの長さ
            //Playerの大きさ次第で調整が必要
            jumpDistance = 0.7f;

            float radius = transform.lossyScale.x * 0.8f;

            isGround = Physics.SphereCast(transform.position, radius, Vector3.up * -1f, out var hits, jumpDistance, layerMask);
            //isGround = Physics.Raycast(transform.position, Vector3.up * -1f, jumpDistance, layerMask);
        }

        //地面から離れたとき
        if (isGround == false && jumpAnim == false)
        {
            jumpAnim = true;
            charaAnimator.SetBool("jump", true); // アニメーション切り替え
        }

        //地面についたとき
        if (isGround == true && jumpAnim == true)
        {
            jumpAnim = false;
            charaAnimator.SetBool("jump", false); // アニメーション切り替え
        }

        charaAnimator.SetBool("jump2", false); // アニメーション切り替え

        // ×ボタンが押されているかどうかを取得する
        var ps4X = false;

        if (Gamepad.current != null)
        {
            if (Gamepad.current.buttonSouth.wasPressedThisFrame)
            {
                ps4X = true;
            }
        }


        //スペースキー（×ボタン）を押したときにジャンプする
        if (Input.GetKeyDown(KeyCode.Space) || ps4X)
        {
            //地面についていた時
            if (isGround == true && jumped == false)
            {
                rb.velocity = Vector3.up * firstJumpPower;
                StateProcessor.State = StateJump1;
                jumped = true;
            }
            //空中にいるときかつ二段ジャンプをしていない時
            else if (isGround == false && doubleJumped == false && jumped == true)
            {
                rb.velocity = Vector3.up * secondJumpPower;

                //boxについているスクリプトのコルーチンを使い、１秒後に箱が消えるようにする
                IEnumerator destroyTimer = connectingBox.GetComponent<Box>().DestroyBox();
                StartCoroutine(destroyTimer);

                //boxには親に追従させるためにRigidbodyがついていないので下に落ちるようにRigidbodyをつける
                connectingBox.AddComponent<Rigidbody>();

                //connectingBoxが下に落ちるようにこのオブジェクトの子からはずす
                connectingBox.transform.parent = null;

                //格納されているconnectingBoxをはずす
                connectingBox = null;

                charaAnimator.SetBool("jump2", true); // アニメーション切り替え

                //二段ジャンプした判定をtrueにする
                StateProcessor.State = StateJump2;
                doubleJumped = true;
            }
        }

        //二段ジャンプをした後の時地面についた場合
        if (isGround == true && doubleJumped == true)
        {
            //connectingBoxが無いとき
            if (connectingBox == null)
            {
                //connectingBoxとして新しくboxをPlayerと同じ位置と向きに生成
                connectingBox = Instantiate(box, new Vector3(transform.position.x, transform.position.y + 1.15f, transform.position.z), transform.rotation);

                //connectingBoxの上にPlayerがくるように位置を調整
                transform.position = new Vector3(transform.position.x, transform.position.y + enterBoxMove, transform.position.z);

                //connectingBoxの親オブジェクトにこのオブジェクトを指定
                connectingBox.transform.parent = gameObject.transform;

            }
            //二段ジャンプした判定をfalseにする
            doubleJumped = false;
        }

        if (isGround == true)
        {
            jumped = false;
            doubleJumped = false;
        }
        else
        {
            jumped = true;
        }
    }

    public void StateTransition()
    {

    }

    public void Idle()
    {
        //Debug.Log("CharacterStateがIdleに状態遷移しました。");
    }
    public void Move()
    {
        //Debug.Log("CharacterStateがMoveに状態遷移しました。");
    }
    public void MoveGlass()
    {
        //Debug.Log("CharacterStateがMoveGlassに状態遷移しました。");
    }
    public void MoveBook()
    {
        //Debug.Log("CharacterStateがMoveBookに状態遷移しました。");
    }
    public void Jump1()
    {
        //Debug.Log("CharacterStateがJump1に状態遷移しました。");
    }
    public void Jump2()
    {
        //Debug.Log("CharacterStateがJump2に状態遷移しました。");
    }
    public void TrampSmall()
    {
        UnityEngine.Debug.Log("CharacterStateがTrampSmallに状態遷移しました。");
    }
    public void TrampBig()
    {
        UnityEngine.Debug.Log("CharacterStateがTrampBigに状態遷移しました。");
    }
}