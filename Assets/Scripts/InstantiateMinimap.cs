using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InstantiateMinimap : MonoBehaviour
{

    void Start()
    {
        if (GameObject.Find("Room" + transform.name) != null)
        {
            SpriteRenderer roomSpriteRenderer = GameObject.Find("Room"+transform.name).GetComponent<SpriteRenderer>();
            Image minimapImage = GetComponent<Image>();
            minimapImage.enabled = true;
            minimapImage.sprite = roomSpriteRenderer.sprite;
            minimapImage.color = roomSpriteRenderer.color;
        }
    }
}
