using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{
    public List<GameObject> vertices;
    LineRenderer line;

    void Awake()
    {
        line = GetComponent<LineRenderer>();
        line.material = new Material(Shader.Find("Unlit/Color"));

        SetLine(vertices);
    }

    void Update()
    {
        int idx = 0;
        foreach (GameObject v in vertices)
        {
            line.SetPosition(idx, v.transform.position);
            idx++;
        }
    }

    void SetLine(List<GameObject> objects)
    {
        line.positionCount = objects.Count;

        foreach (GameObject v in objects)
        {
            v.GetComponent<MeshRenderer>().enabled = false;
        }
    }
}
