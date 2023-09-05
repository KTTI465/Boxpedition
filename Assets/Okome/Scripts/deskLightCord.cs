using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deskLightCord : MonoBehaviour
{
    //ロープの子要素を格納
    GameObject[] cord;
    //子要素の合計
    int cordCount;
    //子要素それぞれが何番目かを決める数字
    int cordIndex;

    LineRenderer line;
    private void Start()
    {
        cordIndex = 0;
        cordCount = transform.childCount;
        cord = new GameObject[cordCount];

        line = GetComponent<LineRenderer>();
        line.positionCount = cordCount;
        foreach (Transform i in transform)
        {
            //子オブジェクトを順番に格納して見た目を非表示にしていく
            cord[cordIndex++] = i.gameObject;
            i.GetComponent<MeshRenderer>().enabled = false;
        }

        //物理演算をとめるためのコルーチン（n秒後,）
        StartCoroutine(StopPhysics(5.0f, cord));
    }
    private void Update()
    {
        //線を表示する
        int idx = 0;
        foreach (GameObject i in cord)
        {
            line.SetPosition(idx, i.transform.position);
            idx++;
        }
    }

    IEnumerator StopPhysics(float time, GameObject[] cord)
    {
        //一定時間たったらロープの物理演算をとめる
        yield return new WaitForSecondsRealtime(time);
        foreach (GameObject i in cord)
        {
            i.GetComponent<Rigidbody>().isKinematic = true;
            i.GetComponent<Rigidbody>().useGravity = false;
        }
    }
}
