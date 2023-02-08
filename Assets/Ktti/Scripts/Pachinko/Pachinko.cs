using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Pachinko : MonoBehaviour
{
    public Pachinko_sensor sensor;

    public Transform point1;
    public Transform point2;

    public float power = 1f;

    Transform target;

    public float waitTime = 3f;

    [SerializeField, NonEditable]
    bool flg = false;
    bool isFirst = false;

    [SerializeField]
    float Sspeed = 10.0f;

    [SerializeField]
    float Pspeed = 3.0f;

    public Vector3 now;
    public Vector3 p1;
    public Vector3 p2;

    float speed;

    Vector3 vec;

    void Start()
    {
        target = point1;
        speed = Sspeed;
        p1 = point1.position; 
        p2 = point2.position;

        vec = p2 - p1;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !flg)
        {
            Action();
        }
        //transform.position = Vector3.MoveTowards(transform.position, target.position, speed);
        now = transform.position;

        if (flg)
        {
            if (!isFirst)
            {
                if (transform.position == point2.position)
                {
                    target = point1;
                    speed = Pspeed;
                    isFirst = true;
                    Debug.Log("p2");

                    sensor.player.GetComponent<Rigidbody>().AddForce(vec.normalized * power, ForceMode.VelocityChange);
                    sensor.player.parent = null;
                }
            }
            else
            {
                if (transform.position == point1.position)
                {
                    flg = false;
                    isFirst = false;
                    Debug.Log("p1");
                }
            }
        }
    }

    void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed);
    }

    public void Action()
    {
        target = point2;
        speed = Sspeed;
        flg = true;
    }
}
