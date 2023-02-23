using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class Train : MonoBehaviour
{
    Transform target;
    int point = 0;
    NavMeshAgent agent;
    public float speed = 4f;
    public float rotateSpeed = 1f;

    Transform pos;

    private UnityEngine.CharacterController controller;
    TrainTargetManager manager;

    public bool moveActive = true;

    void Start()
    {
        controller = GetComponent<UnityEngine.CharacterController>();
        agent = GetComponent<NavMeshAgent>();
        manager = GetComponent<TrainTargetManager>();
        agent.autoBraking = false;

        SetTarget(true);
    }

    void SetTarget(bool first)
    {
        target = manager.GetNextTarget(first);

        if (target != null)
        {
            agent.destination = target.position;
        }
        else
        {
            speed = 0f;
        }
    }

    void Update()
    {
        if (!agent.pathPending && agent.remainingDistance <= 0.5f)
        {
            SetTarget(false);
        }

        if (moveActive)
        {
            pos = transform;
            pos.position = new Vector3(agent.path.corners[0].x, transform.position.y, agent.path.corners[0].z);

            Vector3 targetDir = agent.desiredVelocity;
            Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, rotateSpeed * Time.deltaTime, 0f);
            transform.rotation = Quaternion.LookRotation(newDir);

            controller.Move(agent.desiredVelocity.normalized * speed * Time.deltaTime);
        }
    }
}
