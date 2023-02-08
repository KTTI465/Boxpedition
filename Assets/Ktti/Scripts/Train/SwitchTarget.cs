using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class SwitchTarget : MonoBehaviour
{
    public TrainTarget branch;

    public Transform reverTransform;

    void Update()
    {
        if (UnityEngine.Input.GetKeyDown(KeyCode.E))
        {
            SwitchBranch();
        }
    }

    void SwitchBranch()
    {
        branch.setting.right = !branch.setting.right;

        if (branch.setting.right)
        {
            reverTransform.localEulerAngles = new Vector3(-15f, 0f, 0f);
        }
        else
        {
            reverTransform.localEulerAngles = new Vector3(15f, 0f, 0f);
        }
    }
}
