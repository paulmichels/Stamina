﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quadrature : IEnemy
{
    public string EnemyName { get; set; }
    public int Health { get; set; }
    public int Attack { get; set; }
    public Dungeon.Zone Type { get; set; }
    public Sprite EnemySprite { get; set; }
    
    public Quadrature(EnemySpriteSelector enemySpriteSelector)
    {
        EnemyName = "Quadrature";
        Health = 100;
        Attack = 20;
        Type = Dungeon.Zone.Algorithmique;
        EnemySprite = enemySpriteSelector.Quadrature;
    }
}