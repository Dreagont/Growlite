using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    [SerializeField] private GameObject slashAnimPrefabRight;
    [SerializeField] private GameObject slashAnimPrefabLeft;

    [SerializeField] private Transform slashAnimSpawnPoint;
    [SerializeField] private Transform weaponCollider;
    [SerializeField] private Transform weaponColliderVerical;

    private PlayerControls playerControls;
    private Animator myAnimator;
    private PlayerController playerController;
    private ActiveWeapon activeWeapon;

    private GameObject slashAnim;

    // Variables for attack speed
    [SerializeField] private float attackSpeed = 1.0f; // Attacks per second
    private float attackCooldown;

    private Coroutine attackCoroutine;

    private void Awake()
    {
        playerController = GetComponentInParent<PlayerController>();
        activeWeapon = GetComponentInParent<ActiveWeapon>();
        myAnimator = GetComponent<Animator>();
        playerControls = new PlayerControls();
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    void Start()
    {
        playerControls.Combat.Attack.started += _ => StartAttacking();
        playerControls.Combat.Attack.canceled += _ => StopAttacking();
        attackCooldown = 0f;

        
    }

    private void Update()
    {
        MouseFollowWithOffset();

        // Decrease the attack cooldown over time
        if (attackCooldown > 0)
        {
            attackCooldown -= Time.deltaTime;
        }
    }

    private void StartAttacking()
    {
        if (attackCoroutine == null)
        {
            attackCoroutine = StartCoroutine(AttackRoutine());
        }
    }

    private void StopAttacking()
    {
        if (attackCoroutine != null)
        {
            StopCoroutine(attackCoroutine);
            attackCoroutine = null;
        }
    }

    private IEnumerator AttackRoutine()
    {
        while (true)
        {
            TryAttack();
            yield return null;
        }
    }

    private void TryAttack()
    {
        if (attackCooldown <= 0)
        {
            Attack();
            attackCooldown = 1f / attackSpeed; // Reset cooldown based on attack speed
        }
    }

    private void Attack()
    {

        

            myAnimator.SetTrigger("Attack");
            weaponCollider.gameObject.SetActive(true);
            if (playerController.FacingLeft)
            {
                slashAnim = Instantiate(slashAnimPrefabLeft, slashAnimSpawnPoint.position, slashAnimPrefabLeft.transform.rotation);
            }
            else
            {
                slashAnim = Instantiate(slashAnimPrefabRight, slashAnimSpawnPoint.position, slashAnimPrefabRight.transform.rotation);
            }


            slashAnim.transform.parent = this.transform.parent;
        

       
    }

    public void DoneAttackingAnimation()
    {
        weaponCollider.gameObject.SetActive(false);
        weaponColliderVerical.gameObject.SetActive(false);

    }

    public void SwingUpFlipAnim()
    {
        slashAnim.gameObject.transform.rotation = Quaternion.Euler(-180, 0, 0);

        if (playerController.FacingLeft)
        {
            slashAnim.GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    public void SwingDownFlipAnim()
    {
        slashAnim.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);

        if (playerController.FacingLeft)
        {
            slashAnim.GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    private void MouseFollowWithOffset()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(playerController.transform.position);

        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;

        if (mousePos.x < playerScreenPoint.x)
        {
            activeWeapon.transform.rotation = Quaternion.Euler(0, -180, 0);
            weaponCollider.transform.rotation = Quaternion.Euler(0, -180, 0);
        }
        else
        {
            activeWeapon.transform.rotation = Quaternion.Euler(0, 0, 0);
            weaponCollider.transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        
    }
}
