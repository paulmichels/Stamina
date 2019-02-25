using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    public Vector2 speed = new Vector2(300, 100);

    private Vector2 movement;
    private Rigidbody2D rigidbodyComponent;

    public Button left, right;

    void Update()
    {
        if (left)
        {
            left.onClick.AddListener(MoveLeft);
        }
        if (right)
        {
            right.onClick.AddListener(MoveRight);
        }
    }

    void MoveLeft()
    {
        movement = new Vector2(speed.x * -1, 0);
    }

    void MoveRight()
    {
        movement = new Vector2(speed.x * 1, 0);
    }

    void FixedUpdate()
    {
        if (rigidbodyComponent == null) rigidbodyComponent = GetComponent<Rigidbody2D>();
        rigidbodyComponent.velocity = movement;
    }
}