using UnityEngine;
using UnityEngine.UI;

public class offScreenGuideArrow : MonoBehaviour
{
    [SerializeField]
    private Transform target;
    [SerializeField]
    private Image arrow;
    [SerializeField]
    private Camera playerCamera;
    [SerializeField]
    private GameObject optionPanel;
    private bool openOption;

    private RectTransform rectTransform;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        if (optionPanel.activeSelf == false)
        {
            openOption = false;
        }
        else if (optionPanel.activeSelf == true)
        {
            openOption = true;
        }

        if(openOption == false)
        {
            arrow.enabled = true;
        }
        else
        {
            arrow.enabled = false;
        }
    }

    private void LateUpdate()
    {
        if (openOption == false)
        {
            float canvasScale = transform.root.localScale.z;
            var center = 0.5f * new Vector3(Screen.width, Screen.height);

            var pos = playerCamera.WorldToScreenPoint(target.position) - center;
            if (pos.z < 0f)
            {
                pos.x = -pos.x;
                pos.y = -pos.y;

                if (Mathf.Approximately(pos.y, 0f))
                {
                    pos.y = -center.y;
                }
            }

            var halfSize = 0.5f * canvasScale * (rectTransform.sizeDelta);
            float d = Mathf.Max(
                Mathf.Abs(pos.x / (center.x - halfSize.x)),
                Mathf.Abs(pos.y / (center.y - halfSize.y))
            );

            bool isOffscreen = (pos.z < 0f || d > 1.2f);
            if (isOffscreen)
            {
                pos.x /= d;
                pos.y /= d;
            }
            rectTransform.anchoredPosition = pos / canvasScale;

            arrow.enabled = isOffscreen;
            if (isOffscreen)
            {
                arrow.rectTransform.eulerAngles = new Vector3(
                    0f, 0f,
                    Mathf.Atan2(pos.y, pos.x) * Mathf.Rad2Deg
                );
            }
        }
    }
}
