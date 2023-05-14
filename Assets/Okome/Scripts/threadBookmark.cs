using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class threadBookmark : MonoBehaviour
{
    [SerializeField]
    private LineRenderer line = null;

    [SerializeField]
    private Transform pivot = null;


    public void Update()
    {
        // �q�������̎n�_�ƏI�_�̍��W���X�V
        line.SetPosition(0, this.transform.position);
        line.SetPosition(1, this.pivot.position);
    }
}
