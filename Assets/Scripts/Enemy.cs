using Unity.VisualScripting;
using UnityEditor.Tilemaps;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    [SerializeField] private float distance = 5f;
    private Vector3 startPos;
    private bool movingRight = true;
    public int maxHealth = 20;
    public int currentHealth;
    public Animator animator;
    void Start()
    {
        startPos = transform.position;
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        float leftBound = startPos.x - distance;
        float rightBound = startPos.x + distance;
        if (movingRight)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
            if (transform.position.x >= rightBound)
            {
                movingRight = false;
                Flip();
            }
        }

        else
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
            if (transform.position.x <= leftBound)
            {
                movingRight = true;
                Flip();
            }

        }

    }

    private void Flip()
    {
        Vector3 scaler= transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        animator.SetTrigger("Hurt");

        if (currentHealth <= 0) Die();
    }

    void Die()
    {
        animator.SetBool("isDead", true);
        GetComponent<BoxCollider2D>().enabled = false;
        this.enabled = false;
    }

    }
