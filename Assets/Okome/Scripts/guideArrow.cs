using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class guideArrow : MonoBehaviour
{
    //�ړI�̃I�u�W�F�N�g���i�[
    public Transform target;

    [SerializeField]
    private GameObject player;

    //�v���C���[�Ɩ��̋���
    public float meshOnLength = 2f;

    //���̍���
    [SerializeField]
    private float height;

    private MeshRenderer _meshRenderer;
    private void Start()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
    }
    private void Update()
    {
        if (target != null)
        {
            //��󂪖ړI�̃I�u�W�F�N�g�̂ق��������悤�ɂ���
            transform.LookAt(target);
        }

        //���̈ʒu���v���C���[�̏�ɔz�u
        transform.position = player.transform.position + new Vector3(0.0f, height, 0.0f);

        //�v���C���[�ƖړI�̃I�u�W�F�N�g�̋������߂��Ȃ����Ƃ��ɖ����\���ɂ���
        if (Vector3.Distance(player.transform.position, target.transform.position) <= meshOnLength || target == null)
        {
            _meshRenderer.enabled = false;
        }
        else if (_meshRenderer.enabled == false)
        {
            _meshRenderer.enabled = true;
        }
    }
}
