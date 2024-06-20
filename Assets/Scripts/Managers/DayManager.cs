    using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class DayManager : MonoBehaviour
{
    const float daySecond = 86400f;

    [SerializeField] Color dayColor = Color.white;
    [SerializeField] Color nightColor;
    [SerializeField] AnimationCurve DayTimeCurve;
    [SerializeField] TextMeshProUGUI DayText;
    [SerializeField] int TimeScale = 288;
    [SerializeField] Light2D globalLight;

    public int dayCount = 0;

    float dayTime;

    float Hours
    {
        get
        {
            return dayTime / 3600f;
        }
    }

    private void Update()
    {
        dayTime += Time.deltaTime * TimeScale;
        DayText.text = Hours.ToString();

        float v = DayTimeCurve.Evaluate(Hours);
        Color c = Color.Lerp(dayColor, nightColor, v);

        globalLight.color = c;

        if (dayTime > daySecond)
        {
            dayTime = 0;
            dayCount += 1;
        }
    }
}
