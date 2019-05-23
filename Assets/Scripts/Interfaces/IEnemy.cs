using UnityEngine;

public interface IEnemy
{
    string EnemyName { get; set; }
    int Health { get; set; }
    int Attack { get; set; }
    Dungeon.Zone Type { get; set; }
    Sprite EnemySprite { get; set; }
}