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
        if(triggerStay)  //範囲内にプレイヤーがいるとき
        {
            CheckPS4();

            if (ps4button)  // 何かボタンを押したら
            {
                imagePrefab.SetActive(false);  // 画像を非表示にする
                Time.timeScale = 1;

                titleSlider.SetSE(seValue);

                PlayerPrefs.SetFloat("Sensi", sensiValue);
                PlayerPrefs.Save();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))  // プレイヤーが範囲に入ったら
        {
            if (Gamepad.current != null)
            {
                imagePrefab.SetActive(true);  // 画像を表示する
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
        if (other.CompareTag("Player"))  // プレイヤーが範囲から出たら
        {
            imagePrefab.SetActive(false);  // 画像を非表示にする
            triggerStay = false;
            ps4button = false;
        }
    }

    void CheckPS4()
    {
        if (Gamepad.current.buttonNorth.isPressed || Gamepad.current.buttonEast.isPressed || 
            Gamepad.current.buttonWest.isPressed || Gamepad.current.buttonSouth.isPressed)  // 何かボタンを押したら
        {
            ps4button = true;
        }
    }
}