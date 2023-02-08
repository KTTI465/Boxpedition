using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TrainTarget : MonoBehaviour
{
    public bool enableBranch = false;
    public TargetSetting setting = new TargetSetting();
    public int uuid;

    private void Awake()
    {
       uuid = GetInstanceID();
    }

    [System.Serializable]
    public class TargetSetting
    {
        public TrainTarget nextTarget;

        public TrainTarget leftTarget;
        public TrainTarget rightTarget;
        public bool right = false;
    }

    public bool CheckNextTarget()
    {
        if (!enableBranch)
        {
            if (setting.nextTarget == null)
            {
                return false;
            }

            return true;
        }
        else
        {
            if (setting.leftTarget == null || setting.rightTarget == null)
            {
                return false;
            }

            return true;
        }
    }
}
