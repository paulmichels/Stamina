using UnityEngine;

public interface IRoom
{
    Vector2Int Position { get; set; }
    bool Explored { get; set; }
    bool Completed { get; set; }
}