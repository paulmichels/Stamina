using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New enemy", menuName = "Enemy")]
public class EnemyScriptableObject : ScriptableObject
{
    public new string name;
    public string description;
    public Sprite EnemySprite;
    public int Health;
    public int Attack;
    public int Armor;
    public Dungeon.Zone type;
    [Range(0,100)] public float Precision;
    [Range(0, 100)] public float CriticChance;
    [Range(0, 100)] public float StunResistance;
    [Range(0, 100)] public float BleedResistance;
}
