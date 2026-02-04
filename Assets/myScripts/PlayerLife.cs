using UnityEngine;

public class PlayerLife : MonoBehaviour
{
    private playerHealth health;

    private void Start()
    {
        health = GetComponent<playerHealth>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Trap"))
        {
            if (health != null)
            {
                health.Die(); // Call the Die method directly
            }
        }
    }
}