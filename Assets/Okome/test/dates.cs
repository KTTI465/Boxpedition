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

    public static dates instance;//static �ϐ��ŃC���X�^���X��ێ�
    void Awake()
    {
        //�����Awake�̎��݂̂�����true�ɂȂ�C���X�^���X���o�^�����
        if (instance == null)
        {
            instance = this;//���̃C���X�^���X��static �� instance�ɓo�^
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);//�Q��ڈȍ~�d�����č쐬���Ă��܂���gameObject���폜
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
