using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using ClimbState;

public class RopeActAnim : MonoBehaviour
{
    [SerializeField]
    Transform up_transform;
    [SerializeField]
    Transform up2_transform;
    [SerializeField]
    Transform down_transform;

    [SerializeField]
    warpRope warpRope;
    [SerializeField, NonEditable]
    Animator animator;
    [SerializeField, NonEditable]
    Animator animatorBox;

    [SerializeField, NonEditable]
    GameObject player;
    CharacterController characterController;
    Rigidbody rb;
    [SerializeField, NonEditable]
    Box box;

    Fadeinout fadeinout;

    [SerializeField, NonEditable]
    bool grabRope = false;

    [SerializeField]
    float moveSpeed;
    [SerializeField]
    float climbSpeed;
    [SerializeField]
    float MoveDistance = 5f;
    
    [SerializeField, NonEditable] 
    bool climb1 = false;
    [SerializeField, NonEditable]
    bool climb2 = false;
    [SerializeField, NonEditable]
    bool climb3 = false;
    [SerializeField, NonEditable] 
    bool up = false;
    [SerializeField, NonEditable] 
    bool down = false;

    private string _preStateName;

    public ClimbStateProcessor climbStateProcessor { get; set; } = new ClimbStateProcessor();
    public ClimbStateIdle climbStateIdle { get; set; } = new ClimbStateIdle();
    public ClimbStateMove climbStateMove { get; set; } = new ClimbStateMove();
    public ClimbStateClimb climbStateClimb { get; set; } = new ClimbStateClimb();

    void Start()
    {
        player = warpRope.player.gameObject;
        characterController = player.GetComponent<CharacterController>();
        animator = player.GetComponent<Animator>();
        rb = player.GetComponent<Rigidbody>();
        fadeinout = warpRope.fadeinout;

        if (characterController.GetBox() != null)
        {
            box = characterController.GetBox();
            animatorBox = box.GetAnim();
        }

        climbStateIdle.ExecAction = Idle;
        climbStateMove.ExecAction = Move;
        climbStateClimb.ExecAction = Climb;
        climbStateProcessor.State = climbStateIdle;
    }

