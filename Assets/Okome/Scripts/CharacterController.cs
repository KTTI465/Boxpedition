using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    private Rigidbody rb;
    private float xMovement, zMovement;
    private float movementSpeed = 0.1f;
    [SerializeField] private Camera playerCam;
    public GameObject cam;
    private Quaternion cameraRot, characterRot;
    private float sensitivity = 1f;

    //生成するためのboxのPrefabを格納するための変数
    [SerializeField]
    private GameObject box;

    //Playerの子オブジェクトになっているboxを格納するための変数
    private GameObject connectingBox;

    //1回目のジャンプするときの力を指定するための変数
    public float firstJumpPower;

    //2回目のジャンプするときの力を指定するための変数
    public float secondJumpPower;

    //Raycastの長さを格納するための変数
    private float jumpDistance;

    //二段ジャンプをしたかを判定する
    public bool doubleJumped = false;

    //connectingBoxの上にPlayerがくるよう位置を調整するための変数
    //connectingBoxとPlayerの大きさで次第で調整が必要
    private float enterBoxMove = 1f;

    public RaycastHit rayHitObject;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cameraRot = cam.transform.localRotation;
        characterRot = transform.localRotation;
        cameraRot = Quaternion.Euler(0, 0, 0);

        //connectingBoxが無かったときに呼び出す
        if (connectingBox == null)
        {
            //connectingBox としてboxをPlayerと同じ位置と向きで生成
            connectingBox = Instantiate(box, transform.position, transform.rotation);

            //PlayerがconnectingBox の上に来るように移動
            transform.position = new Vector3(transform.position.x, transform.position.y + enterBoxMove, transform.position.z);

            //このオブジェクトをconnectingBox の親オブジェクトにする
            connectingBox.transform.parent = gameObject.transform;
        }
    }

    void Update()
    {
        CharacterJump();
        ray();
    }

    private void FixedUpdate()
    {
        CharacterMovement();
        CharacterRotate();
    }

    private void CharacterMovement()
    {
        xMovement = Input.GetAxisRaw("Horizontal") * movementSpeed;
        zMovement = Input.GetAxisRaw("Vertical") * movementSpeed;

        transform.Translate(xMovement, 0, zMovement);
    }

    private void CharacterRotate()
    {
        //マウスの横方向の動き× sensitivityで横方向の回転をさせている。
        float xRot = Input.GetAxis("Mouse X") * sensitivity;
        characterRot *= Quaternion.Euler(0, xRot, 0);
        transform.localRotation = characterRot;
    }

    private void CharacterJump()
    {
        //地面にRayが付いているかの判定
        bool isGround;

        //connectingBoxがあるとき
        if (connectingBox)
        {
            //connectingBoxがあることも加味してのPlayerが地面についているかを判定するRayの長さ　
            //値は変更する必要あり（今は埋め込みで実装できていないのでこの値）
            //箱とPlayerの大きさ次第でも調整が必要
            jumpDistance = 2.1f;

            //Playerから出ているRayがconnectingBoxを避けるようにlayerを指定(boxのlayer)
            int layerMask = connectingBox.layer;

            isGround = Physics.Raycast(transform.position, Vector3.up * -1f, jumpDistance, layerMask);
        }
        else
        {
            //connectingBoxが無いときにPlayerが地面についているかを判定するRayの長さ
            //Playerの大きさ次第で調整が必要
            jumpDistance = 1.1f;

            isGround = Physics.Raycast(transform.position, Vector3.up * -1f, jumpDistance);
        }

        //スペースキーを押したときにジャンプする
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //地面についていた時
            if (isGround == true)
            {
                rb.velocity = Vector3.up * firstJumpPower;
            }
            //空中にいるときかつ二段ジャンプをしていない時
            else if (isGround == false && doubleJumped == false)
            {
                rb.velocity = Vector3.up * secondJumpPower;

                //boxについているスクリプトのコルーチンを使い、１秒後に箱が消えるようにする
                IEnumerator destroyTimer = connectingBox.GetComponent<Box>().DestroyBox();
                StartCoroutine(destroyTimer);

                //boxには親に追従させるためにRigidbodyがついていないので下に落ちるようにRigidbodyをつける
                connectingBox.AddComponent<Rigidbody>();

                //connectingBoxが下に落ちるようにこのオブジェクトの子からはずす
                connectingBox.transform.parent = null;

                //二段ジャンプした判定をtrueにする
                doubleJumped = true;
            }
        }

        //二段ジャンプをした後の時地面についた場合
        if (isGround == true && doubleJumped == true)
        {
            //二段ジャンプした判定をfalseにする
            doubleJumped = false;

            //connectingBoxが無いとき
            if (connectingBox == null)
            {
                //connectingBoxとして新しくboxをPlayerと同じ位置と向きに生成
                connectingBox = Instantiate(box, transform.position, transform.rotation);

                //connectingBoxの上にPlayerがくるように位置を調整
                transform.position = new Vector3(transform.position.x, transform.position.y + enterBoxMove, transform.position.z);

                //connectingBoxの親オブジェクトにこのオブジェクトを指定
                connectingBox.transform.parent = gameObject.transform;
            }
        }
    }

    public void ray()
    {
        Physics.Raycast(playerCam.ViewportPointToRay(new Vector2(0.5f, 0.5f)), out rayHitObject, 30f);
    }
}
