using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class interactArea : MonoBehaviour
{
    private GameObject _player;

    private List<GameObject> _interactGameObjectsList;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _interactGameObjectsList = _player.GetComponent<CharacterController>().InteractGameObjectsList;
    }


    private void OnTriggerEnter(Collider other)
    {
        //�G�ꂽ�I�u�W�F�N�g�����X�g�Ɋ܂܂�Ă��Ȃ���
        if (!_interactGameObjectsList.Contains(other.gameObject))
        {
            //�G�ꂽ�I�u�W�F�N�g�����X�g�ɒǉ�
            _interactGameObjectsList.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //���ꂽ�I�u�W�F�N�g�����X�g�̒��Ɋ܂܂�Ă��鎞
        if (_interactGameObjectsList.Contains(other.gameObject))
        {
            //���ꂽ�I�u�W�F�N�g�����X�g����폜
            _interactGameObjectsList.Remove(other.gameObject);
        }
    }
}
