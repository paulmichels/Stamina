using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureParcel : IParcel
{
    public Vector3Int Position { get ; set ; }
    public bool Explored { get; set; }
    public bool Completed { get; set; }

    public TreasureParcel(Vector3Int position)
    {
        Position = position;
    }
}
