using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slingshot : MonoBehaviour
{
    //スクリプトから設定できるターゲットのポジション
    public int targetX;
    public int targetY;
    public int targetZ;

    //発射速度
    public float speed = 100;
    [SerializeField] private Transform obj;
    [SerializeField] private Transform sling;
    [Range(0.0f, 180.0f)] public float arcAngle = 60.0f;
    float Travel;
    bool Throw = false;

    Vector3 pivot;
    Vector3 FromVector;
    Vector3 ToVector;

    private bool IsSet = false;
    private bool IsPosition = false;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 slingpos = this.transform.position;
        Vector3 objpos = obj.transform.position;
        Vector3 targetpos = new Vector3(targetX, targetY, targetZ);
        float arie = Vector3.Distance(objpos, slingpos);

        var targethalfAngle = Mathf.Tan(Mathf.Deg2Rad * arcAngle * 0.5f);
        var midPos = (objpos + targetpos) * 0.5f;
        var half = Vector3.Distance(objpos, midPos);

        pivot = midPos;
        pivot.y -= half / targethalfAngle;

        //中心から出発地、中心から目的地へのベクトルを求めておく
        FromVector = objpos - pivot;
        ToVector = targetpos - pivot;

        //移動量を0.0にリセットしておく
        Travel = 0.0f;

        if (arie <= 5f && IsSet == false)
        {
            IsSet = true;
            IsPosition = true;

            sling.LookAt(targetpos);
        }
        if (IsPosition == true)
        {
            //　セットするオブジェクトの位置を指定
            obj.transform.parent = sling.transform;
            obj.transform.position = new Vector3(slingpos.x, slingpos.y + 3, slingpos.z);
        }
        // 特定キーで発射
        if (Input.GetKey(KeyCode.R) && IsSet == true)
        {
            Throw = true;
        }
        if (Throw == true)
        {
            IsPosition = false;
            obj.gameObject.transform.parent = null;
            Travel += speed * Time.deltaTime;

            //円弧の長さで割って、円弧上を進行した割合を求める
            var t = Travel / (FromVector.magnitude * Mathf.Deg2Rad * arcAngle);

            if (t < 1.0f)
            {
                //FromVectorとToVectorを進行割合で補間し、Pivotを足して現在の位置とする
                obj.transform.position = Vector3.Slerp(FromVector, ToVector, t) + pivot;
            }
            else
            {
                //tが1.0に到達したら移動終了とする
                obj.transform.position = ToVector + pivot;
                Throw = false;
            }
        }
    }
}
