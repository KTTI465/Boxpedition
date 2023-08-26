using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blockWayMonster : MonoBehaviour
{
    [SerializeField] GameObject player;
    CharacterController characterController;
    Animator playerAnimator;

    [SerializeField] GameObject eventCamera;

    Animator monstersAnimator;

    bool isPlayerStepBack;
    bool isEvent;

    Vector3 inEventPlayerPosition;
    Vector3 stepBackPosition;

    [SerializeField] AudioClip monstersSound;
    AudioSource audioSource;
    void Start()
    {
        characterController = player.GetComponent<CharacterController>();
        playerAnimator = player.GetComponent<Animator>();
        monstersAnimator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        eventCamera.SetActive(false);
    }

    void Update()
    {
        if (isEvent == true)
        {
            if (isPlayerStepBack == true)
            {
                //キャラクターの向きの設定
                player.transform.rotation = Quaternion.RotateTowards(player.transform.rotation, Quaternion.Euler(0f, 180f, 0f), 900f * Time.deltaTime);
                player.transform.position = Vector3.MoveTowards(player.transform.position, stepBackPosition, 10f * Time.deltaTime);
            }
            playerAnimator.SetBool("walk", false);
        }
    }
    public void StartEvent()
    {
        isEvent = true;
        characterController.enabled = false;
        //キャラクターの向きの設定
        player.transform.eulerAngles = transform.up;
        inEventPlayerPosition = new Vector3(transform.position.x - 3f, player.transform.position.y, transform.position.z - 40f);
        stepBackPosition = inEventPlayerPosition - new Vector3(0f, 0f, 10f);
        player.transform.position = inEventPlayerPosition;
        characterController.StateProcessor.State = characterController.StateIdle;
        playerAnimator.SetBool("walk", false);
        eventCamera.SetActive(true);
    }

    public void MonstersSound()
    {
        Debug.Log("sound");
        audioSource.PlayOneShot(monstersSound);
    }

    public void PlayerStepBack()
    {
        isPlayerStepBack = true;
    }

    public void EndEvent()
    {
        isEvent = false;
        characterController.enabled = true;
        eventCamera.SetActive(false);
        isPlayerStepBack = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player.gameObject)
        {
            monstersAnimator.SetTrigger("threaten");
        }
    }
}
