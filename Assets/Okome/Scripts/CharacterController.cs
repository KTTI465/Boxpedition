using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.VisualScripting;
using CharacterState;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class CharacterController : MonoBehaviour
{
    private Rigidbody rb;
    private float xMovement, zMovement;
    public float movementSpeed = 0.15f;
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

    public bool infinityJump = false;
    public bool jumpInterval = false;

    private bool isFalling;
    private float prePlayerPosY;
    public float firstJumpHeight;
    public float secondJumpHeight;
    private float originFirstJumpPower;
    private float originSecondJumpPower;

    public bool canJump = true;
    public bool Switch;

    //connectingBoxの上にPlayerがくるよう位置を調整するための変数
    //connectingBoxとPlayerの大きさで次第で調整が必要
    private float enterBoxMove = 0.1f;

    public List<GameObject> InteractGameObjectsList = new List<GameObject>();

    [NonEditable]
    public bool isInteracting;

    public LayerMask layerMask;

    private string _preStateName;

    public CharacterStateProcessor StateProcessor { get; set; } = new CharacterStateProcessor();
    public CharacterStateIdle StateIdle { get; set; } = new CharacterStateIdle();
    public CharacterStateMove StateMove { get; set; } = new CharacterStateMove();
    public CharacterStateMoveGlass StateMoveGlass { get; set; } = new CharacterStateMoveGlass();
    public CharacterStateMoveMat StateMoveMat { get; set; } = new CharacterStateMoveMat();
    public CharacterStateJump1 StateJump1 { get; set; } = new CharacterStateJump1();
    public CharacterStateJump2 StateJump2 { get; set; } = new CharacterStateJump2();
    public CharacterStateTrampolineSmallJump StateTrampSmall { get; set; } = new CharacterStateTrampolineSmallJump();
    public CharacterStateTrampolineBigJump StateTrampBig { get; set; } = new CharacterStateTrampolineBigJump();


    [SerializeField]
    private Animator charaAnimator;

    bool jumpAnim = false;
    private string comparetarget;

    //パーティクル2種類を格納するための変数
    [SerializeField] private ParticleSystem particle1;
    [SerializeField] private ParticleSystem particle2;

    //パーティクルの再生速度
    [SerializeField] private float particlePlaySpeed = 1f;

    [SerializeField]
    private PhysicMaterial physicMaterial;

    [SerializeField]
    private Vector3 createBoxPosBias = Vector3.zero;

    private Vector3 movevel;

    [System.NonSerialized]
    public bool isUsingJumpGravity = true;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cameraRot = cam.transform.localRotation;
        characterRot = transform.localRotation;
        cameraRot = Quaternion.Euler(0, 0, 0);

        originFirstJumpPower = firstJumpPower;
        originSecondJumpPower = secondJumpPower;

        StateIdle.ExecAction = Idle;
        StateMove.ExecAction = Move;
        StateMoveGlass.ExecAction = MoveGlass;
        StateMoveMat.ExecAction = MoveMat;
        StateJump1.ExecAction = Jump1;
        StateJump2.ExecAction = Jump2;
        StateTrampSmall.ExecAction = TrampSmall;
        StateTrampBig.ExecAction = TrampBig;
        StateProcessor.State = StateIdle;

        //connectingBoxが無かったときに呼び出す
        if (connectingBox == null)
        {
            //connectingBox としてboxをPlayerと同じ位置と向きで生成
            connectingBox = Instantiate(box, new Vector3(transform.position.x + createBoxPosBias.x, transform.position.y + createBoxPosBias.y, transform.position.z + createBoxPosBias.z), transform.rotation);

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
                    else if (comparetarget == "Mat")
                    {
                        StateProcessor.State = StateMoveMat;
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

        var main1 = particle1.main;
        main1.simulationSpeed = particlePlaySpeed;
        var main2 = particle2.main;
        main2.simulationSpeed = particlePlaySpeed;

        CheatMode();

        if (infinityJump)
        {
            //connectingBoxが無いとき
            if (connectingBox == null)
            {
                //connectingBoxとして新しくboxをPlayerと同じ位置と向きに生成
                connectingBox = Instantiate(box, new Vector3(transform.position.x + createBoxPosBias.x, transform.position.y + createBoxPosBias.y, transform.position.z + createBoxPosBias.z), transform.rotation);

                //connectingBoxの上にPlayerがくるように位置を調整
                transform.position = new Vector3(transform.position.x, transform.position.y + enterBoxMove, transform.position.z);

                //connectingBoxの親オブジェクトにこのオブジェクトを指定
                connectingBox.transform.parent = gameObject.transform;

            }
            //二段ジャンプした判定をfalseにする
            doubleJumped = false;
        }
    }

    private void FixedUpdate()
    {
        CharacterMovement();
        //CharacterRotate();
        /* if (isUsingJumpGravity == true)
          {
              firstJumpPower = originFirstJumpPower;
              secondJumpPower = originSecondJumpPower;
              //落ちていない
              if (isFalling == false)
              {
                  //重力を追加する
                  if (doubleJumped == true)
                  {
                      rb.AddForce(-(Physics.gravity + ((Mathf.Pow(secondJumpPower, 2) / (2 * secondJumpHeight)) * Vector3.up)), ForceMode.Force);
                  }
                  else if (jumped == true && doubleJumped == false)
                  {
                      rb.AddForce(-(Physics.gravity + ((Mathf.Pow(firstJumpPower, 2) / (2 * firstJumpHeight)) * Vector3.up)), ForceMode.Force);
                  }

                  //落ちているとき
                  if (transform.position.y < prePlayerPosY)
                  {
                      isFalling = true;
                  }
              }
              else
              {
                  //落ちていないとき
                  if (isGround == true || transform.position.y >= prePlayerPosY)
                  {
                      isFalling = false;
                  }
              }
          }
          else
          {
              firstJumpPower = 16f;
              secondJumpPower = 20f;
          }
          prePlayerPosY = transform.position.y;*/
    }

    private void CharacterMovement()
    {
        //xMovement = Input.GetAxisRaw("Horizontal") * movementSpeed;
        //zMovement = Input.GetAxisRaw("Vertical") * movementSpeed;
        //transform.Translate(xMovement, 0, zMovement);

        if (Switch == false) //特定条件下では移動できないように設定した(荻谷
        {
            xMovement = Input.GetAxisRaw("Horizontal");
            zMovement = Input.GetAxisRaw("Vertical");
        }
        else
        {
            xMovement = 0;
            zMovement = 0;
        }
        if (Input.GetAxisRaw("Horizontal") == 0 && Input.GetAxisRaw("Vertical") == 0)
        {
            charaAnimator.SetBool("walk", false); // アニメーション切り替え

            movevel = new Vector3(transform.forward.x, 0, transform.forward.z).normalized * 0;
            rb.velocity = new Vector3(movevel.x, rb.velocity.y, movevel.z);
        }
        else
        {
            if (Switch == false)
            {
                Vector3 position = new Vector3(transform.position.x + xMovement, transform.position.y, transform.position.z + zMovement);
                Vector3 diff = position - transform.position;
                //ベクトルの大きさが0.01以上の時に向きを変える処理をする
                if (diff.magnitude > 0.01f)
                {
                    transform.rotation = Quaternion.LookRotation(diff); //向きを変更する
                }

                transform.Rotate(new Vector3(0, cam.transform.localEulerAngles.y, 0)); //カメラの角度を足す

                //transform.Translate(0, 0, movementSpeed);
                movevel = new Vector3(transform.forward.x, 0, transform.forward.z).normalized * movementSpeed * 100;
                rb.velocity = new Vector3(movevel.x, rb.velocity.y, movevel.z);

                charaAnimator.SetBool("walk", true); // アニメーション切り替え
            }
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
            jumpDistance = 1.4f;

            float radius = transform.lossyScale.x * 0.8f;

            isGround = Physics.SphereCast(transform.position, radius, Vector3.up * -1f, out var hits, jumpDistance, layerMask);

            //isGround = Physics.Raycast(transform.position, Vector3.up * -1f, jumpDistance, layerMask);
        }
        else
        {
            //connectingBoxが無いときにPlayerが地面についているかを判定するRayの長さ
            //Playerの大きさ次第で調整が必要
            jumpDistance = 1.5f;

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
            if (canJump == true)
            {
                //地面についていた時
                if (isGround == true && jumped == false)
                {
                    //ジャンプ力を落下中とそうでない時で変更するようにした。
                    if (rb.velocity.y > 0)
                    {
                        rb.velocity += Vector3.up * firstJumpPower;
                    }
                    else if (rb.velocity.y <= 0)
                    {
                        rb.velocity = Vector3.up * firstJumpPower;
                    }
                    StateProcessor.State = StateJump1;
                    jumped = true;
                }
                //空中にいるときかつ二段ジャンプをしていない時
                else if (isGround == false && doubleJumped == false && jumped == true)
                {
                    //ジャンプ力を落下中とそうでない時で変更するようにした。
                    if (rb.velocity.y > 0)
                    {
                        rb.velocity += Vector3.up * secondJumpPower;
                    }
                    else if (rb.velocity.y <= 0)
                    {
                        rb.velocity = Vector3.up * secondJumpPower;
                    }

                    //boxについているスクリプトのコルーチンを使い、１秒後に箱が消えるようにする
                    IEnumerator destroyTimer = connectingBox.GetComponent<Box>().DestroyBox();
                    StartCoroutine(destroyTimer);

                    //boxには親に追従させるためにRigidbodyがついていないので下に落ちるようにRigidbodyをつける
                    connectingBox.AddComponent<Rigidbody>();

                    //connectingBoxが下に落ちるようにこのオブジェクトの子からはずす
                    connectingBox.transform.parent = null;

                    //connectingBoxの座標にエフェクトを生成する
                    Instantiate(particle1.gameObject, connectingBox.transform.position, Quaternion.Euler(-90, 0, 0));
                    Instantiate(particle2.gameObject, connectingBox.transform.position, Quaternion.Euler(-90, 0, 0));

                    //格納されているconnectingBoxをはずす
                    connectingBox = null;

                    charaAnimator.SetBool("jump2", true); // アニメーション切り替え

                    //二段ジャンプした判定をtrueにする
                    StateProcessor.State = StateJump2;
                    doubleJumped = true;
                }
            }
        }

        //二段ジャンプをした後の時地面についた場合
        if (isGround == true && doubleJumped == true)
        {
            //connectingBoxが無いとき
            if (connectingBox == null)
            {
                //connectingBoxとして新しくboxをPlayerと同じ位置と向きに生成
                connectingBox = Instantiate(box, new Vector3(transform.position.x + createBoxPosBias.x, transform.position.y + createBoxPosBias.y, transform.position.z + createBoxPosBias.z), transform.rotation);

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
            physicMaterial.dynamicFriction = 0.6f;
            physicMaterial.staticFriction = 0.6f;
        }
        else
        {
            jumped = true;
            physicMaterial.dynamicFriction = 0.0f;
            physicMaterial.staticFriction = 0.0f;
        }
    }

    public Box GetBox()
    {
        if (connectingBox == null) return null;
        else return connectingBox.GetComponent<Box>();
    }

    public void StateTransition()
    {

    }

    public void CheatMode()
    {
        if (Gamepad.current.rightShoulder.isPressed && Gamepad.current.leftShoulder.isPressed
            && Gamepad.current.rightTrigger.isPressed && Gamepad.current.leftTrigger.isPressed
            && jumpInterval == false)
        {
            if (infinityJump == true)
            {
                infinityJump = false;
                jumpInterval = true;
                Invoke("JumpInterval", 1.0f);
            }
            else
            {
                infinityJump = true;  //無限ジャンプ
                jumpInterval = true;
                Invoke("JumpInterval", 1.0f);
            }
        }

        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))  //ワープ
        {
            if (SceneManager.GetActiveScene().name == "Stage1_New")
            {
                if (Input.GetKey(KeyCode.Alpha1))
                {
                    this.gameObject.transform.position = new Vector3(2.79f, -7.9f, 37.12f);
                }
                else if (Input.GetKey(KeyCode.Alpha2))
                {
                    this.gameObject.transform.position = new Vector3(-2.9f, 18.5f, -64.5f);
                }
                else if (Input.GetKey(KeyCode.Alpha3))
                {
                    this.gameObject.transform.position = new Vector3(-2.9f, 51.3f, -131.7f);
                }
                else if (Input.GetKey(KeyCode.Alpha4))
                {
                    this.gameObject.transform.position = new Vector3(10.2f, 39.2f, -220.3f);
                }
                else if (Input.GetKey(KeyCode.Alpha5))
                {
                    this.gameObject.transform.position = new Vector3(44.8f, 77.1f, -275.1f);
                }
            }
            else if (SceneManager.GetActiveScene().name == "Stage2")
            {
                if (Input.GetKey(KeyCode.Alpha1))
                {
                    this.gameObject.transform.position = new Vector3(26.3f, 77.29f, -289f);
                }
                else if (Input.GetKey(KeyCode.Alpha2))
                {
                    this.gameObject.transform.position = new Vector3(113.4f, -43.5f, -301.6f);
                }
                else if (Input.GetKey(KeyCode.Alpha3))
                {
                    this.gameObject.transform.position = new Vector3(144.7f, -20.7f, -226.5f);
                }
                else if (Input.GetKey(KeyCode.Alpha4))
                {
                    this.gameObject.transform.position = new Vector3(253f, 14.5f, -275.7f);
                }
                else if (Input.GetKey(KeyCode.Alpha5))
                {
                    this.gameObject.transform.position = new Vector3(148.3f, -22f, -141.9f);
                }
            }
        }
    }

    void JumpInterval()
    {
        jumpInterval = false;
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
    public void MoveMat()
    {
        //Debug.Log("CharacterStateがMoveMatに状態遷移しました。");
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

    public void SwitchON()
    {
        Switch = true;
    }
    public void SwitchOFF()
    {
        Switch = false;
    }
}