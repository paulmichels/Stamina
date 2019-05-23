﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sarkasme : IEnemy
{
    public string EnemyName { get; set; }
    public int Health { get; set; }
    public int Attack { get; set; }
    public Dungeon.Zone Type { get; set; }
    public Sprite EnemySprite { get; set; }

    public Sarkasme()
    {
        EnemyName = "Sarkasme";
        Health = 150;
        Attack = 10;
        Type = Dungeon.Zone.Algorithmique;
        EnemySprite = Resources.Load<Sprite>("Assets/Sprites/Items/Banner_001.png");
    }
}