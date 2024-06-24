using TMPro;
using UnityEngine;

public class TooltipManager : MonoBehaviour
{
    public static TooltipManager instance;

    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private RectTransform tooltipRectTransform;
    [SerializeField] private Canvas canvas;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    void Start()
    {
        Cursor.visible = true;
        gameObject.SetActive(false);
    }

    void Update()
    {
        FollowCursor();
    }

    private void FollowCursor()
    {
        Vector2 position;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, Input.mousePosition, canvas.worldCamera, out position);
        tooltipRectTransform.localPosition = position;
    }

    public void ShowTooltip(string text)
    {
        gameObject.SetActive(true);
        itemName.text = text;
    }

    public void HideTooltip()
    {
        gameObject.SetActive(false);
        itemName.text = string.Empty;
    }
}
