using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ToolTipUI : MonoBehaviour
{
    [SerializeField] private TMP_Text tooltipText;
    [SerializeField] private RectTransform backgroundRectTransform;

    void Start()
    {
        gameObject.SetActive(false); 
    }

    public void ShowTooltip(string skillInfo)
    {
        gameObject.SetActive(true);
        tooltipText.text = skillInfo;

        // Force TextMeshPro cập nhật layout trước khi lấy preferred size
        Canvas.ForceUpdateCanvases();
        LayoutRebuilder.ForceRebuildLayoutImmediate(tooltipText.rectTransform);

        Vector2 backgroundSize = new Vector2(
            tooltipText.preferredWidth + 8f,
            tooltipText.preferredHeight + 8f
        );
        backgroundRectTransform.sizeDelta = backgroundSize;
    }


    public void HideTooltip()
    {
        gameObject.SetActive(false);
    }

    void Update()
    {
        Vector2 mousePosition = Input.mousePosition;
        transform.position = mousePosition; 
    }
}
