using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamageAbleEnemy : MonoBehaviour
{
    [SerializeField] private float MAX_HEALTH = 100;
    [SerializeField] private float health;

    private Knockback knockback;
    private Flash flash;

    public GameObject damageText;
    public TMP_Text popupText;

    private void Awake()
    {
        flash = GetComponent<Flash>();
        knockback = GetComponent<Knockback>();
    }
    private void Start()
    {
        health = MAX_HEALTH;
    }

    public void TakeDamage(float damage)
    {
        RectTransform textTramform = Instantiate(damageText).GetComponent<RectTransform>();
        textTramform.transform.position = Camera.main.WorldToScreenPoint(gameObject.transform.position);

        popupText.text = damage.ToString();

        Canvas canvas = GameObject.FindAnyObjectByType<Canvas>();
        textTramform.SetParent(canvas.transform);

        health -= damage;
        knockback.GetKnockedBack(PlayerController.Instance.transform, 15f);
        StartCoroutine(flash.FlashRoutine());
        
       
    }

    public void isDieCheck()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
 }
