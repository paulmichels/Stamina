using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleParcel : IParcel
{
    public Vector3Int Position { get ; set ; }
    public bool Explored { get; set; }
    public bool Completed { get; set; }

    public BattleParcel(Vector3Int position)
    {
        Position = position;
    }
}
