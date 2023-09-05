using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class plug : MonoBehaviour
{
    //ロープの子要素を格納
    GameObject[] plugRope;
    //子要素の合計
    int plugRopeCount;
    //子要素それぞれが何番目かを決める数字
    int plugRopeIndex;

    //プレイヤーを格納
    [SerializeField] GameObject player;
    //プレイヤーのCharacterController,Rigidbody,Animatorを格納
    CharacterController characterController;
    Rigidbody playerRb;
    Animator playerAnimator;

    //掴めるかの判定
    bool canGrab;
    //滑っているときの判定
    bool isSlideDown;
    //滑るスピード
    float speed = 20f;

    LineRenderer line;
    //コントローラーのボタンの判定
    bool ps4O;
    [SerializeField]//インタラクトの画像を格納
    private GameObject interactImage;

    [SerializeField]//英語のインタラクトの画像を格納
    private GameObject interactImageEnglish;

    // Start is called before the first frame update
    void Start()
    {
        characterController = player.GetComponent<CharacterController>();
        playerRb = player.GetComponent<Rigidbody>();
        playerAnimator = player.GetComponent<Animator>();

        canGrab = false;
        isSlideDown = false;
        interactImage.SetActive(false);

        plugRopeIndex = 0;
        plugRopeCount = transform.childCount;
        plugRope = new GameObject[plugRopeCount];

        line = GetComponent<LineRenderer>();
        line.positionCount = plugRopeCount;
        foreach (Transform rope in transform)
        {
            //子オブジェクトを順番に格納して見た目を非表示にしていく
            plugRope[plugRopeIndex++] = rope.gameObject;
            rope.GetComponent<MeshRenderer>().enabled = false;
        }
        //ロープを掴んだ時に移動する子オブジェクト（6番目のところから滑り始めるようになっている）
        plugRopeIndex = 6;
        //物理演算をとめるためのコルーチン（n秒後,）
        StartCoroutine(StopPhysics(10.0f, plugRope));


        if (PlayerPrefs.GetString("Language") == "English")
        {
            interactImage = interactImageEnglish;
        }
    }

    // Update is called once per frame
    void Update()
    {
        GetPS4O();

        //線を表示する
        int idx = 0;
        foreach (GameObject rope in plugRope)
        {
            line.SetPosition(idx, rope.transform.position);
            idx++;
        }

        //掴めるときにボタンを押すと掴む
        if (canGrab == true)
        {
            if (Input.GetMouseButtonDown(0) || ps4O)
            {
                isSlideDown = true;
                interactImage.SetActive(false);
                playerRb.isKinematic = true;
                playerRb.useGravity = false;

                //アニメーションの設定
                playerAnimator.SetBool("jump", false);
                playerAnimator.SetBool("jump2", false);
                playerAnimator.SetBool("climbStay", true);

                //キャラクターの操作をできないようにする
                if (characterController.enabled == true)
                {
                    characterController.enabled = false;
                }
            }
        }
        //滑っているとき
        if (isSlideDown == true)
        {
            //アニメーションの設定
            playerAnimator.SetBool("jump", false);
            playerAnimator.SetBool("jump2", false);
            playerAnimator.SetBool("climbStay", true);

            //キャラクターの向きの設定
            player.transform.eulerAngles = transform.up * 180f;
            //目的の子オブジェクトの位置を設定してその位置に向かって滑っていく
            Vector3 nextPos = plugRope[plugRopeIndex].transform.position - new Vector3(0.0f, 3.0f, 0.0f);
            player.transform.position = Vector3.MoveTowards(player.transform.position, nextPos, speed * Time.deltaTime);
            //目的の子オブジェクトに到達したとき
            if (player.transform.position == nextPos)
            {
                //最後の子オブジェクトに到達したとき
                if (plugRopeIndex == plugRopeCount - 1)
                {
                    isSlideDown = false;
                    player.GetComponent<Rigidbody>().isKinematic = false;
                    player.GetComponent<Rigidbody>().useGravity = true;
                    playerAnimator.SetBool("climbStay", false);
                    if (characterController.enabled == false)
                    {
                        characterController.enabled = true;
                    }
                }
                //次の子オブジェクトの数字にする
                plugRopeIndex++;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            //掴むことができるようにして、インタラクトの画像を表示する
            canGrab = true;
            interactImage.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            //掴むことができないようにして、インタラクトの画像を非表示にする
            canGrab = false;
            interactImage.SetActive(false);
        }
    }

    IEnumerator StopPhysics(float time, GameObject[] plugRope)
    {
        //一定時間たったらロープの物理演算をとめる
        yield return new WaitForSecondsRealtime(time);
        foreach (GameObject rope in plugRope)
        {
            rope.GetComponent<Rigidbody>().isKinematic = true;
            rope.GetComponent<Rigidbody>().useGravity = false;
        }
    }

    void GetPS4O()
    {
        if (Gamepad.current != null)
        {
            if (Gamepad.current.buttonEast.wasPressedThisFrame)
            {
                ps4O = true;
            }
            else
            {
                ps4O = false;
            }
        }
    }
}
