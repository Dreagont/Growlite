using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    private Player player;
    private int maxHealth;
    private int currentHealth;
    public GameObject damageText;
    public TMP_Text popupText;

    private bool isImmune = false;
    public float immunityDuration = 0.5f; 

    void Start()
    {
        player = GetComponent<Player>();
        maxHealth = player.Health;
        currentHealth = maxHealth;
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

        Canvas canvas = GameObject.FindAnyObjectByType<Canvas>();
        textTramform.SetParent(canvas.transform);

        currentHealth -= damage;

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
