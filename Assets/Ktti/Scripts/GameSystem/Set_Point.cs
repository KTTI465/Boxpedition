using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Set_Point : MonoBehaviour
{
    //�����Z����X�R�A
    public int point;
    //��x�������s���邩�ۂ�
    public bool once = false;
    //��x�������s���ꂽ���̐����p�t���O
    bool isFirst = false;

    void OnTriggerEnter()
    {
        //once��true�̎��A�܂����s����ĂȂ����ۂ�
        if (!isFirst)
        {
            //�|�C���g�}�l�[�W���[���Ăяo��
            if (PointManager.Get())
            {
                //�|�C���g�}�l�[�W���[��AddPoint�֐��Ń|�C���g�������Z
                PointManager.Get().AddPoint(point);
                Debug.Log(PointManager.Get().GetPoint());
            }
        }
        if (once)
        {
            //once��true�̎��A��x���s���ꂽ��t���O��ύX
            isFirst = true;
        }
    }
}
