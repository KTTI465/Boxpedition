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
        //触れたオブジェクトがリストに含まれていない時
        if (!_interactGameObjectsList.Contains(other.gameObject))
        {
            //触れたオブジェクトをリストに追加
            _interactGameObjectsList.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //離れたオブジェクトがリストの中に含まれている時
        if (_interactGameObjectsList.Contains(other.gameObject))
        {
            //離れたオブジェクトをリストから削除
            _interactGameObjectsList.Remove(other.gameObject);
        }
    }
}
