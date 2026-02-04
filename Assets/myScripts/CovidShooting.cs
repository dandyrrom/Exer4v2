using UnityEngine;

public class CovidShooting : MonoBehaviour
{
    public GameObject bullet;
    public GameObject player;
    public Transform bulletPos;

    private float timer;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (player == null)
            return;

        timer += Time.deltaTime;

        float distance = Vector2.Distance(transform.position, player.transform.position);

        if (distance < 3f)
        {
            if (timer > 2f)
            {
                timer = 0f;
                Shoot();
            }
        }
    }

    void Shoot()
    {
        Instantiate(bullet, bulletPos.position, Quaternion.identity);
    }
}
