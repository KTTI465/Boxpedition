using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

[System.Serializable]
public class Train : MonoBehaviour
{
    public bool lead = false;
    public TrainSetting setting = new TrainSetting();

    public Transform target;
    NavMeshAgent agent;

    public bool moveActive = true;

    Transform pos;

    private UnityEngine.CharacterController controller;

    TrainTargetManager manager;

    [System.Serializable]
    public class TrainSetting
    {
        public TrainTargetManager manager;
        public float speed;
        public float rotateSpeed;
        public bool moveActive;

        public Train train;
    }

    void Start()
    {
        controller = GetComponent<UnityEngine.CharacterController>();
        agent = GetComponent<NavMeshAgent>();
        agent.autoBraking = false;

        if (lead)
        {
            manager = GetComponent<TrainTargetManager>();
        }
        else
        {
            setting.speed = setting.train.setting.speed;
            setting.rotateSpeed = setting.train.setting.rotateSpeed;
            setting.moveActive = setting.train.setting.moveActive;
        }

        SetTarget(true);
    }

    void SetTarget(bool first)
    {
        if (lead)
        {
            target = manager.GetNextTarget(first);

            if (target != null)
            {
                agent.destination = target.position;
            }
            else
            {
                setting.speed = 0f;
            }
        }
        else
        {
            target = setting.train.target;
            if (target != null)
            {
                agent.destination = target.position;
            }
            else
            {
                setting.speed = 0f;
            }
        }
    }

    void Update()
    {
        if (!agent.pathPending && agent.remainingDistance <= 0.5f)
        {
            SetTarget(false);
        }

        if (!lead)
        {
            setting.speed = setting.train.setting.speed;
            setting.rotateSpeed = setting.train.setting.rotateSpeed;
            setting.moveActive = setting.train.setting.moveActive;
        }

        if (moveActive)
        {
            pos = transform;
            pos.position = new Vector3(agent.path.corners[0].x, transform.position.y, agent.path.corners[0].z);

            Vector3 targetDir = agent.desiredVelocity;
            Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, setting.rotateSpeed * Time.deltaTime, 0f);
            transform.rotation = Quaternion.LookRotation(newDir);

            controller.Move(agent.desiredVelocity.normalized * setting.speed * Time.deltaTime);
        }
    }
}
