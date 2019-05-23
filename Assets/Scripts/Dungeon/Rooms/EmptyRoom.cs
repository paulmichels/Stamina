using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyRoom : IRoom
{
    public Vector2Int Position { get; set; }
    public bool Explored { get; set; }
    public bool Completed { get; set; }

    public EmptyRoom(Vector2Int position)
    {
        Position = position;
    }

    public void TriggerEvent()
    {
        //Rien
    }
}
