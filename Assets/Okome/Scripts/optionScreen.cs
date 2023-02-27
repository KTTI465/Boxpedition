using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class optionScreen : MonoBehaviour
{
    //カーソルの表示をするかどうか
    public bool cursorLock;
    //オプションのパネルを格納
    public GameObject OptionPanel;
    //オプション画面を表示しているかどうかの判定
    public bool OptionScreen;

    private void Start()
    {
        cursorLock = true;
        OptionScreen = false;
    }
    private void Update()
    {
        OptionAndCursor();

        if (OptionScreen == true)
        {
            OptionPanel.SetActive(true);
        }
        else
        {
            OptionPanel.SetActive(false);
        }
    }

    private void OptionAndCursor()
    {
        //Escキーでオプションを開く
        if (cursorLock && Input.GetKeyDown(KeyCode.Escape))
        {
            OpenOption();
        }
        //オプションを開いているときにEscキーでオプションを閉じる
        else if (!cursorLock && Input.GetKeyDown(KeyCode.Escape))
        {
            OptionScreen = false;
            cursorLock = true;
        }

       
        if (cursorLock)
        {
            //カーソルを表示させない
            Cursor.lockState = CursorLockMode.Locked;
        }
        else if (!cursorLock)
        {
            //カーソルを表示させる
            Cursor.lockState = CursorLockMode.None;
        }
    }

    public void OpenOption()
    {
        OptionScreen = true;
        cursorLock = false;
    }

    public void CloseOption()
    {
        OptionScreen = false;
        cursorLock = true;
    }
}
