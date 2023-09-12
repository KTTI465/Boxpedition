using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GoalButton2 : MonoBehaviour
{
    public float Stagenumber;
    private void OnTriggerEnter(Collider other)
    {
     
        if (other.gameObject.tag == "Player")
        {
            if (Stagenumber == 0)
            {
                SceneManager.LoadScene("GoalScene0");
            }
            if (Stagenumber == 1)
            {
                SceneManager.LoadScene("GoalScene");
            }
            if (Stagenumber == 2)
            {
                SceneManager.LoadScene("GoalScene2");
            }
        }
    }
}
