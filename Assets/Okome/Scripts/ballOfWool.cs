using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ballOfWool : MonoBehaviour
{
    [SerializeField]
    public GameObject animationCamara;

    private Animator animator;
    private bool enabledAnimation;

    private GameObject Player;

    GameObject _rayHitObject;

    void Start()
    {
        animator = GetComponent<Animator>();
        enabledAnimation = true;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (enabledAnimation == true)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    _rayHitObject = other.GetComponent<CharacterController>().rayHitObject.collider.gameObject;
                    if (_rayHitObject != null && _rayHitObject == gameObject)
                    {
                        Player = other.gameObject;
                        animator.SetTrigger("rollBallOfWool");
                        enabledAnimation = false;
                    }
                }
            }
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
}
