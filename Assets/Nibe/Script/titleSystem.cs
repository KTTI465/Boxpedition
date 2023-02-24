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
            StartGame();  //�Q�[���X�^�[�g
        }

        if (Input.GetKey(KeyCode.O))
        {
            OpenOption();  //�I�v�V�������J��
        }

        if (Input.GetKey(KeyCode.Escape))
        {
            QuitGame();  //�Q�[���I��
        }
    }

    //�X�^�[�g�{�^��������������s����
    public void StartGame()
    {
        SceneManager.LoadScene("Stage1_Image");
    }

    //�I�v�V�����{�^��������������s����
    public void OpenOption()
    {
        SceneManager.LoadScene("optionScene");
    }

    //�Q�[���I���{�^��������������s����
    public void QuitGame()
    {
        Application.Quit();
    }
}
