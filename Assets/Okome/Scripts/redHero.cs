using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class redHero : MonoBehaviour
{
    private Animator animator;

    [SerializeField]
    private GameObject player;
    private Rigidbody playerRb;
    private CharacterController characterController;
    private Animator playerAnimator;

    private Vector3 playerEventPos;
    bool canTalk;

    [SerializeField]
    private GameObject eventCamera;

    [SerializeField]
    private GameObject guideArrow;

    [SerializeField]
    private GameObject offScreenArrow;

    private bool isEvent = false;

    [SerializeField]
    private blockWayMonster monsters;

    [SerializeField]
    private GameObject rope;

    private GameObject interactImage;

    [SerializeField]//パッド操作のときのインタラクトの画像
    private GameObject interactImageGamepad;

    [SerializeField]
    private GameObject interactImageEnglish;

    private void Start()
    {
        ImageChange();
        animator = GetComponent<Animator>();

        playerRb = player.GetComponent<Rigidbody>();
        characterController = player.GetComponent<CharacterController>();
        playerAnimator = player.GetComponent<Animator>();

        playerEventPos = transform.position + transform.right * -6f + transform.up * 1.3f + transform.forward * 10f;

        //言語が英語だった時、掴むの画像を英語版に変更する
        if (PlayerPrefs.GetString("Language") == "English")
        {
            interactImageGamepad = interactImageEnglish;
        }

        rope.SetActive(false);
        eventCamera.SetActive(false);
    }

    private void Update()
    {
        ImageChange();
        if (monsters.existRedHero == false)
        {
            if (canTalk == true && isEvent == false)
            {
                if (characterController.InteractGameObjectsList != null &&
                    characterController.InteractGameObjectsList.Contains(gameObject))
                {
                    interactImage.SetActive(true);
                    if (Gamepad.current != null && Gamepad.current.buttonEast.wasPressedThisFrame)
                    {
                        animator.SetTrigger("talk");
                    }
                }
                else
                {
                    interactImage.SetActive(false);
                }
            }
        }
    }

    private void FixedUpdate()
    {
        if (isEvent == true)
        {
            playerRb.AddForce(Vector3.up * -100.0f, ForceMode.Force);
            player.transform.LookAt(new Vector3(transform.position.x, player.transform.position.y, transform.position.z));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            canTalk = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            canTalk = false;
            interactImage.SetActive(false);
        }
    }

    public void StartEvent()
    {
        isEvent = true;
        playerRb.velocity = Vector3.zero;
        player.transform.position = playerEventPos;
        //プレイヤーの操作を不可にする
        characterController.Switch = true;
        characterController.canJump = false;

        guideArrow.SetActive(false);
        offScreenArrow.SetActive(false);
        interactImage.SetActive(false);
        characterController.StateProcessor.State = characterController.StateIdle;
        playerAnimator.SetBool("walk", false);
        eventCamera.SetActive(true);
    }



    public void PlayerJump()
    {
        playerRb.velocity = Vector3.up * 30f;
    }

    public void ropeSwingStart()
    {
        rope.SetActive(true);
    }

    public void ropeSwingEnd()
    {
        rope.SetActive(false);
    }

    public void EndEvent()
    {
        isEvent = false;
        //キャラクターを操作可能にする
        characterController.Switch = false;
        characterController.canJump = true;

        guideArrow.SetActive(true);
        offScreenArrow.SetActive(true);
        eventCamera.SetActive(false);
        monsters.existRedHero = true;
    }

    void ImageChange()
    {
        //パッド操作のとき
        if (Gamepad.current != null)
        {
            if (interactImage != interactImageGamepad)
            {
                //パッド操作のインタラクトの画像を設定
                interactImage = interactImageGamepad;
            }
        }
    }
}
