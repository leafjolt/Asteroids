using UnityEngine;
using UnityEngine.InputSystem;

public class Spaceship : MonoBehaviour
{
    public float thrust = 6;
    public float turnSpeed = 200;
    public float maxSpeed = 5;
    public float fireCooldown = 0.25f;

    public GameObject bulletFab;
    public Transform firePoint;

    Rigidbody2D rb;
    float fireTimer = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
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
            Instantiate(bulletFab, firePoint.position, transform.rotation);
            fireTimer = fireCooldown;
        }
    }
}
