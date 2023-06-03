using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Fadeinout : MonoBehaviour
{
    public GameObject FadePanel;   //�t�F�[�h�p�l���̎擾

    Image fadealpha;               //�t�F�[�h�p�l���̃C���[�W�擾�ϐ�

    private float alpha;           //�p�l����alpha�l�擾�ϐ�

    public bool fadeout = false;          //�t�F�[�h�A�E�g�̃t���O�ϐ�
    public bool fadein = false;          //�t�F�[�h�C���̃t���O�ϐ�


    // Use this for initialization
    void Start()
    {
        fadealpha = FadePanel.GetComponent<Image>(); //�p�l���̃C���[�W�擾
        alpha = fadealpha.color.a;                 //�p�l����alpha�l���擾
    }

    // Update is called once per frame
    void Update()
    {
        if (fadeout == true)
        {
            FadeOut();
        }

        if (fadein == true)
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