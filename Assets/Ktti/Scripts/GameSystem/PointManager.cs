using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointManager : MonoBehaviour
{
    //�|�C���g�i�[�p�ϐ�
    [SerializeField, NonEditable]
    int score = 0;
    
    //�Ăяo���p�ϐ�
    static PointManager _manage;

    //�O������̌Ăяo���p
    public static PointManager Get()
    {
        if (_manage == null)
        {
            _manage = FindObjectOfType<PointManager>();
            if (_manage == null)
            {
                return null;
            }
        }
        return _manage;
    }

    //�|�C���g�̃Z�b�^�[
    public void AddPoint(int index)
    {
        score += index;
    }

    //�|�C���g�̃Q�b�^�[
    public int GetPoint()
    {
        return score;
    }
}
