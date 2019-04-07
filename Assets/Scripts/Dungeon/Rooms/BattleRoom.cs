using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleRoom : IRoom
{
    public Vector2Int Position { get; set; }
    public bool Explored { get; set; }
    public bool Completed { get; set; }

    public BattleRoom(Vector2Int position)
    {
        Position = position;
    }
}
