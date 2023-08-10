using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using Unity.VisualScripting;
using UnityEngine.UI;


public class TitleManager : MonoBehaviour
{
    [SerializeField] MusicManager musicManager;
    [SerializeField] TitleSlider titleSlider;

    [SerializeField] GameObject titlePanel;
    [SerializeField] GameObject optionPanel;
    [SerializeField] GameObject stagePanel;
    [SerializeField] GameObject resultPanel;

    [SerializeField] GameObject rightImage;
    [SerializeField] GameObject leftImage;

    [SerializeField] Text startGameText;
    [SerializeField] Text openOptionText;
    [SerializeField] Text quitGameText;
    [SerializeField] Text bgmText;
    [SerializeField] Text seText;
    [SerializeField] Text sensiText;
    [SerializeField] Text exitOptionText;
    [SerializeField] Text stageSelectText;
    [SerializeField] Text startStageText;

    [SerializeField] GameObject stage1Image;
    [SerializeField] GameObject stage2Image;
    [SerializeField] GameObject stage3Image;

    [SerializeField] UnityEngine.UI.Slider bgmSlider;
    [SerializeField] UnityEngine.UI.Slider seSlider;
    [SerializeField] UnityEngine.UI.Slider sensiSlider;


    private bool title = true;
    private bool option = false;
    private bool stage = false;

    private int titleNum = 0;
    private int optionNum = 0;
    private int selectNum = 0;
    private int stageNum = 0;

    private bool right = false;
    private bool left = false;
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

    private int ps4Count = 25;


    void Awake()
    {
        // 保存
        PlayerPrefs.SetFloat("BGM", 1.0f);
        PlayerPrefs.Save();

        PlayerPrefs.SetFloat("SE", 1.0f);
        PlayerPrefs.Save();

        PlayerPrefs.SetFloat("Sensi", 1.0f);
        PlayerPrefs.Save();
    }


    // Start is called before the first frame update
    void Start()
    {
        musicManager.SetBGM();
        musicManager.SetSE();
        musicManager.PlayBGM1();
    }

    // Update is called once per frame
    void Update()
    {
        // ゲームパッドが接続されていないとnullになる。
        if (Gamepad.current == null) return;

        getPS4();


        if (title)
        {
            if (up == true)
            {
                if (titleNum == 0)
                {
                    up = false;
                }
                else
                {
                    titleNum--;
                    up = false;
                    musicManager.PlaySE1();
                }
            }
            else if (down == true)
            {
                if (titleNum == 2)
                {
                    down = false;
                }
                else
                {
                    titleNum++;
                    down = false;
                    musicManager.PlaySE1();
                }
            }

            if (Gamepad.current.buttonEast.wasPressedThisFrame)
            {
                if (titleNum == 0)
                {
                    StartGameButton();
                    rightImage.SetActive(true);
                }
                else if (titleNum == 1)
                {
                    OpenOptionButton();
                }
                else if (titleNum == 2)
                {
                    QuitGameButton();
                }
            }
        }
        else if (option)
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
                if (optionNum == 3)
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

            if (Gamepad.current.buttonEast.wasPressedThisFrame)
            {
                if (optionNum == 3)
                {
                    optionNum = 0;
                    ExitOptionButton();
                }
            }
            else if (Gamepad.current.buttonSouth.wasPressedThisFrame)
            {
                optionNum = 0;
                ExitOptionButton();
            }
        }
        else if (stage)
        {
            if (right == true)
            {
                StageRightButton();

                right = false;
            }
            else if (left == true)
            {
                StageLeftButton();

                left = false;
            }           

            if (Gamepad.current.buttonEast.wasPressedThisFrame)
            {
                StartStageButton();
            }
            else if (Gamepad.current.buttonSouth.wasPressedThisFrame)
            {
                stageNum = 0;
                HideStage();
                ExitGameButton();
            }
        }


