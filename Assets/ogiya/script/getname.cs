using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class getname : MonoBehaviour
{
    public string obj1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        obj1 = collision.gameObject.name;
        Debug.Log(obj1);
    }
}
