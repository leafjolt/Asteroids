using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour
{
    public float thrust = 6;
    public float turnSpeed = 200;
    public float maxSpeed = 2;
    public float fireCooldown = 0.25f;

    public GameObject bulletFab;

    float invincibilityTimer = 0f;
    SpriteRenderer renderer;

    Rigidbody2D rb;
    float fireTimer = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        renderer = GetComponent<SpriteRenderer>();
        Invincify();
    }

    // Update is called once per frame
    void Update()
    {
        if (invincibilityTimer > 0)
        {
            invincibilityTimer = invincibilityTimer - Time.deltaTime;
            // Make player green if invincible
            renderer.color = Color.green;
        }
        else
        {
            renderer.color = Color.white;
        }

        Keyboard keys = Keyboard.current;

        // Rotation logic
        if (keys.leftArrowKey.isPressed) {
            transform.Rotate(0, 0, turnSpeed * Time.deltaTime);
        }
        if (keys.rightArrowKey.isPressed) {
            transform.Rotate(0, 0, -turnSpeed * Time.deltaTime);
        }

        // Thrust logic
        if (keys.upArrowKey.isPressed) {
            rb.AddForce(transform.up * thrust);
        }

        // Limiting velocity
        if (rb.linearVelocity.magnitude > maxSpeed) {
            rb.linearVelocity = rb.linearVelocity.normalized * maxSpeed;
        }

        // Shooting bullets
        fireTimer = fireTimer - Time.deltaTime;
        if (keys.spaceKey.isPressed && fireTimer <= 0)
        {
            Instantiate(bulletFab, transform.position, transform.rotation);
            fireTimer = fireCooldown;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Asteroid") && invincibilityTimer <= 0)
        {
            if (other.GetComponent<Asteroid>().isLife)
            {
                GameManager.instance.AddLife();
                Destroy(other.gameObject);
            } 
            else
            {
                GameManager.instance.Death();
            }
            
        }
    }

    public void Invincify()
    {
        invincibilityTimer = 3;
    }
}
