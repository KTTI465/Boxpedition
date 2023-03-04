using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class optionSystem : MonoBehaviour
{
    [SerializeField] AudioMixer audioMixer;

    public AudioClip optionSE;
    AudioSource audioSource;

    public bool DontDestroyEnabled = true;


    // Start is called before the first frame update
    void Start()
    {
        // Componentを取得
        audioSource = GetComponent<AudioSource>();

        if (DontDestroyEnabled)
        {
            // Sceneを遷移してもオブジェクトが消えないようにする
            DontDestroyOnLoad(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //audioMixerに代入
        audioMixer.SetFloat("SE", PlayerPrefs.GetFloat("SE"));

        if (Input.GetKey(KeyCode.Escape))
        {
            ReturnTitle();  //タイトルに戻る
        }
    }

    //　タイトルへ戻るボタンを押したら実行する
    public void ReturnTitle()
    {
        audioSource.PlayOneShot(optionSE);
        SceneManager.LoadScene("titleScene");
    }
}
