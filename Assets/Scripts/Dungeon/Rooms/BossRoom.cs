using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoom : IRoom
{
    public Vector2Int Position { get; set; }
    public bool Explored { get; set; }
    public bool Completed { get; set; }

    public BossRoom(Vector2Int position)
    {
        Position = position;
    }

    public void TriggerEvent()
    {
        throw new System.NotImplementedException();
    }
}
