using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pachinko : MonoBehaviour
{
    Rigidbody rb;

    public int waitTime;

    [SerializeField, NonEditable]
    bool flg = false;

    bool isFirst = false;

    void Start()
    {
        rb= GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isFirst && flg)
        {
            Action();
            isFirst = false;
        }


    }

    public void Action()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine("CoolTime");
    }

    private void OnTriggerExit(Collider other)
    {
        flg = false;
        isFirst = false;
        StopCoroutine("CoolTime");
    }

    IEnumerable CoolTime()
    {
        flg = true;

        yield return new WaitForSeconds(waitTime);

        flg = false;
    }
}
