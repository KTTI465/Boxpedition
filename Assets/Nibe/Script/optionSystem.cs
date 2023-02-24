using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class optionSystem : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            ReturnTitle();  //タイトルに戻る
        }
    }

    //　タイトルへ戻るボタンを押したら実行する
    public void ReturnTitle()
    {
        SceneManager.LoadScene("titleScene");
    }
}
