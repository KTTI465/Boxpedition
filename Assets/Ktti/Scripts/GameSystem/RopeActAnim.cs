using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using CharacterState;
using ClimbState;

public class RopeActAnim : MonoBehaviour
{
    [SerializeField]
    Transform up_transform;
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
    bool up = false;
    [SerializeField, NonEditable] 
    bool down = false;

    private string _preStateName;
    private string _CpreStateName;

    public CharacterStateProcessor CharacterStateProcessor { get; set; } = new CharacterStateProcessor();
    public CharacterStateIdle CharacterStateIdle { get; set; } = new CharacterStateIdle();
    public CharacterStateMove CharacterStateMove { get; set; } = new CharacterStateMove();
    public ClimbStateProcessor ClimbStateProcessor { get; set; } = new ClimbStateProcessor();
    public ClimbStateIdle ClimbStateIdle { get; set; } = new ClimbStateIdle();
    public StateClimb StateClimb { get; set; } = new StateClimb();

    void Start()
    {
        player = warpRope.player.gameObject;
        characterController = player.GetComponent<CharacterController>();
        animator = player.GetComponent<Animator>();
        rb = player.GetComponent<Rigidbody>();
        fadeinout = warpRope.fadeinout;
        //animatorBox = player.getBoxAnim();

        CharacterStateIdle.ExecAction = Idle;
        CharacterStateMove.ExecAction = Move;
        CharacterStateProcessor.State = CharacterStateIdle;
        ClimbStateIdle.ExecAction = ClimbIdle;
        StateClimb.ExecAction = ClimbMove;
        ClimbStateProcessor.State = ClimbStateIdle;
    }

    void Update()
    {
        if (grabRope)
        {
            characterController.enabled = false;
            rb.isKinematic = true;
            warpRope.enable = false;

            if (up)
            {
                if (!climb1 && !climb2)
                {
                    if (new Vector3(player.transform.position.x, 0f, player.transform.position.z) == new Vector3(down_transform.position.x, 0f, down_transform.position.z))
                    {
                        animator.SetBool("walk", false);
                        climb1 = true;
                        CharacterStateProcessor.State = CharacterStateIdle;
                    }
                    else
                    {
                        player.transform.position = Vector3.MoveTowards(player.transform.position, new Vector3(down_transform.position.x, player.transform.position.y, down_transform.position.z), moveSpeed * Time.deltaTime);
                        animator.SetBool("walk", true);
                        CharacterStateProcessor.State = CharacterStateMove;
                    }
                }

                if (climb1)
                {
                    animator.SetBool("climbStay", true);
                    animator.SetBool("climb", true);

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
                        climb2 = false;
                        grabRope = false;
                        up = false;

                        warpRope.enable = true;
                        warpRope.SetRopeDown(false);
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
                        player.transform.rotation = up_transform.rotation;
                        climb1 = true;
                        CharacterStateProcessor.State = CharacterStateIdle;
                    }
                    else
                    {
                        player.transform.position = Vector3.MoveTowards(player.transform.position, new Vector3(up_transform.position.x, player.transform.position.y, up_transform.position.z), moveSpeed * Time.deltaTime);
                        animator.SetBool("walk", true);
                        CharacterStateProcessor.State = CharacterStateMove;
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
        }

        //ステートの値が変更されたら実行処理を行う
        if (CharacterStateProcessor.State.GetStateName() != _preStateName)
        {
            _preStateName = CharacterStateProcessor.State.GetStateName();
            CharacterStateProcessor.Execute();
        }
        if (ClimbStateProcessor.State.GetStateName() != _CpreStateName)
        {
            _CpreStateName = CharacterStateProcessor.State.GetStateName();
            CharacterStateProcessor.Execute();
        }
    }

    public void Idle()
    {
        //Debug.Log("CharacterStateがIdleに状態遷移しました。");
    }
    public void Move()
    {
        //Debug.Log("CharacterStateがMoveに状態遷移しました。");
    }
    public void ClimbIdle()
    {
        //Debug.Log("ClimbStateがIdleに状態遷移しました。");
    }
    public void ClimbMove()
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
}
