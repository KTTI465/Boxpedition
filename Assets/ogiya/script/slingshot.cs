using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slingshot : MonoBehaviour
{
    //�X�N���v�g����ݒ�ł���^�[�Q�b�g�̃|�W�V����
    public int targetX;
    public int targetY;
    public int targetZ;

    //���ˑ��x
    public float speed = 100;
    [SerializeField] private Transform obj;
    [SerializeField] private Transform sling;
    [Range(0.0f, 180.0f)] public float arcAngle = 60.0f;
    float Travel;
    bool Throw = false;

    Vector3 pivot;
    Vector3 FromVector;
    Vector3 ToVector;

    private bool IsSet = false;
    private bool IsPosition = false;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 slingpos = this.transform.position;
        Vector3 objpos = obj.transform.position;
        Vector3 targetpos = new Vector3(targetX, targetY, targetZ);
        float arie = Vector3.Distance(objpos, slingpos);

        var targethalfAngle = Mathf.Tan(Mathf.Deg2Rad * arcAngle * 0.5f);
        var midPos = (objpos + targetpos) * 0.5f;
        var half = Vector3.Distance(objpos, midPos);

        pivot = midPos;
        pivot.y -= half / targethalfAngle;

        //���S����o���n�A���S����ړI�n�ւ̃x�N�g�������߂Ă���
        FromVector = objpos - pivot;
        ToVector = targetpos - pivot;

        //�ړ��ʂ�0.0�Ƀ��Z�b�g���Ă���
        Travel = 0.0f;

        if (arie <= 5f && IsSet == false)
        {
            IsSet = true;
            IsPosition = true;

            sling.LookAt(targetpos);
        }
        if (IsPosition == true)
        {
            //�@�Z�b�g����I�u�W�F�N�g�̈ʒu���w��
            obj.transform.parent = sling.transform;
            obj.transform.position = new Vector3(slingpos.x, slingpos.y + 3, slingpos.z);
        }
        // ����L�[�Ŕ���
        if (Input.GetKey(KeyCode.R) && IsSet == true)
        {
            Throw = true;
        }
        if (Throw == true)
        {
            IsPosition = false;
            obj.gameObject.transform.parent = null;
            Travel += speed * Time.deltaTime;

            //�~�ʂ̒����Ŋ����āA�~�ʏ��i�s�������������߂�
            var t = Travel / (FromVector.magnitude * Mathf.Deg2Rad * arcAngle);

            if (t < 1.0f)
            {
                //FromVector��ToVector��i�s�����ŕ�Ԃ��APivot�𑫂��Č��݂̈ʒu�Ƃ���
                obj.transform.position = Vector3.Slerp(FromVector, ToVector, t) + pivot;
            }
            else
            {
                //t��1.0�ɓ��B������ړ��I���Ƃ���
                obj.transform.position = ToVector + pivot;
                Throw = false;
            }
        }
    }
}
