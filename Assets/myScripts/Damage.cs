using UnityEngine;

public class Damage : MonoBehaviour
{
    public float damage = 10f;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerHealth pHealth = other.gameObject.GetComponent<playerHealth>();

            if (pHealth != null)
            {
                pHealth.TakeDamage(damage);
            }
            else
            {
                Debug.LogError("Player does not have playerHealth script attached!");
            }
        }
    }
}