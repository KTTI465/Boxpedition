using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using static UnityEditor.PlayerSettings;

public class TutorialImage : MonoBehaviour
{
    [SerializeField] TitleSlider titleSlider;

    public GameObject imagePrefab;
    bool ps4button = false;
    bool triggerStay = false;
    float seValue = 0.0f;
    float sensiValue = 0.0f;


    void Update()
    {
        if(triggerStay)  //�͈͓��Ƀv���C���[������Ƃ�
        {
            CheckPS4();

            if (ps4button)  // �����{�^������������
            {
                imagePrefab.SetActive(false);  // �摜���\���ɂ���
                Time.timeScale = 1;

                titleSlider.SetSE(seValue);

                PlayerPrefs.SetFloat("Sensi", sensiValue);
                PlayerPrefs.Save();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))  // �v���C���[���͈͂ɓ�������
        {
            if (Gamepad.current != null)
            {
                imagePrefab.SetActive(true);  // �摜��\������
                triggerStay = true;
                Time.timeScale = 0;


                seValue = PlayerPrefs.GetFloat("SE");
                titleSlider.SetSE(0.0f);

                sensiValue = PlayerPrefs.GetFloat("Sensi");
                PlayerPrefs.SetFloat("Sensi", 0.0f);
                PlayerPrefs.Save();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))  // �v���C���[���͈͂���o����
        {
            imagePrefab.SetActive(false);  // �摜���\���ɂ���
            triggerStay = false;
            ps4button = false;
        }
    }

    void CheckPS4()
    {
        if (Gamepad.current.buttonNorth.isPressed || Gamepad.current.buttonEast.isPressed || 
            Gamepad.current.buttonWest.isPressed || Gamepad.current.buttonSouth.isPressed)  // �����{�^������������
        {
            ps4button = true;
        }
    }
}