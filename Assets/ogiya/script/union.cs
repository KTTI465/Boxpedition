using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;
using UnityEngine.InputSystem;

public class union : MonoBehaviour
{
    public GameObject main;
    //PS4コントローラーの設定
    public bool ps40 = false;

    // Update is called once per frame
    void Update()
    {
        Transform Box = this.transform;
        Vector3 cube = Box.transform.position;
        Vector3 capsule = main.transform.position;


 
        float arie = Vector3.Distance(cube, capsule);

        if(arie < 3.0f && ps40 == true)
        {
            main.transform.position = new Vector3(cube.x, cube.y + 1, cube.z);
            this.gameObject.transform.parent = main.transform;
        }

    }

    void GetPS40()
    {
        //コントローラーのボタンを認識
        if (Gamepad.current != null)
        {
            if (Gamepad.current.buttonEast.isPressed)
            {
                //〇ボタンを押した時
                ps40 = true;
            }
            else
            {
                //そうでなければ判定なし
                ps40 = false;
            }
        }
    }
}
