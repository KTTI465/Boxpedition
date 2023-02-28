using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class optionScreen : MonoBehaviour
{
    //�J�[�\���̕\�������邩�ǂ���
    public bool cursorLock;
    //�I�v�V�����̃p�l�����i�[
    public GameObject OptionPanel;
    //�I�v�V������ʂ�\�����Ă��邩�ǂ����̔���
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
        //Esc�L�[�ŃI�v�V�������J��
        if (cursorLock && Input.GetKeyDown(KeyCode.Escape))
        {
            OpenOption();
        }
        //�I�v�V�������J���Ă���Ƃ���Esc�L�[�ŃI�v�V���������
        else if (!cursorLock && Input.GetKeyDown(KeyCode.Escape))
        {
            OptionScreen = false;
            cursorLock = true;
        }

       
        if (cursorLock)
        {
            //�J�[�\����\�������Ȃ�
            Cursor.lockState = CursorLockMode.Locked;
        }
        else if (!cursorLock)
        {
            //�J�[�\����\��������
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
