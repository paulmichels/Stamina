﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fhlaime : IEnemy
{
    public string EnemyName { get; set; }
    public int Health { get; set; }
    public int Attack { get; set; }
    public Dungeon.Zone Type { get; set; }
    public Sprite EnemySprite { get; set; }
    
    public Fhlaime(EnemySpriteSelector enemySpriteSelector)
    {
        EnemyName = "Fhlaime";
        Health = 70;
        Attack = 20;
        Type = Dungeon.Zone.Algorithmique;
        EnemySprite = enemySpriteSelector.Fhlaime;
    }
}
