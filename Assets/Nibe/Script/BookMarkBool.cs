using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookMarkBool : MonoBehaviour
{
    public pullBookmark pullBookMark;

    //�^�񒆂�͂�ł��邩�ǂ����̔���t���O
    public bool grabMiddle;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (this.gameObject.name == ("markMiddle") || pullBookMark.grabStart == false)
            {
                grabMiddle = true;
            }
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (this.gameObject.name == ("markMiddle") || pullBookMark.grabStart == false)
            {
                grabMiddle = true;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (this.gameObject.name == ("markMiddle"))
        {
            grabMiddle = false;
        }
    }
}
