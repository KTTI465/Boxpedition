using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    public IEnumerator DestroyBox()
    {
        yield return new WaitForSeconds(1);

        Destroy(gameObject);
    }
}
