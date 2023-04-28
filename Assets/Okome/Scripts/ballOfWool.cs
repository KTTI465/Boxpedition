using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using Unity.VisualScripting;
using UnityEngine.UI;
using static UnityEditor.PlayerSettings;
using ballOfWoolState;

public class ballOfWool : MonoBehaviour
{
    [SerializeField]
    public GameObject animationCamara;

    public GameObject interactImage;

    private Animator animator;
    private bool enabledAnimation;

    private GameObject Player;

    private GameObject _rayHitObject;

    // 〇ボタンが押されているかどうかを取得する
    bool ps4O = false;

    private string _preStateName;
    public ballOfWoolStateProcessor StateProcessor { get; set; } = new ballOfWoolStateProcessor();
    public ballOfWoolStateIdle StateIdle { get; set; } = new ballOfWoolStateIdle();
    public ballOfWoolStateAnimation StateAnimation { get; set; } = new ballOfWoolStateAnimation();
    void Start()
    {
        animator = GetComponent<Animator>();
        enabledAnimation = true;

        StateProcessor.State = StateIdle;
        StateIdle.ExecAction = Idle;
        StateAnimation.ExecAction = Animation;
    }

    void Update()
    {
        //ステートの値が変更されたら実行処理を行う
        if (StateProcessor.State.GetStateName() != _preStateName)
        {
            Debug.Log(" Now State:" + StateProcessor.State.GetStateName());
            _preStateName = StateProcessor.State.GetStateName();
            StateProcessor.Execute();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (enabledAnimation == true)
            {
                _rayHitObject = other.GetComponent<CharacterController>().rayHitObject.collider.gameObject;

                if (_rayHitObject != null && _rayHitObject == gameObject)
                {
                    interactImage.SetActive(true);

                    GetPS4O();

                    if (Input.GetMouseButton(0) || ps4O)
                    {
                        Player = other.gameObject;
                        animator.SetTrigger("rollBallOfWool");
                        enabledAnimation = false;
                    }
                }
                else
                {
                    interactImage.SetActive(false);
                }
            }
            else
            {
                interactImage.SetActive(false);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            interactImage.SetActive(false);
        }
    }

    public void StartAnimation()
    {
        Player.GetComponent<CharacterController>().enabled = false;
        animationCamara.GetComponent<Camera>().depth = 1;
    }
    public void EndAnimation()
    {
        Player.GetComponent<CharacterController>().enabled = true;
        Destroy(animationCamara);
    }

    void GetPS4O()
    {
        if (Gamepad.current != null)
        {
            if (Gamepad.current.buttonEast.isPressed)
            {
                ps4O = true;
            }
            else
            {
                ps4O = false;
            }
        }
    }

    public void Idle()
    {
        Debug.Log("StateがIdleに状態遷移しました。");
    }
    public void Animation()
    {
        Debug.Log("StateがAnimationに状態遷移しました。");
    }
}
