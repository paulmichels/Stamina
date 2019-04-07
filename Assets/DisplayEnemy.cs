using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayEnemy : MonoBehaviour
{
    public EnemyScriptableObject enemy;

    void Start()
    {
        SpriteRenderer sr = gameObject.GetComponent<SpriteRenderer>();
        sr.sprite = enemy.EnemySprite;
    }

}
