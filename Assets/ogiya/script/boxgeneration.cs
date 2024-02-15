using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boxgeneration : MonoBehaviour
{
    public GameObject main;
    public GameObject cube;
    private bool Cubegeneration = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Transform floor = this.transform;

        // ì¡íËÇÃè∞Ç…êGÇÍÇΩéû
        Vector3 gimmickfloor = floor.transform.position;
        Vector3 Main = main.transform.position;
        Vector3 cubepos = new Vector3(Main.x, Main.y, Main.z);
        Vector3 mainpos = new Vector3(Main.x, Main.y + 1, Main.z);
        float arie = Vector3.Distance(gimmickfloor, Main);

        if (arie < 1.0f && Cubegeneration)
        {
            Cubegeneration = false;
            Instantiate(cube);
            main.transform.position = mainpos;
            cube.transform.position = cubepos;
            cube.gameObject.transform.parent = main.transform;

        }
        if (Input.GetKey(KeyCode.T))
        {
            cube.gameObject.transform.parent = null;
        }
    }
}
