using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slingshot : MonoBehaviour
{
    public int targetX;
    public int targetY;
    public int targetZ;
    public int addforce;
    [SerializeField] private Transform obj;
    [SerializeField] private Transform sling;

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
        Vector3 force = new Vector3(slingpos.x, slingpos.y, slingpos.z);
        Vector3 forward = Vector3.forward;
        float arie = Vector3.Distance(objpos, slingpos);


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
            obj.transform.position = new Vector3(slingpos.x, slingpos.y + 1, slingpos.z);
        }
        // 特定キーで発射
        if (Input.GetKeyDown(KeyCode.R) && IsSet == true)
        {
            IsPosition = false;
            obj.gameObject.transform.parent = null;
            obj.GetComponent<Rigidbody>().AddForce(transform.forward * addforce * 50f, ForceMode.Force);
        }
    }
}
