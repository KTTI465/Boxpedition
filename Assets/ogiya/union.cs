using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class union : MonoBehaviour
{
    public GameObject main;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Transform Box = this.transform;
        Vector3 cube = Box.transform.position;
        Vector3 capsule = main.transform.position;
 
        float arie = Vector3.Distance(cube, capsule);

        if(arie < 3.0f && Input.GetKey(KeyCode.R))
        {
            main.transform.position = new Vector3(cube.x, cube.y + 1, cube.z);
            this.gameObject.transform.parent = main.transform;
        }
        if(Input.GetKey(KeyCode.T))
        {
            this.gameObject.transform.parent = null;
        }
    }
}
