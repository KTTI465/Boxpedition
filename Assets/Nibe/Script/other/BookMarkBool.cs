using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookMarkBool : MonoBehaviour
{
    public pullBookmark pullBookMark;

    // ê^ÇÒíÜÇíÕÇÒÇ≈Ç¢ÇÈÇ©Ç«Ç§Ç©ÇÃîªíËÉtÉâÉO
    public bool grabMiddle;

    void Start()
    {
        
    }

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
