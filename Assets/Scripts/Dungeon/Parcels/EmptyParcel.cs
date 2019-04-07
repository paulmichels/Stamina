using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyParcel : IParcel
{
    public Vector3Int Position { get ; set ; }
    public bool Explored { get; set; }
    public bool Completed { get; set; }

    public EmptyParcel(Vector3Int position)
    {
        Position = position;
    }
}
