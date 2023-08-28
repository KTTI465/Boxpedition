using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using Unity.VisualScripting;
using UnityEngine.UI;

public class GoalManager : MonoBehaviour
{
    [SerializeField] Text stage2Text;
    [SerializeField] Text titleText;

    private int buttonNum = 0;
    private int ps4Count = 25;
    private bool up = false;
    private bool down = false;

    private float r1 = 1.0f;
    private float g1 = 1.0f;
    private float b1 = 1.0f;
    private float a1 = 1.0f;
    private float r2 = 1.0f;
    private float g2 = 1.0f;
    private float b2 = 0.0f;
    private float a2 = 1.0f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // ゲームパッドが接続されていないとnullになる。
        if (Gamepad.current == null) return;

        getPS4();


        if (up == true)
        {
            if (buttonNum == 0) //ステージ2
            {
                up = false;
            }
            else
            {
                buttonNum--;
                up = false;
            }
        }
        else if (down == true)
        {
            if (buttonNum == 1) //タイトル
            {
                down = false;
            }
            else
            {
                buttonNum++;
                down = false;
            }
        }


        // 見た目
        if (buttonNum == 0)
        {
            stage2Text.color = new Color(r2, g2, b2, a2);
            titleText.color = new Color(r1, g1, b1, a1);
        }
        else if (buttonNum == 1)
        {
            stage2Text.color = new Color(r1, g1, b1, a1);
            titleText.color = new Color(r2, g2, b2, a2);
        }


        if (Gamepad.current.buttonEast.wasPressedThisFrame)
        {
            if (buttonNum == 0)
            {
                Stage2Button();
            }
            else if (buttonNum == 1)
            {
                TitleButton();
            }
        }
    }


    public void Stage2Button()
    {
        SceneManager.LoadScene("Stage2");
    }

    public void TitleButton()
    {
        SceneManager.LoadScene("titleScene");
    }

    void getPS4()
    {
        // スティックの入力を受け取る
        var v = Gamepad.current.leftStick.ReadValue();

        if (ps4Count >= 25)
        {
            if (v.y >= 0.75)
            {
                up = true;
                ps4Count = 0;
            }
            else if (v.y <= -0.75)
            {
                down = true;
                ps4Count = 0;
            }
        }

        ps4Count++;
    }
}
