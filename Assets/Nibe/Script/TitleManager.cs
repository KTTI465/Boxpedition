using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using Unity.VisualScripting;
using UnityEngine.UI;
using CriWare;


public class TitleManager : MonoBehaviour
{
    [SerializeField] MusicManager musicManager;
    [SerializeField] TitleSlider titleSlider;

    [SerializeField] GameObject titlePanel;
    [SerializeField] GameObject optionPanel;
    [SerializeField] GameObject stagePanel;
    [SerializeField] GameObject resultPanel;

    [SerializeField] GameObject creditPanel1;
    [SerializeField] GameObject creditPanel2;

    [SerializeField] GameObject rightImage;
    [SerializeField] GameObject leftImage;

    [SerializeField] Text startGameText;
    [SerializeField] Text openOptionText;
    [SerializeField] Text quitGameText;
    [SerializeField] Text languageText;
    [SerializeField] Text creditText;

    [SerializeField] Text bgmText;
    [SerializeField] Text seText;
    [SerializeField] Text sensiText;
    [SerializeField] Text guideText;
    [SerializeField] Text guideOnText;
    [SerializeField] Text guideOffText;
    [SerializeField] Text exitOptionText;
    [SerializeField] Text stageSelectText;
    [SerializeField] Text startStageText;

    [SerializeField] GameObject stage1Image;
    [SerializeField] GameObject stage2Image;
    [SerializeField] GameObject stage3Image;

    [SerializeField] UnityEngine.UI.Slider bgmSlider;
    [SerializeField] UnityEngine.UI.Slider seSlider;
    [SerializeField] UnityEngine.UI.Slider sensiSlider;

    [SerializeField] Button guideOnButton;
    [SerializeField] Button guideOffButton;

    [SerializeField] GameObject japaneseButton;
    [SerializeField] GameObject englishButton;
    [SerializeField] Text japaneseText;
    [SerializeField] Text englishText;

    private bool title = true;
    private bool option = false;
    private bool stage = false;
    private bool language = false;
    private bool credit = false;

    private int titleNum = 0;
    private int optionNum = 0;
    private int selectNum = 0;
    private int stageNum = 0;
    private int languageNum = 0;

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

    [SerializeField]
    private Titleplayer soundController;

