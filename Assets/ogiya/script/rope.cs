using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.VisualScripting;


// ターザン用のロープ

public class rope: MonoBehaviour
{
    public int targetX;
    public int targetY;
    public int targetZ;
    private float xMovement, zMovement;
    private float movementSpeed = 0.1f;  //相殺用

    //　軸の角度
    private float angle = 0f;
    //　動き始める時の時間
    private float startTime;
    //　振り子をする角度
    [SerializeField]
    private float limitAngle = 90f;

    //　進んでいる方向
    private int direction = 1;

    //　ロープが動くかどうかの判定
    private bool moveOn = false;

    //　補間間隔
    [SerializeField]
    private float duration = 5f;

    public float angleX;
    public float angleY;
    public float angleZ;
    public bool panelOn = false;
    public bool button = false;

    //プレイヤーのrigidbody格納用変数
    new Rigidbody rigidbody;
    GameObject player;
    public GameObject panel1;
    public GameObject panel2;
    public GameObject panel1English;
    public GameObject panel2English;

    [SerializeField] Transform target;
    private float speed = 5.0f;

    public float speed2 = 100;
    [SerializeField] private Transform P;
    [Range(0.0f, 180.0f)] public float arcAngle = 60.0f;
    float Travel;
    bool Throw = false;
    bool Ysafepoint = false;

    // ×ボタンが押されているかどうかを取得する
    bool ps4X = false;
    // 〇ボタンが押されているかどうかを取得する
    bool ps40 = false;

    Vector3 pivot;
    Vector3 FromVector;
    Vector3 ToVector;

    //座標指定用のオブジェクト
    public GameObject pointobj;
    public GameObject ropeobj;

    void Start()
    {
        startTime = Time.time;

        //プレイヤーを見つける
        player = GameObject.FindGameObjectWithTag("Player");
        rigidbody = player.gameObject.GetComponent<Rigidbody>();

        if (PlayerPrefs.GetString("Language") == "English")
        {
            panel1 = panel1English;
            panel2 = panel2English;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerpos = player.transform.position;
        Vector3 setpos = pointobj.transform.position;
        Vector3 ropepos = ropeobj.transform.position;
        float arie = Vector3.Distance(setpos, ropepos);
        float Parie = Vector3.Distance(playerpos, ropepos);
        if(Parie <= 8f)
        {
            panel1.SetActive(true);
        }
        else if(Parie > 8f || panelOn == true)
        {
            panel1.SetActive(false);
        }
        //座標指定用オブジェクトと一定の距離になったら条件を満たす
        if (arie < 10f)
        {
            Ysafepoint = true;
        }
        if(arie > 10f)
        {
            Ysafepoint =false;
        }
        if(panelOn == true)
        {
            panel2.SetActive(true);
        }
        else
        {
            panel2.SetActive(false);
        }

        GetPS40();
        GetPS4X();

        //Xボタンを押したら〇ボタンを打ち消す
        if(ps4X)
        {
            ps40 = false;
        }

        if (moveOn == true && (Input.GetKey(KeyCode.Space) || ps40))  //動く
        {
            panelOn = true;
            //　経過時間に合わせた割合を計算
            float t = (Time.time - startTime) / duration;

            //　スムーズに角度を計算
            angle = Mathf.SmoothStep(angle, direction * limitAngle, t);

            //ロープの下のほうに移動する
            player.transform.position = Vector3.MoveTowards(player.transform.position, target.position, speed * Time.deltaTime);

            //CharacterMovement();  //相殺

        }
        else  //止まる
        {
            if (moveOn == true)
            {
                panelOn = false;
                moveOn = false;

                //重力を復活させる
                rigidbody.isKinematic = false;

                //親子関係を解除
                player.gameObject.transform.parent = null;
                //プレイヤーを発射
                if(Ysafepoint == true)
                {
                    Throw = true;
                }
            }
            if (angle >= -1f && angle <= 1f)
            {
                angle = 0f;
                direction = 1;
            }
            else
            {
                if (angle > 0f)
                {
                    angle -= 1f;  //元の位置に戻す
                }

                if (angle < 0f)
                {
                    angle += 0.1f;  //元の位置に戻す
                }
            }
        }
        //　角度を変更
        transform.localEulerAngles = new Vector3(angleX * angle, angleY * angle, angleZ * angle);
        //　角度が指定した角度と1度の差になったら反転
        if (Mathf.Abs(Mathf.DeltaAngle(angle, direction * limitAngle)) < 1f)
        {
            direction *= -1;
            startTime = Time.time;
        }

        if (Throw == true)
        {
            // 目標地点を設定
            Vector3 targetpos = new Vector3(targetX, targetY, targetZ);

            var targethalfAngle = Mathf.Tan(Mathf.Deg2Rad * arcAngle * 0.5f);
            var midPos = (playerpos + targetpos) * 0.5f;
            var half = Vector3.Distance(playerpos, midPos);

            pivot = midPos;
            pivot.y -= half / targethalfAngle;

            //中心から出発地、中心から目的地へのベクトルを求めておく
            FromVector = playerpos - pivot;
            ToVector = targetpos - pivot;

            //移動量を0.0にリセットしておく
            Travel = 0.0f;

            Travel += speed2 * Time.deltaTime;
            Travel += speed2 * Time.deltaTime;

            //円弧の長さで割って、円弧上を進行した割合を求める
            var t = Travel / (FromVector.magnitude * Mathf.Deg2Rad * arcAngle);

            if (t < 1.0f)
            {
                //FromVectorとToVectorを進行割合で補間し、Pivotを足して現在の位置とする
                P.transform.position = Vector3.Slerp(FromVector, ToVector, t) + pivot;
            }
            else
            {
                //tが1.0に到達したら移動終了とする
                P.transform.position = ToVector + pivot;
                Throw = false;
            }

        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            GetPS40();
            GetPS4X();

            if (Input.GetKey(KeyCode.Space) || ps40)
            {
                moveOn = true;

                startTime = Time.time;

                //Rigidbodyを停止
                rigidbody.velocity = Vector3.zero;

                //重力を停止させる
                rigidbody.isKinematic = true;

                //親子関係にする
                player.gameObject.transform.parent = this.gameObject.transform;
            }
        }
    }

    private void CharacterMovement()
    {
        xMovement = Input.GetAxisRaw("Horizontal") * movementSpeed;
        zMovement = Input.GetAxisRaw("Vertical") * movementSpeed;

        player.transform.Translate(-xMovement, 0, -zMovement);  //相殺するために逆向きに力加える
    }

    void GetPS40()
    {
        if (Gamepad.current != null)
        {
            if (ps40 == false && Gamepad.current.buttonEast.isPressed)
            {
                ps40 = true;
            }
            else if (ps40 == true && Gamepad.current.buttonEast.isPressed)
            {
                ps40 = false;
            }
        }
    }
    void GetPS4X()
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
        }
    }
}