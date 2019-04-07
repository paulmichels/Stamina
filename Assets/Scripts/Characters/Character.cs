using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : IStatusEffect, IStatusAbility, ICharacterStatistics
{
    public int Health { get; set; }
    public int HealthMax { get; set; }
    public int Level { get; set; }
    public int Armor { get; set; }
    public int ArmorMax { get; set; }

    public void Bleed()
    {
        throw new System.NotImplementedException();
    }

    public void Bleeding()
    {
        throw new System.NotImplementedException();
    }

    public void ShieldBreak()
    {
        throw new System.NotImplementedException();
    }

    public void ShieldBreaked()
    {
        throw new System.NotImplementedException();
    }

    public void Stun()
    {
        throw new System.NotImplementedException();
    }

    public void Stuned()
    {
        throw new System.NotImplementedException();
    }
}