    void Awake()
    {
        if (PlayerPrefs.GetString("First") != "false")
        {
            // 保存
            PlayerPrefs.SetFloat("BGM", 1.0f);
            PlayerPrefs.Save();

            PlayerPrefs.SetFloat("SE", 1.0f);
            PlayerPrefs.Save();

            PlayerPrefs.SetFloat("Sensi", 1.0f);
            PlayerPrefs.Save();

            PlayerPrefs.SetString("Language", "Japanese");
            PlayerPrefs.Save();

            PlayerPrefs.SetString("Guide", "true");
            PlayerPrefs.Save();

            PlayerPrefs.SetString("First", "false");
            PlayerPrefs.Save();
        }
    }


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
            if (Gamepad.current == null && credit)
        {
            if (Input.GetMouseButtonDown(1) || Input.GetKey(KeyCode.Escape))
            {
                ExitCreditButton();
            }
        }

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
                    PlaySelectSE();
                    titleNum--;
                    up = false;
                }
            }
            else if (down == true)
            {
                if (titleNum == 4)
                {
                    down = false;
                }
                else
                {
                    PlaySelectSE();
                    titleNum++;
                    down = false;                   
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
                else if (titleNum == 3)
                {
                    OpenLanguageButton();
                }
                else if (titleNum == 4)
                {
                    OpenCreditButton();
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
                    PlaySelectSE();
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
                    PlaySelectSE();
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
                    GuideOffButton();
                    PlaySelectSE();
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
                    GuideOnButton();
                    PlaySelectSE();
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
        else if (language)
        {
            if (up == true) //日本語
            {
                languageNum = 0;
                up = false;
                PlaySelectSE();
            }
            else if (down == true)  //英語
            {
                languageNum = 1;
                down = false;
                PlaySelectSE();
            }

            if (Gamepad.current.buttonEast.wasPressedThisFrame)
            {
                ExitLanguageButton();
            }
        }
        else if (credit)
        {
            if (Input.GetMouseButtonDown(1) || Input.GetKey(KeyCode.Escape))
            {
                ExitCreditButton();
            }

            if (Gamepad.current.buttonSouth.wasPressedThisFrame)
            {
                ExitCreditButton();
            }
        }


        // 見た目
        if (titleNum == 0)
        {
            startGameText.color = new Color(r2, g2, b2, a2);
            openOptionText.color = new Color(r1, g1, b1, a1);
            quitGameText.color = new Color(r1, g1, b1, a1);
            languageText.color = new Color(r1, g1, b1, a1);
            creditText.color = new Color(r1, g1, b1, a1);
        }
        else if (titleNum == 1)
        {
            startGameText.color = new Color(r1, g1, b1, a1);
            openOptionText.color = new Color(r2, g2, b2, a2);
            quitGameText.color = new Color(r1, g1, b1, a1);
            languageText.color = new Color(r1, g1, b1, a1);
            creditText.color = new Color(r1, g1, b1, a1);
        }
        else if (titleNum == 2)
        {
            startGameText.color = new Color(r1, g1, b1, a1);
            openOptionText.color = new Color(r1, g1, b1, a1);
            quitGameText.color = new Color(r2, g2, b2, a2);
            languageText.color = new Color(r1, g1, b1, a1);
            creditText.color = new Color(r1, g1, b1, a1);
        }
        else if (titleNum == 3)
        {
            startGameText.color = new Color(r1, g1, b1, a1);
            openOptionText.color = new Color(r1, g1, b1, a1);
            quitGameText.color = new Color(r1, g1, b1, a1);
            languageText.color = new Color(r2, g2, b2, a2);
            creditText.color = new Color(r1, g1, b1, a1);
        }
        else if (titleNum == 4)
        {
            startGameText.color = new Color(r1, g1, b1, a1);
            openOptionText.color = new Color(r1, g1, b1, a1);
            quitGameText.color = new Color(r1, g1, b1, a1);
            languageText.color = new Color(r1, g1, b1, a1);
            creditText.color = new Color(r2, g2, b2, a2);
        }

        if (optionNum == 0)
        {
            bgmText.color = new Color(r2, g2, b2, a2);
            seText.color = new Color(r1, g1, b1, a1);
            //sensiText.color = new Color(r1, g1, b1, a1);
            guideText.color = new Color(r1, g1, b1, a1);
            exitOptionText.color = new Color(r1, g1, b1, a1);
        }
        else if (optionNum == 1)
        {
            bgmText.color = new Color(r1, g1, b1, a1);
            seText.color = new Color(r2, g2, b2, a2);
            //sensiText.color = new Color(r1, g1, b1, a1);
            guideText.color = new Color(r1, g1, b1, a1);
            exitOptionText.color = new Color(r1, g1, b1, a1);
        }
        else if (optionNum == 2)
        {
            bgmText.color = new Color(r1, g1, b1, a1);
            seText.color = new Color(r1, g1, b1, a1);
            //sensiText.color = new Color(r2, g2, b2, a2);
            guideText.color = new Color(r2, g2, b2, a2);
            seText.color = new Color(r1, g1, b1, a1);
        }
        else if (optionNum == 3)
        {
            bgmText.color = new Color(r1, g1, b1, a1);
            seText.color = new Color(r1, g1, b1, a1);
            //sensiText.color = new Color(r2, g2, b2, a2);
            guideText.color = new Color(r1, g1, b1, a1);
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

        if (languageNum == 0)
        {
            japaneseText.color = new Color(r2, g2, b2, a2);
            englishText.color = new Color(r1, g1, b1, a1);
        }
        else if (languageNum == 1)
        {
            japaneseText.color = new Color(r1, g1, b1, a1);
            englishText.color = new Color(r2, g2, b2, a2);
        }

        if (optionNum == 2)
        {
            if (PlayerPrefs.GetString("Guide") == "true")
            {
                guideOnText.color = new Color(r2, g2, b2, a2);
                guideOffText.color = new Color(r1, g1, b1, a1);
            }
            else if (PlayerPrefs.GetString("Guide") == "false")
            {
                guideOnText.color = new Color(r1, g1, b1, a1);
                guideOffText.color = new Color(r2, g2, b2, a2);
            }
        }
        else
        {
            guideOnText.color = new Color(r1, g1, b1, a1);
            guideOffText.color = new Color(r1, g1, b1, a1);
        }

        if (option)
        {
            Image guideOnImage = guideOnButton.GetComponent<Image>();
            Image guideOffImage = guideOffButton.GetComponent<Image>();

            Color guideOnColor = guideOffImage.color;
            Color guideOffColor = guideOffImage.color;

            if (PlayerPrefs.GetString("Guide") == "true")
            {
                // aは透明度を表す
                guideOnColor.a = 1.0f;
                guideOffColor.a = 0.3f;
            }
            else if (PlayerPrefs.GetString("Guide") == "false")
            {
                // aは透明度を表す
                guideOnColor.a = 0.3f;
                guideOffColor.a = 1.0f;
            }

            guideOnImage.color = guideOnColor;
            guideOffImage.color = guideOffColor;
        }
    }


    public void StartGameButton()
    {
        stagePanel.SetActive(true);
        titlePanel.SetActive(false);
        stage = true;
        title = false;
    }

    public void ExitGameButton()
    {
        titlePanel.SetActive(true);
        stagePanel.SetActive(false);
        title = true;
        stage = false;
    }

    public void OpenOptionButton()
    {
        optionPanel.SetActive(true);
        titlePanel.SetActive(false);
        option = true;
        title = false;
    }

    public void ExitOptionButton()
    {
        titlePanel.SetActive(true);
        optionPanel.SetActive(false);
        title = true;
        option = false;
    }

    public void OpenLanguageButton()
    {
        japaneseButton.SetActive(true);
        englishButton.SetActive(true);
        language = true;
        title = false;
    }
    public void ExitLanguageButton()
    {
        japaneseButton.SetActive(false);
        englishButton.SetActive(false);

        language = false;
        title = true;

        if (languageNum == 0)
        {
            PlayerPrefs.SetString("Language", "Japanese");
            PlayerPrefs.Save();
        }
        else if (languageNum == 1)
        {
            PlayerPrefs.SetString("Language", "English");
            PlayerPrefs.Save();
        }
    }

    public void SaveJapaneseButton()
    {
        languageNum = 0;
        ExitLanguageButton();
    }

    public void SaveEngulishButton()
    {
        languageNum = 1;
        ExitLanguageButton();
    }

    public void GuideOnButton()
    {
        PlayerPrefs.SetString("Guide", "true");
        PlayerPrefs.Save();
    }

    public void GuideOffButton()
    {
        PlayerPrefs.SetString("Guide", "false");
        PlayerPrefs.Save();
    }

    public void OpenCreditButton()
    {
        if (PlayerPrefs.GetString("Language") == "Japanese")
        {
            creditPanel1.SetActive(true);
        }
        else
        {
            creditPanel2.SetActive(true);
        }

        titlePanel.SetActive(false);
        credit = true;
        title = false;
    }
    public void ExitCreditButton()
    {
        titlePanel.SetActive(true);

        if (PlayerPrefs.GetString("Language") == "Japanese")
        {
            creditPanel1.SetActive(false);
        }
        else
        {
            creditPanel2.SetActive(false);
        }

        title = true;
        credit = false;
    }
    public void QuitGameButton()
    {
        Application.Quit();
    }

    public void StageRightButton()
    {
        if (stageNum != 2)
        {
            stageNum++;
        }

        HideStage();
    }

    public void StageLeftButton()
    {
        if (stageNum != 0)
        {
            stageNum--;
        }

        HideStage();
    }

    public void StartStageButton()
    {
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
            SceneManager.LoadScene("Stage2");
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


    public void PlaySelectSE()
    {
        //ここに音が鳴る処理を書く

        soundController.ClickPlay();
    }
}
