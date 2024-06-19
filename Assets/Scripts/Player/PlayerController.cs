using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : Singleton<PlayerController>
{
    public float moveSpeed;

    public float rollSpeed = 10f;  

    private Rigidbody2D rb;

    public Vector3 moveInput;

    private Animator animator;

    private float rollTime;

    public float rollDuration;

    bool isRolling = false;

    public GameObject attackArea;

    private Vector2 movement;

    private PlayerControls playerControls;

    TrailRenderer trailRenderer;

    private TileManager tileManager;

    public bool canAction = true;

    private float attackTime;

    public float attackDuration;

    bool isAttacking = false;

    private AttackArea attackAreaScript;

    private Vector3Int cellPosition;

    public InventoryManager inventory;

    private Player player;

    public bool FacingLeft { get { return facingleft; } set { facingleft = value; } }

    private bool facingleft;

    protected override void Awake()
    {
        base.Awake();
        playerControls =  new PlayerControls();
        rb = GetComponent<Rigidbody2D>();
        inventory = GetComponent<InventoryManager>();
        DontDestroyOnLoad(gameObject);
    }

    private void OnEnable()
    {
        playerControls.Enable();

    }

    private void Start()
    {
        trailRenderer = GetComponent<TrailRenderer>();
        animator = GetComponent<Animator>();
        tileManager = GameManager.Instance.TileManager;
        attackAreaScript = attackArea.GetComponent<AttackArea>();
        attackArea.SetActive(false);
        player = GetComponent<Player>();

    }
    void attack()
    {
        if (Input.GetMouseButton(0) && canAction && GameManager.Instance.player.inventory.toolBar.selectedSlot.itemName == "Sword")
        {
            if (!isAttacking)
            {
                isAttacking = true;
                attackTime = attackDuration;
                animator.SetTrigger("Attack");
                
                
            }
            else
            {
                attackTime -= Time.deltaTime;
                if (attackTime <= 0)
                {
                    isAttacking = false;
                    attackArea.SetActive(false);
                }
            }
        }
        else
        {
            isAttacking = false;
            attackArea.SetActive(false);
        }
    }
    public void activeAttack()
    {
        attackArea.SetActive(true);
    }

    private void Update()
    {
        playerRoll();
        PlayerInput();
        playerPlow();
        attack();

        moveSpeed = player.movementSpeed;

    }

    private void FixedUpdate()
    {
        Move();
    }

    private void PlayerInput()
    {
        movement = playerControls.Movement.Move.ReadValue<Vector2>();
        
    }

    void Move()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(transform.position);

        if (mousePos.x < playerScreenPoint.x)
        {
            FacingLeft = true;
        } else
        {
            FacingLeft = false;
        }
        

        rb.MovePosition(rb.position + movement * (moveSpeed * Time.deltaTime));
        if (movement.x != 0 || movement.y != 0)
        {
            if (movement.y != 0)
            {
                animator.SetFloat("X", movement.x);

                animator.SetFloat("Y", movement.y);

            } else
            {

                if (FacingLeft)
                {
                    animator.SetFloat("Y", movement.y);
                    animator.SetFloat("X", -1);
                }
                else
                {
                    animator.SetFloat("Y", movement.y);
                    animator.SetFloat("X", 1);

                }
            }

            if (movement.x > 0)
            {
                attackAreaScript.SetFacingDirection(1);
            }
            else if (movement.x < 0)
            {
                attackAreaScript.SetFacingDirection(2);
            }
            else if (movement.y > 0)
            {
                attackAreaScript.SetFacingDirection(3);
            }
            else if (movement.y < 0)
            {
                attackAreaScript.SetFacingDirection(4);
            }




            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }

        
    }
    void playerPlow()
    {
        if (Input.GetMouseButton(0) && canAction && GameManager.Instance.player.inventory.toolBar.selectedSlot.itemName == "Copper Hoe")
        {
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            cellPosition = tileManager.HightLightTile.WorldToCell(mouseWorldPos);
            cellPosition.z = 0;

            Vector3Int playerCellPosition = tileManager.HightLightTile.WorldToCell(transform.position);
            playerCellPosition.z = 0;

            float distance = Vector3Int.Distance(playerCellPosition, cellPosition);

            if (distance <= 3f && GameManager.Instance.TileManager.IsInteractable(cellPosition))
            {
                animator.SetTrigger("Plow");
            }
            else
            {
                Debug.Log("Position too far or not interactable");
            }

        }
    }

    public void Plow()
    {
        GameManager.Instance.TileManager.SetInteracted(cellPosition);
    }
    void playerRoll()
    {
        if (rollTime > 0)
        {
            rollTime -= Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Space) && rollTime <= 0)
        {
            rollTime = rollDuration;
            moveSpeed = rollSpeed;
            isRolling = true;
            trailRenderer.emitting = true;
            StartCoroutine(stopDashing());

        }

        if (rollTime <= 0 && isRolling)
        {
            moveSpeed = 5f;
            isRolling = false;
        }
    }

    private IEnumerator stopDashing()
    {
        yield return new WaitForSeconds(rollDuration);
        trailRenderer.emitting = false;
    }

    public void OnAttackEnd()
    {
        isAttacking = false;
        attackArea.SetActive(false);
    }

    public void DropItem(Item item)
    {
        Vector2 dropLocation = transform.position;

        Vector2 spawnOffset = UnityEngine.Random.insideUnitCircle * 1.5f;

        Instantiate(item, dropLocation + spawnOffset, Quaternion.identity);
    }

    public void DropItem(Item item, int quantity)
    {
        for (int i = 0; i < quantity; i++)
        {
            DropItem(item);
        }
    }

}
