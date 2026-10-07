using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float velocity = 12f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = transform.up * velocity;
    }

    // Update is called once per frame
    void Update()
    {
        float limitX = 9.5f;
        float limitY = 5.5f;
        Vector2 pos = transform.position;
        if (pos.x > limitX || pos.x < -limitX || pos.y > limitY || pos.y < -limitY)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Asteroid"))
        {
            Asteroid asteroid = other.GetComponent<Asteroid>();
            asteroid.Hit();
            Destroy(gameObject);
        }
    }
}
