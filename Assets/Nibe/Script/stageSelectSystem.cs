using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class stageSelectSystem : MonoBehaviour
{
    public GameObject stage1Text;
    public GameObject stage1Image;
    public GameObject stage2Text;
    public GameObject stage2Image;

    public GameObject rightButton;
    public GameObject leftButton;


    public bool DontDestroyEnabled = true;


    int stageNum = 1;
    int moveCount = 0;
    bool moveLeft = false;
    bool moveRight = false;

    private bool right = false;
    private bool left = false;


    // Start is called before the first frame update
    void Start()
    {
        rightButton.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (moveLeft == true)
        {
            if (moveCount <= 100)
            {
                if (stageNum == 1)
                {

                }
                else if (stageNum == 2)
                {
                    stage1Text.transform.position += new Vector3(10.0f, 0, 0);
                    stage1Image.transform.position += new Vector3(10.0f, 0, 0);
                    stage2Text.transform.position += new Vector3(10.0f, 0, 0);
                    stage2Image.transform.position += new Vector3(10.0f, 0, 0);
                }

                moveCount++;
            }
            else
            {
                moveCount = 0;
                stageNum--;
                moveLeft = false;
            }
        }

        if (moveRight == true)
        {
            if (moveCount <= 100)
            {
                if (stageNum == 1)
                {
                    stage1Text.transform.position -= new Vector3(10.0f, 0, 0);
                    stage1Image.transform.position -= new Vector3(10.0f, 0, 0);
                    stage2Text.transform.position -= new Vector3(10.0f, 0, 0);
                    stage2Image.transform.position -= new Vector3(10.0f, 0, 0);
                }
                else if (stageNum == 2)
                {

                }

                moveCount++;
            }
            else
            {
                moveCount = 0;
                stageNum++;
                moveRight = false;
            }
        }

        if (!moveLeft && !moveRight && stageNum == 1)
        {
            rightButton.SetActive(true);
        }
        else if (!moveLeft && !moveRight && stageNum == 2)
        {
            leftButton.SetActive(true);
        }
        else if (!moveLeft && !moveRight)
        {
            rightButton.SetActive(true);
            leftButton.SetActive(true);
        }

        /*
        if ()
        {
            StartGame();  //ゲームスタート
        }
        */

        if (left)
        {
            StageLeft();  //左側のステージに移る
            left = false;
        }

        if (right)
        {
            StageRight();  //右側のステージに移る
            right = false;
        }

        // ゲームパッドが接続されていないとnullになる。
        if (Gamepad.current == null) return;

        getPS4();
    }


    public void StartGame()
    {
        if (moveLeft == false && moveRight == false)
        {
            if (stageNum == 1)
            {
                //SceneManager.LoadScene("Stage1_Image");
            }
            else if (stageNum == 2)
            {

            }
        }
    }


    public void StageLeft()
    {
        if (moveLeft == false && moveRight == false)
        {
            if (stageNum != 1)
            {
                rightButton.SetActive(false);
                leftButton.SetActive(false);

                moveLeft = true;
            }
        }
    }


    public void StageRight()
    {
        if (moveLeft == false && moveRight == false)
        {
            if (stageNum != 2)
            {
                rightButton.SetActive(false);
                leftButton.SetActive(false);

                moveRight = true;
            }
        }
    }

    void getPS4()
    {
        // スティックの入力を受け取る
        var v = Gamepad.current.leftStick.ReadValue();

        if (v.x >= 0.75)
        {
            right = true;
        }
        else if (v.x <= -0.75)
        {
            left = true;
        }
    }
}