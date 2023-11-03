using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Fadeinout : MonoBehaviour
{
    public GameObject FadePanel;  // フェードパネルの取得

    Image fadealpha;  // フェードパネルのイメージ取得変数

    private float alpha;  // パネルのalpha値取得変数

    public bool fadeout = false;  // フェードアウトのフラグ変数
    public bool fadein = false; // フェードインのフラグ変数


    void Start()
    {
        fadealpha = FadePanel.GetComponent<Image>();  // パネルのイメージ取得
        alpha = fadealpha.color.a;  // パネルのalpha値を取得
    }

    void Update()
    {
        if (fadeout == true)
        {
            FadeOut();
        }
        else if (fadein == true)
        {
            FadeIn();
        }
    }


    void FadeOut()
    {
        alpha += 0.5f;
        fadealpha.color = new Color(0, 0, 0, alpha);

        if (alpha >= 1)
        {
            fadeout = false;
            fadein = true;
        }
    }

    void FadeIn()
    {
        alpha -= 0.02f;
        fadealpha.color = new Color(0, 0, 0, alpha);

        if (alpha <= 0)
        {
            fadein = false;
        }
    }
}