        // 見た目
        if (titleNum == 0)
        {
            startGameText.color = new Color(r2, g2, b2, a2);
            openOptionText.color = new Color(r1, g1, b1, a1);        
            quitGameText.color = new Color(r1, g1, b1, a1);
        }
        else if (titleNum == 1)
        {
            startGameText.color = new Color(r1, g1, b1, a1);
            openOptionText.color = new Color(r2, g2, b2, a2);
            quitGameText.color = new Color(r1, g1, b1, a1);
        }
        else if (titleNum == 2)
        {
            startGameText.color = new Color(r1, g1, b1, a1);
            openOptionText.color = new Color(r1, g1, b1, a1);
            quitGameText.color = new Color(r2, g2, b2, a2);
        }
        
        if (optionNum == 0)
        {
            bgmText.color = new Color(r2, g2, b2, a2);
            seText.color = new Color(r1, g1, b1, a1);
            sensiText.color = new Color(r1, g1, b1, a1);
            exitOptionText.color = new Color(r1, g1, b1, a1);
        }
        else if (optionNum == 1)
        {
            bgmText.color = new Color(r1, g1, b1, a1);
            seText.color = new Color(r2, g2, b2, a2);
            sensiText.color = new Color(r1, g1, b1, a1);
            exitOptionText.color = new Color(r1, g1, b1, a1);
        }
        else if (optionNum == 2)
        {
            bgmText.color = new Color(r1, g1, b1, a1);
            seText.color = new Color(r1, g1, b1, a1);
            sensiText.color = new Color(r2, g2, b2, a2);
            exitOptionText.color = new Color(r1, g1, b1, a1);
        }
        else if (optionNum == 3)
        {
            bgmText.color = new Color(r1, g1, b1, a1);
            seText.color = new Color(r1, g1, b1, a1);
            sensiText.color = new Color(r1, g1, b1, a1);
            exitOptionText.color = new Color(r2, g2, b2, a2);
        }

        if (selectNum == 0)
        {
            stageSelectText.color = new Color(r2, g2, b2, a2);
            startStageText.color = new Color(r1, g1, b1, a1);
        }
        else if (selectNum == 1)
        {
            stageSelectText.color = new Color(r1, g1, b1, a1);
            startStageText.color = new Color(r2, g2, b2, a2);
        }
    }


    public void StartGameButton()
    {
        stagePanel.SetActive(true);
        titlePanel.SetActive(false);
        stage = true;
        title = false;
        musicManager.PlaySE1();
    }

    public void ExitGameButton()
    {
        titlePanel.SetActive(true);
        stagePanel.SetActive(false);
        title = true;
        stage = false;
        musicManager.PlaySE1();
    }

    public void OpenOptionButton()
    {
        optionPanel.SetActive(true);
        titlePanel.SetActive(false);
        option = true;
        title = false;
        musicManager.PlaySE1();
    }

    public void ExitOptionButton()
    {
        titlePanel.SetActive(true);
        optionPanel.SetActive(false);
        title = true;
        option = false;
        musicManager.PlaySE1();
    }

    public void QuitGameButton()
    {
        musicManager.PlaySE1();
        Application.Quit();
    }

    public void StageRightButton()
    {
        if (stageNum != 2)
        {
            stageNum++;
            musicManager.PlaySE1();
        }

        HideStage();
    }

    public void StageLeftButton()
    {
        if (stageNum != 0)
        {
            stageNum--;
            musicManager.PlaySE1();
        }

        HideStage();
    }

    public void StartStageButton()
    {
        musicManager.PlaySE1();

        if (stageNum == 0)
        {
            SceneManager.LoadScene("Tutorial");
        }
        else if (stageNum == 1)
        {
            SceneManager.LoadScene("Stage1_New");
        }
        else if (stageNum == 2)
        {
            SceneManager.LoadScene("titleScene");
        }
    }

    void HideStage()
    {
        if (stageNum == 0)
        {
            stageSelectText.text = "Tutorial";

            stage1Image.SetActive(true);
            stage2Image.SetActive(false);

            leftImage.SetActive(false);
        }
        else if (stageNum == 1)
        {
            stageSelectText.text = "Stage1";

            stage2Image.SetActive(true);
            stage1Image.SetActive(false);
            stage3Image.SetActive(false);

            rightImage.SetActive(true);
            leftImage.SetActive(true);
        }
        else if (stageNum == 2)
        {
            stageSelectText.text = "Stage2";

            stage3Image.SetActive(true);
            stage2Image.SetActive(false);

            rightImage.SetActive(false);
        }
    }

    void getPS4()
    {
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
