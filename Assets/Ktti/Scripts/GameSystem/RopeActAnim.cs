using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RopeActAnim : MonoBehaviour
{
    [SerializeField]
    Transform up_transform;
    [SerializeField]
    Transform down_transform;

    [SerializeField]
    warpRope warpRope;
    [SerializeField]
    Animator animator;

    [SerializeField, NonEditable]
    GameObject player;
    CharacterController characterController;

    [SerializeField, NonEditable]
    bool grabRope = false;

    [SerializeField]
    GameObject climbInteractImage;

    [SerializeField]
    float moveSpeed;
    [SerializeField]
    float climbSpeed;

    bool crimb = false;
    bool up = false;
    bool down = false;

    void Start()
    {
        player = warpRope.player.gameObject;
        characterController = player.GetComponent<CharacterController>();
    }

    void Update()
    {
        if (grabRope)
        {
            characterController.enabled = false;

            if (up)
            {
                if (new Vector3(transform.position.x, 0f, transform.position.z) == new Vector3(down_transform.position.x, 0f, down_transform.position.z))
                {
                    crimb = true;
                }
                else
                {
                    player.transform.position = Vector3.MoveTowards(player.transform.position, new Vector3(down_transform.position.x, player.transform.position.y, down_transform.position.z), moveSpeed * Time.deltaTime);
                }

                if (crimb)
                {
                    animator.SetBool("climbStay", true);
                    animator.SetBool("climb", true);

                    player.transform.position = Vector3.MoveTowards(player.transform.position, up_transform.position, climbSpeed * Time.deltaTime);
                }
            }
            if (down)
            {
                if (new Vector3(transform.position.x, 0f, transform.position.z) == new Vector3(up_transform.position.x, 0f, up_transform.position.z))
                {
                    crimb = true;
                }
                else
                {
                    player.transform.position = Vector3.MoveTowards(player.transform.position, new Vector3(up_transform.position.x, player.transform.position.y, up_transform.position.z), moveSpeed * Time.deltaTime);
                }

                if (crimb)
                {
                    animator.SetBool("climbStay", true);
                    player.transform.position = Vector3.MoveTowards(player.transform.position, down_transform.position, climbSpeed * Time.deltaTime);
                }
            }
        }
    }

    public void UpAnim()
    {
        up = true;
        grabRope = true;
    }

    public void DownAnim()
    {
        down = true;
        grabRope = true;
    }
}
