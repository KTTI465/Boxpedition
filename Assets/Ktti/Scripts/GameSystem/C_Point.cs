using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_Point : MonoBehaviour
{
    [SerializeField]
    bool once = true;

    bool isFirst = false;

    public ResPoint resPoint;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            if (!isFirst)
            {
                if (CheckPointManager.Get())
                {
                    CheckPointManager.Get().SetPoint(resPoint.GetID());
                    Debug.Log($"Check Point:{resPoint.GetID()} SET");
                }
            }

            if (once)
            {
                isFirst = true;
            }
        }
    }

    public void SetCheckPoint()
    {
        if (CheckPointManager.Get())
        {
            CheckPointManager.Get().SetPoint(resPoint.GetID());
            Debug.Log($"Check Point:{resPoint.GetID()} SET");
        }
    }
}
