using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class titleSystem : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.S))
        {
            StartGame();  //ゲームスタート
        }

        if (Input.GetKey(KeyCode.O))
        {
            OpenOption();  //オプションを開く
        }

        if (Input.GetKey(KeyCode.Escape))
        {
            QuitGame();  //ゲーム終了
        }
    }

    //スタートボタンを押したら実行する
    public void StartGame()
    {
        SceneManager.LoadScene("Stage1_Image");
    }

    //オプションボタンを押したら実行する
    public void OpenOption()
    {
        SceneManager.LoadScene("optionScene");
    }

    //ゲーム終了ボタンを押したら実行する
    public void QuitGame()
    {
        Application.Quit();
    }
}
