using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Set_Point : MonoBehaviour
{
    //加減算するスコア
    public int point;
    //一度だけ実行するか否か
    public bool once = false;
    //一度だけ実行されたかの制限用フラグ
    bool isFirst = false;

    void OnTriggerEnter()
    {
        //onceがtrueの時、まだ実行されてないか否か
        if (!isFirst)
        {
            //ポイントマネージャーを呼び出す
            if (PointManager.Get())
            {
                //ポイントマネージャーのAddPoint関数でポイントを加減算
                PointManager.Get().AddPoint(point);
                Debug.Log(PointManager.Get().GetPoint());
            }
        }
        if (once)
        {
            //onceがtrueの時、一度実行されたらフラグを変更
            isFirst = true;
        }
    }
}
