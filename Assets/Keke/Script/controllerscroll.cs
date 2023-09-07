using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class controllerscroll : MonoBehaviour
{
    // Start is called before the first frame update
    // Update is called once per frame
    public Scrollbar ScrollBar;

    public void MoveScrollView()
    {
        if (Input.GetAxisRaw("Vertical") >0)
        {
            ScrollBar.value = ScrollBar.value + 0.01f;
        }
        if (Input.GetAxisRaw("Vertical") < 0)
        {
            ScrollBar.value = ScrollBar.value - 0.01f;
        }
    }
}
