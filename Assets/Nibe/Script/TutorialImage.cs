using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using static UnityEditor.PlayerSettings;

public class TutorialImage : MonoBehaviour
{
    public GameObject imagePrefab;  // 画像用プレハブ

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))  // プレイヤーが範囲に入ったら
        {
            imagePrefab.SetActive(true);  // 画像を表示する
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (Gamepad.current != null)
            {
                if (Gamepad.current.buttonEast.isPressed)  // 〇ボタンを押したら
                {
                    imagePrefab.SetActive(false);  // 画像を非表示にする
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))  // プレイヤーが範囲から出たら
        {
            imagePrefab.SetActive(false);  // 画像を非表示にする
        }
    }
}