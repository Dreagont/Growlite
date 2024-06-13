using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : Singleton<PlayerController>
{
    public float moveSpeed = 5f;
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

    public bool canAction = true;

    public float damage = 20;

    public bool FacingLeft { get { return facingleft; } set { facingleft = value; } }
    public bool FacingUp { get { return facingup; } set { facingup = value; } }
    public bool FacingDown { get { return facindown; } set { facindown = value; } }


    private bool facingleft;

    private bool facingup;

    private bool facindown;



    protected override void Awake()
    {
        base.Awake();
        playerControls =  new PlayerControls();
        rb = GetComponent<Rigidbody2D>();
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
        FacingDown = false;
        FacingUp = false;
    }

    private void Update()
    {
        playerRoll();
        PlayerInput();
        playerPlow();
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


            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }

        
    }
    void playerPlow()
    {
        if (Input.GetMouseButton(0) && canAction)
        {
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int cellPosition = GameManager.Instance.TileManager.interactAbleTile.WorldToCell(mouseWorldPos);

            cellPosition.z = 0;

            Vector3 playerPosition = new Vector3Int((int)transform.position.x, (int)transform.position.y, 0);

            float distance = Vector3.Distance(playerPosition, cellPosition);

            if (distance <= 5f && GameManager.Instance.TileManager.IsInteractable(cellPosition))
            {
                GameManager.Instance.TileManager.SetInteracted(cellPosition);
            } else
            {
                Debug.Log("Position too far or not interactable");
            }
        }
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
}
