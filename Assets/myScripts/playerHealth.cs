using UnityEngine;
using System.Collections;

public class playerHealth : MonoBehaviour
{
    public float health = 100f;
    public float hitFlashDuration = 0.1f;
    public float flickerInterval = 0.15f;
    public float deathFlickerDuration = 1.5f;

    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private bool isDead = false;
    private Color originalColor;

    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            originalColor = spriteRenderer.color;
        }
    }

    void Update()
    {
        if (!isDead && health <= 0)
        {
            Die();
        }
    }

    // Public method for taking damage with flash effect
    public void TakeDamage(float damage)
    {
        if (isDead) return;

        health -= damage;

        // Flash white when hit
        if (spriteRenderer != null)
        {
            StartCoroutine(FlashWhite());
        }

        // Check if dead after taking damage
        if (!isDead && health <= 0)
        {
            Die();
        }
    }

    IEnumerator FlashWhite()
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.color = Color.white;
            yield return new WaitForSeconds(hitFlashDuration);
            spriteRenderer.color = originalColor;
        }
    }

    IEnumerator DeathFlicker()
    {
        float timer = 0f;
        bool visible = true;

        while (timer < deathFlickerDuration)
        {
            if (spriteRenderer != null)
            {
                spriteRenderer.enabled = visible;
                visible = !visible;
            }

            yield return new WaitForSeconds(flickerInterval);
            timer += flickerInterval;
        }

        // Make sprite visible one last time
        if (spriteRenderer != null)
        {
            spriteRenderer.enabled = true;
        }
    }

    public void Die()
    {
        if (isDead) return;

        isDead = true;

        // Trigger death animation
        if (animator != null)
        {
            animator.SetTrigger("death");
        }

        // Start flicker effect
        StartCoroutine(DeathFlicker());

        // Disable player movement/controls
        PlayerMovement movement = GetComponent<PlayerMovement>();
        if (movement != null) movement.enabled = false;

        // Disable collider
        Collider2D collider = GetComponent<Collider2D>();
        if (collider != null) collider.enabled = false;

        // Disable Rigidbody2D physics (Unity 6 compatible)
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.gravityScale = 0f;
            rb.bodyType = RigidbodyType2D.Static; // Changed from isKinematic to bodyType
        }

        Destroy(gameObject, 2f);
    }
}