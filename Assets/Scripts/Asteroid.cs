using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public Sprite graySprite;
    public Sprite brownSprite;

    // 3 is large, 2 is medium, 1 is small, 0 is tiny
    public int size = 3;
    public bool isGray = true;
    public bool fromSplit = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Randomizes color unless the asteroid split from another
        if (!fromSplit)
        {
            int roll = Random.Range(0, 2);
            if (roll == 0) {
                isGray = false;
            }
            else {
                isGray = true;
            }

            // Set object sprite
            SpriteRenderer sr = GetComponent<SpriteRenderer>();
            if (isGray) {
                sr.sprite = graySprite;
            }
            else {
                sr.sprite = brownSprite;
            }

            // Size handling
            float scale = size / 3f;
            transform.localScale = new Vector3(scale, scale, 1);

            // Random movement logic
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            float vx = Random.Range(-3f, 3f);
            float vy = Random.Range(-3f, 3f);
            rb.linearVelocity = new Vector2(vx, vy);
            rb.angularVelocity = Random.Range(-90f, 90f);
        }


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Hit()
    {
        if (size == 3) {
            //GameManager.instance.AddScore(20);
        }
        else if (size == 2) {
            //GameManager.instance.AddScore(50);
        }
        else {
            //GameManager.instance.AddScore(100);
        }

        // Split asteroid if it's not already the smallest size
        if (size > 1) {
            for (int i = 0; i < 2; i++)
            {
                GameObject child = Instantiate(gameObject, transform.position, Quaternion.identity);
                Asteroid splitAsteroid = child.GetComponent<Asteroid>();
                splitAsteroid.size = size - 1;
                splitAsteroid.isGray = isGray;
                splitAsteroid.fromSplit = true;
            }
        }

        Destroy(gameObject);
    }
}
