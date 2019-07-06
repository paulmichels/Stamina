using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DixHuit : IEnemy
{
    public string EnemyName { get; set; }
    public int Health { get; set; }
    public int Attack { get; set; }
    public Dungeon.Zone Type { get; set; }
    public Sprite EnemySprite { get; set; }
    
    public DixHuit(EnemySpriteSelector enemySpriteSelector)
    {
        EnemyName = "DixHuit";
        Health = 100;
        Attack = 20;
        Type = Dungeon.Zone.Algorithmique;
        EnemySprite = enemySpriteSelector.DixHuit;
    }
}