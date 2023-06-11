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
            fadeinout.fadeout = true;  //��ʂ��Ó]����
            CheckPointManager.Get().Respawn();
        }

        if (other.CompareTag("ResTarget"))
        {
            var ID = other.transform.GetInstanceID();
            CheckPointManager.Get().ObRespawn(ID);
        }
    }
}
