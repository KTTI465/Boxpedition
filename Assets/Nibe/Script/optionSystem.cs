using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class optionSystem : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            ReturnTitle();  //�^�C�g���ɖ߂�
        }
    }

    //�@�^�C�g���֖߂�{�^��������������s����
    public void ReturnTitle()
    {
        SceneManager.LoadScene("titleScene");
    }
}
