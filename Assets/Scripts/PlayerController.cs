using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 15f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundCheck;
    private Animator animator;
    private bool isGrounded;
    private Rigidbody2D rb;
    public GameManager gm;
    public Transform attackpoint;
    public float attackrange = 0.2f;
    public LayerMask Enemies;
    public int attackDamage = 2;
    public float attackrate = 2f;
    float nextattacktime = 0f;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }
    void Start()
    {

    }

    void Update()
    {
        if (gm.IsGameOver() || (gm.IsGameWin())) return;
        HandleMovement();
        HandleJump();
        UpdateAnimation();
        HandleAttack();
    }

    private void HandleMovement()
    {
        float moveInput = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);
        if (moveInput > 0) transform.localScale = new Vector3(1, 1, 1);
        else if (moveInput < 0) transform.localScale = new Vector3(-1, 1, 1);
    }

    private void HandleJump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void UpdateAnimation()
    {
        bool isRunning = Mathf.Abs(rb.linearVelocity.x) > 0.1;
        bool isJumping = !isGrounded;
        animator.SetBool("isRunning", isRunning);
        animator.SetBool("isJumping", isJumping);
    }

    private void HandleAttack()
    {
        if (Time.time >= nextattacktime)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                //  isAttacking = true;
                animator.SetTrigger("Attack");
                Collider2D[] EnemiesHit = Physics2D.OverlapCircleAll(attackpoint.position, attackrange, Enemies);

                foreach (Collider2D enemy in EnemiesHit)
                {
                    enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
                }
                nextattacktime = Time.time + 1/attackrate;
            }
        }
        
    }

    public void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackpoint.position, attackrange);
    }

}
