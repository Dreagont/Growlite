using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    [SerializeField] public int CurrentGold;
    [SerializeField] List<TextMeshProUGUI>  textMeshProUGUI = new List<TextMeshProUGUI>();
    void Start()
    {
    }

    void Update()
    {
        foreach (var item in textMeshProUGUI)
        {
            item.text = CurrentGold.ToString();
        }
    }
}
