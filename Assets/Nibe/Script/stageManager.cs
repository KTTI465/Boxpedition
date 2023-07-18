using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using Unity.VisualScripting;
using UnityEngine.UI;

public class stageManager : MonoBehaviour
{
    [SerializeField] MusicManager musicManager;
    [SerializeField] TitleSlider titleSlider;

    [SerializeField] GameObject optionPanel;
    [SerializeField] GameObject TitleButton;

    [SerializeField] Text bgmText;
    [SerializeField] Text seText;
    [SerializeField] Text sensiText;

    [SerializeField] UnityEngine.UI.Slider sensiSlider;

    public bool openOption = false;
    private int optionNum = 0;
    private int ps4Count = 25;

    private float r1 = 1.0f;
    private float g1 = 1.0f;
    private float b1 = 1.0f;
    private float a1 = 1.0f;
    private float r2 = 1.0f;
    private float g2 = 1.0f;
    private float b2 = 0.0f;
    private float a2 = 1.0f;

    private bool right = false;
    private bool left = false;
    private bool up = false;
    private bool down = false;

    // Start is called before the first frame update
    void Start()
    {
        musicManager.SetBGM();
        musicManager.SetSE();
        musicManager.PlayBGM2();
    }

    // Update is called once per frame
    void Update()
    {
        // ゲームパッドが接続されていないとnullになる。
        if (Gamepad.current == null) return;

        getPS4();

        if (openOption)
        {
            if (up == true)
            {
                if (optionNum == 0)
                {
                    up = false;
                }
                else
                {
                    optionNum--;
                    up = false;
                    musicManager.PlaySE1();
                }
            }
            else if (down == true)
            {
                if (optionNum == 2)
                {
                    down = false;
                }
                else
                {
                    optionNum++;
                    down = false;
                    musicManager.PlaySE1();
                }
            }

            if (right == true)
            {
                if (optionNum == 0)
                {
                    titleSlider.upBGM();
                }
                else if (optionNum == 1)
                {
                    titleSlider.upSE();
                }
                else if (optionNum == 2)
                {
                    titleSlider.upSensi();
                }

                right = false;
            }
            else if (left == true)
            {
                if (optionNum == 0)
                {
                    titleSlider.downBGM();
                }
                else if (optionNum == 1)
                {
                    titleSlider.downSE();
                }
                else if (optionNum == 2)
                {
                    titleSlider.downSensi();
                }

                left = false;
            }
        }
        else
        {
            optionNum = 0;
        }


        // 見た目
        if (optionNum == 0)
        {
            bgmText.color = new Color(r2, g2, b2, a2);
            seText.color = new Color(r1, g1, b1, a1);
            sensiText.color = new Color(r1, g1, b1, a1);
        }
        else if (optionNum == 1)
        {
            bgmText.color = new Color(r1, g1, b1, a1);
            seText.color = new Color(r2, g2, b2, a2);
            sensiText.color = new Color(r1, g1, b1, a1);
        }
        else if (optionNum == 2)
        {
            bgmText.color = new Color(r1, g1, b1, a1);
            seText.color = new Color(r1, g1, b1, a1);
            sensiText.color = new Color(r2, g2, b2, a2);
        }
    }

    public void OptionButton()
    {
        if (openOption)
        {
            optionPanel.SetActive(false);
            TitleButton.SetActive(false);
            openOption = false;
            Time.timeScale = 1;

            titleSlider.SetSensi(sensiSlider.value);
        }
        else
        {
            titleSlider.SetSensi(0);

            optionPanel.SetActive(true);
            TitleButton.SetActive(true);
            openOption = true;
            Time.timeScale = 0;         
        }

        musicManager.PlaySE1();
    }

    void getPS4()
    {
        if (Gamepad.current.startButton.wasPressedThisFrame)
        {
            OptionButton();
        }

        // スティックの入力を受け取る
        var v = Gamepad.current.leftStick.ReadValue();

        if (ps4Count >= 25)
        {
            if (v.x >= 0.75)
            {
                right = true;
                ps4Count = 0;
            }
            else if (v.x <= -0.75)
            {
                left = true;
                ps4Count = 0;
            }
            else if (v.y >= 0.75)
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
