using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Netflex : IEnemy
{
    public string EnemyName { get; set; }
    public int Health { get; set; }
    public int Attack { get; set; }
    public Dungeon.Zone Type { get; set; }
    public Sprite EnemySprite { get; set; }

    public Netflex()
    {
        EnemyName = "Netflex";
        Health = 70;
        Attack = 5;
        Type = Dungeon.Zone.Algorithmique;
        EnemySprite = Resources.Load<Sprite>("Assets/Sprites/Items/Banner_003.png");
    }
}