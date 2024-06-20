using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthUI : MonoBehaviour
{
    public Image fillBar;
    public TextMeshProUGUI currentHealthText;

    public void updateBar(int currentHealth, int maxHealth)
    {
        fillBar.fillAmount = (float)currentHealth / (float)maxHealth;
        currentHealthText.text = currentHealth.ToString();
    }
}
