using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Fan : MonoBehaviour
{
    [SerializeField]
    public GameObject player;
    public GameObject fan;
    public GameObject button;

    public bool FanAction　= false;
    public bool OnOff = true;
    public bool ps40 = false;


    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        Vector3 FanA = fan.transform.position;
        Vector3 P = player.transform.position;
        Vector3 Off = button.transform.position;
        Vector3 On = new Vector3(Off.x,Off.y,Off.z + 0.3f);

        float arie = Vector3.Distance(FanA, P);

        //スイッチのオンオフ判定
        if (arie < 10.0f && ps40 == true)
        {
            FanAction = true;
        }
        if (FanAction == true && OnOff == true)
        {
            //スイッチがオンの時にスイッチの位置を移動
            button.transform.position = new Vector3(On.x,On.y,On.z);
            OnOff = false;
        }
        if(FanAction == false)
        {
            button.transform.position = new Vector3(Off.x, Off.y, Off.z);
        }


    }
    void OnTriggerStay(Collider other)
    {
        //吹き飛ばし判定
        if (other.CompareTag("Player") && FanAction == true)
        {
            //プレイヤーの位置を後ろへ更新し続ける
            Vector3 newpos = player.transform.position;
            player.transform.position = new Vector3(newpos.x, newpos.y + 0.1f, newpos.z - 1.0f);
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
