using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Keito : MonoBehaviour
{
    public GameObject jointBody;

    public GameObject startJoint;
    public float interval;

    List<GameObject> joints;

    Vector3 tmpTransform;

    public Rope ropeBody;

    Rigidbody t_Rigidbody;

    Transform ropeTrans;

    public Vector3 tensor;

    void Start()
    {
        joints = new List<GameObject>
        {
            startJoint
        };

        t_Rigidbody = GetComponent<Rigidbody>();

        joints[0].GetComponent<HingeJoint>().connectedBody = t_Rigidbody;
        ropeTrans = ropeBody.transform;

        tmpTransform = transform.position;
    }

    void Update()
    {
        if(Vector3.Distance(transform.position, tmpTransform) > interval)
        {
            tmpTransform = transform.position;

            ConnectJoint();
        }
    }

    void ConnectJoint()
    {
        GameObject lastJoint = Instantiate(jointBody, transform.position, Quaternion.Euler(Vector3.zero));

        lastJoint.GetComponent<Transform>().parent = ropeTrans;

        lastJoint.GetComponent<HingeJoint>().connectedBody = t_Rigidbody;
        joints[joints.Count - 1].GetComponent<HingeJoint>().connectedBody = lastJoint.GetComponent<Rigidbody>();

        //joints[joints.Count - 1].GetComponent<Rigidbody>().isKinematic = true;
        //lastJoint.GetComponent<Rigidbody>().isKinematic = true;


        lastJoint.GetComponent<Rigidbody>().inertiaTensor = tensor;
        joints.Add(lastJoint);

        ropeBody.SetLine(joints);
    }

    public void KeitoKinematic()
    {
        foreach (var j in joints)
        {
            j.GetComponent<Rigidbody>().isKinematic = true;
        }
    }
    public void KeitoKinematicFalse()
    {
        int i = 0;
        foreach (var j in joints)
        {
            i++;
            if (i >= 2 && i != joints.Count-1)
            {
                j.GetComponent<Rigidbody>().isKinematic = false;
            }
        }
    }

    public void SetTensor()
    {
        foreach (var j in joints)
        {
            j.GetComponent<Rigidbody>().inertiaTensor = tensor;
        }
    }

    public void EndAnimation()
    {

        //KeitoKinematic();
    }


}

#if UNITY_EDITOR
[CustomEditor(typeof(Keito))]
public class KeitoEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        Keito t = target as Keito;

        if (GUILayout.Button("All is kinematic"))
        {
            t.KeitoKinematic();
        }
        if (GUILayout.Button("All is not kinematic"))
        {
            t.KeitoKinematicFalse();
        }
        if (GUILayout.Button("SetTensor"))
        {
            t.SetTensor();
        }
    }
}
#endif
