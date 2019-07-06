using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleRoom : IRoom
{
    public GameObject position1, position2, position3, position4;
    public Vector2Int Position { get; set; }
    public bool Explored { get; set; }
    public bool Completed { get; set; }
    private List<IEnemy> enemies;

    public BattleRoom(Vector2Int position)
    {
        Position = position;
    }

    public void TriggerEvent()
    {
        position1 = GameObject.Find("Position1");
        position2 = GameObject.Find("Position2");
        position3 = GameObject.Find("Position3");
        position4 = GameObject.Find("Position4");

        enemies = SpawnGroup.CreateGroup();

        Debug.Log(enemies[0].EnemyName + " " + (enemies[1] != null ? "" + enemies[1].EnemyName : "") + " " + (enemies[2] != null ? "" + enemies[2].EnemyName : "") + " " + (enemies[3] != null ? "" + enemies[3].EnemyName : ""));

        SpriteRenderer p1sp = position1.AddComponent<SpriteRenderer>();
        p1sp.sprite = enemies[0].EnemySprite;
        p1sp.sortingLayerName = "Middleground";
        if (enemies[1] != null)
        {
            SpriteRenderer p2sp = position2.AddComponent<SpriteRenderer>();
            p2sp.sprite = enemies[1].EnemySprite;
            p2sp.sortingLayerName = "Middleground";
        }
        if (enemies[2] != null)
        {
            SpriteRenderer p3sp = position3.AddComponent<SpriteRenderer>();
            p3sp.sprite = enemies[2].EnemySprite;
            p3sp.sortingLayerName = "Middleground";
        }
        if (enemies[3] != null)
        {
            SpriteRenderer p4sp = position4.AddComponent<SpriteRenderer>();
            p4sp.sprite = enemies[3].EnemySprite;
            p4sp.sortingLayerName = "Middleground";
        }
    }
}