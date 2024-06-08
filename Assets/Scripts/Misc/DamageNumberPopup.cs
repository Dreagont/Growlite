using TMPro;
using UnityEngine;

public class DamageNumberPopup : MonoBehaviour
{
    public float duration = 2f;
    public TextMeshProUGUI TextMesh;
    public float floatSpeed = 10;

    RectTransform rectTransform;
    float timeElaf = 0f;
    Vector3 floatPotion = new Vector3(0,1,0);
    Color startColor;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        TextMesh = GetComponent<TextMeshProUGUI>();
    }
    void Update()
    {
        timeElaf += Time.deltaTime;

        rectTransform.position += floatPotion * floatSpeed * Time.deltaTime;


        if(timeElaf > duration)
        {
            Destroy(gameObject);
        }
    }
}
