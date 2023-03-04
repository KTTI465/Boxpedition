using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class titleSystem : MonoBehaviour
{
    [SerializeField] AudioMixer audioMixer;

    public AudioClip titleSE;
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

        if (Input.GetKey(KeyCode.S))
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
        audioSource.PlayOneShot(titleSE);
        SceneManager.LoadScene("Stage1_Image");
    }

    //オプションボタンを押したら実行する
    public void OpenOption()
    {
        audioSource.PlayOneShot(titleSE);
        SceneManager.LoadScene("optionScene");
    }

    //ゲーム終了ボタンを押したら実行する
    public void QuitGame()
    {
        audioSource.PlayOneShot(titleSE);
        Application.Quit();
    }
}
