using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fan : MonoBehaviour
{
    public GameObject main;
    public GameObject fan;
    public GameObject botan;

    private bool FanActionÅ@= false;
    private bool OnOff = true;


    // Start is called before the first frame update
    void Start()
    {

}



    // Update is called once per frame
    void Update()
    {
        Vector3 FanA = fan.transform.position;
        Vector3 player = main.transform.position;
        Vector3 Off = botan.transform.position;
        Vector3 On = new Vector3(Off.x,Off.y,Off.z + 0.3f);

        float arie = Vector3.Distance(FanA, player);

        if (arie < 10.0f && Input.GetKey(KeyCode.R))
        {
            FanAction = true;
        }
        if(FanAction == true && OnOff == true)
        {
            botan.transform.position = new Vector3(On.x,On.y,On.z);
            OnOff = false;
        }
        if(FanAction == false)
        {
            botan.transform.position = new Vector3(Off.x, Off.y, Off.z);
        }


    }
    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && FanAction == true)
        {
            Vector3 capsule = main.transform.position;
            main.transform.position = new Vector3(capsule.x, capsule.y + 0.1f, capsule.z - 1.0f);
        }
    }

}
