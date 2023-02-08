using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class TrainTargetManager : MonoBehaviour
{
    public TrainTarget[] targets;
    private TrainTarget nowTarget;
    private Transform returnTransform;

    public Transform GetNextTarget(bool first)
    {
        returnTransform = null;

        if (targets.Length != 0)
        {
            if (first)
            {
                nowTarget = targets[0];
                return targets[0].transform;
            }
            else
            {
                foreach (var target in targets)
                {
                    if (nowTarget.CheckNextTarget())
                    {
                        if (nowTarget.enableBranch)
                        {
                            if (nowTarget.setting.right)
                            {
                                if (nowTarget.setting.rightTarget.uuid == target.uuid)
                                {
                                    nowTarget = target;
                                    returnTransform = target.transform;
                                    break;
                                }
                            }
                            else
                            {
                                if (nowTarget.setting.leftTarget.uuid == target.uuid)
                                {
                                    nowTarget = target;
                                    returnTransform = target.transform;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            if (nowTarget.setting.nextTarget.uuid == target.uuid)
                            {
                                nowTarget = target;
                                returnTransform = target.transform;
                                break;
                            }
                        }
                    }
                }
            }
        }

        return returnTransform;
    }
}