using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.VisualScripting;

public class camera : MonoBehaviour
{
    public GameObject playerObject;         //追尾 オブジェクト
    public Vector2 rotationSpeed;           //回転速度

    //playerのオブジェクトを格納
    private GameObject Parent;

    //このカメラのローカル座標を格納
    private Vector3 Position;

    //カメラを正面に戻したときの位置
    private Vector3 firstPosition;


    private Vector3 prePosition;
    private Vector3 currentPosition;

    //カメラとプレイヤーが最も近い距離
    public float nearestCameraPosition = 3.0f;

    //カメラがものに当たっている情報
    private RaycastHit Hit;

    //カメラとプレイヤーの距離を格納
    private float Distance;

    //カメラが避けるレイやーを格納
    public LayerMask Mask;

    //カメラがオブジェクトを避けているかの判定
    private bool avoidWall;

    void Start()
    {
        Parent = transform.root.gameObject;
        firstPosition = transform.localPosition;
        if (transform.root.gameObject.CompareTag("Player"))
            gameObject.transform.parent = null;
        Position = transform.position;       
        Distance = Vector3.Distance(Parent.transform.position, transform.position);
        currentPosition = Parent.transform.position;
    }

    void FixedUpdate()
    {
        //子オブジェクトから外してカメラが移動するようにする
        prePosition = currentPosition;
        currentPosition = Parent.transform.position;
        transform.position = transform.position + (currentPosition - prePosition);

        bool stickPressed = Gamepad.current.rightStickButton.isPressed;
        if (Input.GetKey(KeyCode.H) || (stickPressed == true))
        {
            Vector3 bhindAngle = currentPosition + (Parent.transform.up * firstPosition.y) + (Parent.transform.forward * firstPosition.z);
            transform.position = bhindAngle;
        }
    }

    void Update()
    {
        Rotate();
        AvoidWall();
    }

    void Rotate()
    {
        float cal = PlayerPrefs.GetFloat("Sensi");  //マウス感度を取得してる（重要）
        transform.LookAt(Parent.transform);
        float rotX = Input.GetAxis("Mouse X") * rotationSpeed.x;
        float rotY = Input.GetAxis("Mouse Y") * rotationSpeed.y;

        if (transform.forward.y * -1f > 0.8f && rotY < 0)
        {
            rotY = 0;
        }
        if (transform.forward.y * -1f < -0.8f && rotY > 0)
        {
            rotY = 0;
        }

        // ゲームパッドが接続されていないとき
        if (Gamepad.current == null)
        {
            //プレイヤーを中心に回転する
            transform.RotateAround(playerObject.transform.position, transform.up, rotX * cal);
            transform.RotateAround(playerObject.transform.position, transform.right, -rotY * cal);
        }
        else
        {
            // 右スティックの入力を受け取る
            var v = Gamepad.current.rightStick.ReadValue();

            if (transform.forward.y * -1f > 0.8f && v.y < 0)
            {
                v.y = 0;
            }
            if (transform.forward.y * -1f < -0.8f && v.y > 0)
            {
                v.y = 0;
            }

            if (rotY == 0)
            {
                //プレイヤーを中心に回転する
                transform.RotateAround(playerObject.transform.position, transform.up, v.x * 0.5f * cal);
                transform.RotateAround(playerObject.transform.position, transform.right, -v.y * 0.5f * cal);
            }
            else
            {
                //プレイヤーを中心に回転する
                transform.RotateAround(playerObject.transform.position, transform.up, rotX * cal);
                transform.RotateAround(playerObject.transform.position, transform.right, -rotY * cal);
            }
        }
    }

    void AvoidWall()
    {
        //壁を避けていないとき
        if (avoidWall == false)
        {
            Position = transform.position;
        }
        //プレイヤーとカメラの間にオブジェクトがあるとき
        if (Physics.SphereCast(Parent.transform.position, 0.5f, (transform.position - Parent.transform.position　).normalized, out Hit, Distance, Mask))
        {      
            avoidWall = true;
            if (Hit.distance < nearestCameraPosition + 0.1f)
            {
                //カメラとプレイヤーの距離が近いときカメラが中に埋まらないように
                transform.position = Parent.transform.position + (transform.position - Parent.transform.position).normalized * nearestCameraPosition;
            }
            else
                //カメラの位置を移動
                transform.position = Parent.transform.position + (transform.position - Parent.transform.position).normalized * Hit.distance;
        }
        else//プレイヤーとカメラの間にオブジェクトがないとき
        {
            //カメラを元の位置まで移動する
            transform.localPosition = Vector3.Lerp(transform.position, Position, 2);
            if (avoidWall == true)
            {
                avoidWall = false;
            }
        }
    }
}



