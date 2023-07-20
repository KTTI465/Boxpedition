using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class detectionBuildingBlockArea : MonoBehaviour
{
    [SerializeField]　//積み木
    private GameObject block;

    private buildingBlock buildingBlock;

    [NonSerialized]　//積み木が上に置かれているかの判定
    public bool onBlock;

    private void Start()
    {
        buildingBlock = block.GetComponent<buildingBlock>();
        onBlock = false;
    }

    private void OnTriggerStay(Collider other)
    {
        //棚の上に積み木が置かれたとき
        if(other.gameObject == block)
        {
            if (buildingBlock.isGrabed == false)
            {
                onBlock = true;
                Debug.Log("adjfehafiouehfuieahfbeifb");
            }
            else
            {
                onBlock = false;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == block)
        {
            onBlock = false;
        }
    }

    //[SerializeField]
    //private List<GameObject> blockList;
    //
    //[NonEditable]
    //public List<GameObject> blockOnshelfList;
    //
    //private void OnTriggerEnter(Collider other)
    //{
    //    if (blockList.Contains(other.gameObject))
    //    {
    //        blockList.Add(other.gameObject);
    //    }
    //}
    //
    //private void OnTriggerExit(Collider other)
    //{
    //    if (blockList.Contains(other.gameObject))
    //    {
    //        blockList.Remove(other.gameObject);
    //    }
    //}
}


