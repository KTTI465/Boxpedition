using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope_Point : MonoBehaviour
{
    [SerializeField]
    bool isUp = false;

    [SerializeField,NonEditable]
    private warpRope warpRope;

    private void Start()
    {
        warpRope = transform.root.GetComponent<warpRope>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (isUp)
            {
                warpRope.SetRopeUp(true);
            }
            else
            {
                warpRope.SetRopeDown(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (isUp)
            {
                warpRope.SetRopeUp(false);
            }
            else
            {
                warpRope.SetRopeDown(false);
            }
        }
    }
}
