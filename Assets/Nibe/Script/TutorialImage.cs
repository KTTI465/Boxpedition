using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using static UnityEditor.PlayerSettings;

public class TutorialImage : MonoBehaviour
{
    public GameObject imagePrefab;  // �摜�p�v���n�u

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))  // �v���C���[���͈͂ɓ�������
        {
            imagePrefab.SetActive(true);  // �摜��\������
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (Gamepad.current != null)
            {
                if (Gamepad.current.buttonEast.isPressed)  // �Z�{�^������������
                {
                    imagePrefab.SetActive(false);  // �摜���\���ɂ���
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))  // �v���C���[���͈͂���o����
        {
            imagePrefab.SetActive(false);  // �摜���\���ɂ���
        }
    }
}