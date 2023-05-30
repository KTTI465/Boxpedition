using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{
    [SerializeField]
    Fadeinout fadeinout;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CheckPointManager.Get().Respawn();
            fadeinout.fadeout = true;  //‰æ–Ê‚ªˆÃ“]‚·‚é
        }
    }
}
