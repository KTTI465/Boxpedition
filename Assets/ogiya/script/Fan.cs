using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Fan : MonoBehaviour
{
    [SerializeField]
    public GameObject player;
    public GameObject fan;
    public GameObject button;

    public bool FanAction�@= false;
    public bool OnOff = true;
    public bool ps40 = false;


    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        Vector3 FanA = fan.transform.position;
        Vector3 P = player.transform.position;
        Vector3 Off = button.transform.position;
        Vector3 On = new Vector3(Off.x,Off.y,Off.z + 0.3f);

        float arie = Vector3.Distance(FanA, P);

        //�X�C�b�`�̃I���I�t����
        if (arie < 10.0f && ps40 == true)
        {
            FanAction = true;
        }
        if (FanAction == true && OnOff == true)
        {
            //�X�C�b�`���I���̎��ɃX�C�b�`�̈ʒu���ړ�
            button.transform.position = new Vector3(On.x,On.y,On.z);
            OnOff = false;
        }
        if(FanAction == false)
        {
            button.transform.position = new Vector3(Off.x, Off.y, Off.z);
        }


    }
    void OnTriggerStay(Collider other)
    {
        //������΂�����
        if (other.CompareTag("Player") && FanAction == true)
        {
            //�v���C���[�̈ʒu�����֍X�V��������
            Vector3 newpos = player.transform.position;
            player.transform.position = new Vector3(newpos.x, newpos.y + 0.1f, newpos.z - 1.0f);
        }
    }

    void GetPS40()
    {
        //�R���g���[���[�̃{�^����F��
        if (Gamepad.current != null)
        {
            if (Gamepad.current.buttonEast.isPressed)
            {
                //�Z�{�^������������
                ps40 = true;
            }
            else
            {
                //�����łȂ���Δ���Ȃ�
                ps40 = false;
            }
        }
    }
}
