using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public Vector2 speed = new Vector2(400, 0);
    private Vector2 movement;
    private Rigidbody2D rigidbodyComponent;
    private SpriteRenderer spriteRenderer;

    public void GoLeft()
    {
        movement = new Vector2(-speed.x,speed.y);
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.flipY = true;
        transform.eulerAngles = new Vector3(0,0,180);
    }

    public void GoRight()
    {
        movement = new Vector2(speed.x, speed.y);
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.flipY = false;
        transform.eulerAngles = new Vector3(0, 0, 0);
    }

    public void Idle()
    {
        movement = new Vector2(0, 0);
    }

    void FixedUpdate()
    {
        if (rigidbodyComponent == null)
        {
            rigidbodyComponent = GetComponent<Rigidbody2D>();
        }
        rigidbodyComponent.velocity = movement;
    }
}
