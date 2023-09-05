using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deskLightCord : MonoBehaviour
{
    //���[�v�̎q�v�f���i�[
    GameObject[] cord;
    //�q�v�f�̍��v
    int cordCount;
    //�q�v�f���ꂼ�ꂪ���Ԗڂ������߂鐔��
    int cordIndex;

    LineRenderer line;
    private void Start()
    {
        cordIndex = 0;
        cordCount = transform.childCount;
        cord = new GameObject[cordCount];

        line = GetComponent<LineRenderer>();
        line.positionCount = cordCount;
        foreach (Transform i in transform)
        {
            //�q�I�u�W�F�N�g�����ԂɊi�[���Č����ڂ��\���ɂ��Ă���
            cord[cordIndex++] = i.gameObject;
            i.GetComponent<MeshRenderer>().enabled = false;
        }

        //�������Z���Ƃ߂邽�߂̃R���[�`���in�b��,�j
        StartCoroutine(StopPhysics(5.0f, cord));
    }
    private void Update()
    {
        //����\������
        int idx = 0;
        foreach (GameObject i in cord)
        {
            line.SetPosition(idx, i.transform.position);
            idx++;
        }
    }

    IEnumerator StopPhysics(float time, GameObject[] cord)
    {
        //��莞�Ԃ������烍�[�v�̕������Z���Ƃ߂�
        yield return new WaitForSecondsRealtime(time);
        foreach (GameObject i in cord)
        {
            i.GetComponent<Rigidbody>().isKinematic = true;
            i.GetComponent<Rigidbody>().useGravity = false;
        }
    }
}
