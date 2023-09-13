using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class guideArrow : MonoBehaviour
{
    //�ړI�̃I�u�W�F�N�g���i�[
    [System.NonSerialized]
    public Transform target;

    [SerializeField]
    private GameObject cameraObj;

    [SerializeField]�@//���̍ő�U��
    private float maxAmplitude;

    [SerializeField]�@//���̓����̑���
    private float arrowSpeed;

    [SerializeField]�@//�ڕW�Ɩ��̍ŏ��U�����́A�ŒZ����
    private float minDistArrowFromTarget;

    [SerializeField]�@//�ڕW�I�u�W�F�N�g����̍����̍��W
    private float heightFromTarget;

    //����
    private float period;

    //�����ƖڕW�I�u�W�F�N�g�̐����̑��̍��W
    private Vector3 perpendicularCoordinates;

    //���̍ŏ��̑傫��
    [System.NonSerialized]
    public Vector3 firstScale;

    private void Start()
    {
        firstScale = transform.localScale;
    }
    private void Update()
    {
        if (target != null)
        {
            //��󂪖ړI�̃I�u�W�F�N�g�̂ق��������悤�ɂ���
            transform.LookAt(target);

            //���̓�����ݒ�
            period = Mathf.PingPong(Time.time * arrowSpeed, maxAmplitude);
            //�i�ڕW�I�u�W�F�N�g�̏�ƃJ���������ԁj�����ƖڕW�I�u�W�F�N�g�̐����̑������߂�
            perpendicularCoordinates = PerpendicularFootPoint(cameraObj.transform.position,
                target.position + (Vector3.up * heightFromTarget), target.position);
            //���̍��W��ݒ�
            transform.position = perpendicularCoordinates - (transform.forward
                * (minDistArrowFromTarget + period));

        }
    }
    Vector3 PerpendicularFootPoint(Vector3 a, Vector3 b, Vector3 p)
    {
        return a + Vector3.Project(p - a, b - a);
    }
}



