using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapRoom : IRoom
{
    public Vector2Int Position { get; set; }
    public bool Explored { get; set; }
    public bool Completed { get; set; }

    public TrapRoom(Vector2Int position)
    {
        Position = position;
    }
}
