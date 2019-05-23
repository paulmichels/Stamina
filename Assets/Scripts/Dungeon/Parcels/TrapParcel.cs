using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapParcel : IParcel
{
    public Vector3Int Position { get ; set ; }
    public bool Explored { get; set; }
    public bool Completed { get; set; }

    public TrapParcel(Vector3Int position)
    {
        Position = position;
    }

    public void TriggerEvent()
    {
        throw new System.NotImplementedException();
    }
}
