using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character, IPlayerStatistics
{
    public int Experience { get; set; }
    public int Mana { get; set; }
    public int ManaMax { get; set; }

    public Player()
    {

    }
}
