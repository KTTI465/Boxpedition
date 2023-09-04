using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class titleball : MonoBehaviour
{
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        var movement = new Vector3(1f, 0, 0);
        rb.AddForce(movement * 1f);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "floor")
        {
            Invoke("restart", 8f);
        }
    }

    public void restart()
    {
        this.transform.position = new Vector3(-7, 4, -8);
    }
}