using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class dates : MonoBehaviour
{
    [SerializeField]
    public AudioMixer audioMixer;

    public static float bgmVolume;
    public static float seVolume;

    public static dates instance;//static 変数でインスタンスを保持
    void Awake()
    {
        //初回のAwakeの時のみここがtrueになりインスタンスが登録される
        if (instance == null)
        {
            instance = this;//このインスタンスをstatic な instanceに登録
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);//２回目以降重複して作成してしまったgameObjectを削除
        }
    }
    public void SetBGM(float volume)
    {
        audioMixer.SetFloat("BGMVol", volume);
        bgmVolume = volume;
    }

    public void SetSE(float volume)
    {
        audioMixer.SetFloat("SEVol", volume);
        seVolume = volume;
    }
}
