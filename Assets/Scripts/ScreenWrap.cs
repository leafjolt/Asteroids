using UnityEngine;

public class ScreenWrap : MonoBehaviour
{
    // Screen limits with a 16:9 aspect ratio
    public float limitX = 9.5f;
    public float limitY = 5.5f;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 pos = transform.position;

        if (pos.x > limitX)
        {
            pos.x = -limitX;
        }
        if (pos.x < -limitX)
        {
            pos.x = limitX;
        }
        if (pos.y > limitY)
        {
            pos.y = -limitY;
        }
        if (pos.y < -limitY)
        {
            pos.y = limitY;
        }

        transform.position = pos;
    }
}
