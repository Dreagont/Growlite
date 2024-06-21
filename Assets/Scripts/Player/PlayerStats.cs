using System.Collections;
using TMPro;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    private Player player;
    private int maxHealth;
    [SerializeField] private int currentHealth;
    public GameObject damageText;
    public TMP_Text popupText;

    private bool isImmune = false;
    public float immunityDuration = 0.5f;

    public PlayerHealthUI healthUI;

    void Start()
    {
        player = GetComponent<Player>();

        if (healthUI == null)
        {
            healthUI = GetComponent<PlayerHealthUI>();
        }

        if (healthUI == null)
        {
            Debug.LogError("PlayerHealthUI component is not assigned or found!");
            return;
        }

        maxHealth = player.Health;
        currentHealth = maxHealth;
        healthUI.updateBar(currentHealth, maxHealth);
    }

    void Update()
    {
        maxHealth = player.Health;
        isDieCheck();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        EnemyAI enemy = collision.gameObject.GetComponent<EnemyAI>();
       

        if (enemy != null)
        {
            TakeDamage(20);
        }
    }

    public void TakeDamage(int damage)
    {
        if (isImmune) return;

        RectTransform textTramform = Instantiate(damageText).GetComponent<RectTransform>();
        textTramform.transform.position = Camera.main.WorldToScreenPoint(gameObject.transform.position);

        popupText.text = damage.ToString();

        Canvas canvas = GameObject.FindObjectOfType<Canvas>();
        textTramform.SetParent(canvas.transform);

        currentHealth -= damage;
        healthUI.updateBar(currentHealth, maxHealth); 

        StartCoroutine(ImmuneCoroutine());
    }

    private IEnumerator ImmuneCoroutine()
    {
        isImmune = true;
        yield return new WaitForSeconds(immunityDuration);
        isImmune = false;
    }

    public void isDieCheck()
    {
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
