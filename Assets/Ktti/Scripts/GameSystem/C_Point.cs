using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_Point : MonoBehaviour
{
    [SerializeField]
    public int index_p;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            if (CheckPointManager.Get())
            {
                CheckPointManager.Get().SetPoint(index_p);
                Debug.Log($"Check Point:{index_p} SET");
            }
        }
    }
}
