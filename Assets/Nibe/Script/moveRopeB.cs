using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class moveRopeB : MonoBehaviour
{
    private float xMovement, zMovement;
    private float movementSpeed = 0.05f;  //‘ŠE—p

    //ÚG‚µ‚½‚©‚Ç‚¤‚©‚Ì”»’è
    private bool moveOn = false;

    //ƒvƒŒƒCƒ„[‚ÌrigidbodyŠi”[—p•Ï”
    new Rigidbody rigidbody;
    GameObject player;

    private float speed = 5.0f;


    void Start()
    {
        //ƒvƒŒƒCƒ„[‚ğŒ©‚Â‚¯‚é
        player = GameObject.FindGameObjectWithTag("Player");
        rigidbody = player.gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (moveOn == true && Input.GetKey(KeyCode.Space))  //“o‚é
        {
            player.transform.position = Vector3.MoveTowards(player.transform.position, this.transform.position, speed * Time.deltaTime);
            CharacterMovement();  //‘ŠE
        }
        else
        {
            if (moveOn == true)
            {
                moveOn = false;

                //d—Í‚ğ•œŠˆ‚³‚¹‚é
                rigidbody.isKinematic = false;

                //eqŠÖŒW‚ğ‰ğœ
                player.gameObject.transform.parent = null;
            }
        }
    }

    void OnTriggerStay(Collider col)
    {
        if (col.tag == "Player")
        {
            moveOn = true;

            //Rigidbody‚ğ’â~
            rigidbody.velocity = Vector3.zero;

            //d—Í‚ğ’â~‚³‚¹‚é
            rigidbody.isKinematic = true;

            //eqŠÖŒW‚É‚·‚é
            player.gameObject.transform.parent = this.gameObject.transform;
        }
    }

    private void CharacterMovement()
    {
        xMovement = Input.GetAxisRaw("Horizontal") * movementSpeed;
        zMovement = Input.GetAxisRaw("Vertical") * movementSpeed;

        player.transform.Translate(-xMovement, 0, -zMovement);  //‘ŠE‚·‚é‚½‚ß‚É‹tŒü‚«‚É—Í‰Á‚¦‚é
    }
}