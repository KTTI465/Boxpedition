using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pachinko_sensor : MonoBehaviour
{
    public Pachinko pachinko;

    public Transform player;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine("CoolTime");

            player = other.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StopCoroutine("CoolTime");
            player = null;
        }
    }

    IEnumerator CoolTime()
    {
        Debug.Log("col");
        yield return new WaitForSeconds(pachinko.waitTime);
        Debug.Log("col");
        player.parent = transform;
        pachinko.Action();
    }
}