    void Update()
    {
        if (characterController.GetBox() != null)
        {
            box = characterController.GetBox();
            animatorBox = box.GetAnim();
        }

        if (grabRope)
        {
            characterController.enabled = false;
            rb.isKinematic = true;
            warpRope.enable = false;
            box.enabled = false;

            if (up)
            {
                if (!climb1 && !climb2 && !climb3)
                {
                    if (new Vector3(player.transform.position.x, 0f, player.transform.position.z) == new Vector3(down_transform.position.x, 0f, down_transform.position.z))
                    {
                        animator.SetBool("walk", false);
                        animatorBox.SetBool("walk", false);
                        player.transform.rotation = down_transform.rotation;
                        climb1 = true;
                        climbStateProcessor.State = climbStateIdle;
                    }
                    else
                    {
                        player.transform.position = Vector3.MoveTowards(player.transform.position, new Vector3(down_transform.position.x, player.transform.position.y, down_transform.position.z), moveSpeed * Time.deltaTime);
                        animator.SetBool("walk", true);
                        animatorBox.SetBool("walk", true);
                        climbStateProcessor.State = climbStateMove;
                    }
                }

                if (climb1)
                {
                    animator.SetBool("climbStay", true);
                    animator.SetBool("climb", true);

                    climbStateProcessor.State = climbStateClimb;

                    player.transform.position = Vector3.MoveTowards(player.transform.position, up_transform.position, climbSpeed * Time.deltaTime);
                    
                    if (Vector3.Distance(player.transform.position, down_transform.position) > MoveDistance)
                    {
                        fadeinout.fadeout = true;
                        player.transform.position = new Vector3(up_transform.position.x, up_transform.position.y - MoveDistance, up_transform.position.z);
                        climb1 = false;
                        climb2 = true;
                    }
                }

                if (climb2)
                {
                    player.transform.position = Vector3.MoveTowards(player.transform.position, up_transform.position, climbSpeed * Time.deltaTime);

                    if (player.transform.position == up_transform.position)
                    {
                        animator.SetBool("climbStay", false);
                        animator.SetBool("climb", false);
                        animator.SetBool("walk", true);
                        animatorBox.SetBool("walk", true);
                        climb2 = false;
                        climb3 = true;

                        climbStateProcessor.State = climbStateMove;
                    }
                }

                if (climb3)
                {
                    player.transform.position = Vector3.MoveTowards(player.transform.position, up2_transform.position, climbSpeed * Time.deltaTime);

                    if (player.transform.position == up2_transform.position)
                    {
                        animator.SetBool("walk", false);
                        animatorBox.SetBool("walk", false);
                        climb3 = false;
                        grabRope = false;
                        up = false;

                        warpRope.enable = true;
                        warpRope.SetRopeDown(false);

                        climbStateProcessor.State = climbStateIdle;
                    }
                }
            }
            if (down)
            {
                if (!climb1 && !climb2)
                {
                    if (new Vector3(player.transform.position.x, 0f, player.transform.position.z) == new Vector3(up_transform.position.x, 0f, up_transform.position.z))
                    {
                        animator.SetBool("walk", false);
                        animatorBox.SetBool("walk", false);
                        player.transform.rotation = up_transform.rotation;
                        climb1 = true;
                        climbStateProcessor.State = climbStateIdle;
                    }
                    else
                    {
                        player.transform.position = Vector3.MoveTowards(player.transform.position, new Vector3(up_transform.position.x, player.transform.position.y, up_transform.position.z), moveSpeed * Time.deltaTime);
                        animator.SetBool("walk", true);
                        animatorBox.SetBool("walk", true);
                        climbStateProcessor.State = climbStateMove;
                    }
                }

                if (climb1)
                {
                    animator.SetBool("climbStay", true);
                    player.transform.position = Vector3.MoveTowards(player.transform.position, down_transform.position, climbSpeed * Time.deltaTime);

                    if (Vector3.Distance(player.transform.position, up_transform.position) > MoveDistance)
                    {
                        fadeinout.fadeout = true;
                        player.transform.position = new Vector3(down_transform.position.x, down_transform.position.y + MoveDistance, down_transform.position.z);
                        climb1 = false;
                        climb2 = true;
                    }
                }

                if (climb2)
                {
                    player.transform.position = Vector3.MoveTowards(player.transform.position, down_transform.position, climbSpeed * Time.deltaTime);

                    if (player.transform.position == down_transform.position)
                    {
                        animator.SetBool("climbStay", false);
                        climb2 = false;
                        grabRope = false;
                        down = false;

                        warpRope.enable = true;
                        warpRope.SetRopeUp(false);
                    }
                }
            }
        }
        else
        {
            characterController.enabled = true;
            rb.isKinematic = false;
            warpRope.enable = true;
            box.enabled = true;
        }

        //ステートの値が変更されたら実行処理を行う
        if (climbStateProcessor.State.GetStateName() != _preStateName)
        {
            _preStateName = climbStateProcessor.State.GetStateName();
            climbStateProcessor.Execute();
        }
    }

    public void Idle()
    {
        //Debug.Log("ClimbStateがIdleに状態遷移しました。");
    }
    public void Move()
    {
        //Debug.Log("ClimbStateがMoveに状態遷移しました。");
    }
    public void Climb()
    {
        //Debug.Log("ClimbStateがMoveに状態遷移しました。");
    }

    public void UpAnim()
    {
        up = true;
        grabRope = true;
        player.transform.LookAt(down_transform);
    }

    public void DownAnim()
    {
        down = true;
        grabRope = true;
        player.transform.LookAt(up_transform);
    }

    public bool ClimbChecker()
    {
        return up && (climb1 || climb2);
    }
}
