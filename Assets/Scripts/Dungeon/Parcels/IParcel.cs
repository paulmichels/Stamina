using UnityEngine;

public interface IParcel
{
    Vector3Int Position { get; set; }
    bool Explored { get; set; }
    bool Completed { get; set; }
}