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
    private void Update()
    {
        MoveScrollView();
    }

    public void MoveScrollView()
    {

        if (Input.GetAxisRaw("Vertical") > 0)
        {
            if (ScrollBar.value <= 1.0)
            {
                ScrollBar.value = ScrollBar.value + 0.001f;
            }
        }
        if (Input.GetAxisRaw("Vertical") < 0)
        {
            if (ScrollBar.value >= 0.0)
            {
                ScrollBar.value = ScrollBar.value - 0.001f;
            }
        }

    }
}
