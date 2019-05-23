using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public enum Type : short { Enter, Exit };
    public Type type;
   
    private FadeScript fadeScript;

    private void Awake()
    {
        fadeScript = FadeScript.instance;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (type)
        {
            case Type.Enter:
                Dungeon.PlayerPosition = new Vector3Int(Dungeon.PreviousRoom.Position.x, Dungeon.PreviousRoom.Position.y, 0);
                Dungeon.PreviousRoom = null;
                Dungeon.NextRoom = null;
                Dungeon.CurrentRoom = Dungeon.Rooms[Dungeon.PlayerPosition.x, Dungeon.PlayerPosition.y];
                break;

            case Type.Exit:
                Dungeon.PlayerPosition = new Vector3Int(Dungeon.NextRoom.Position.x, Dungeon.NextRoom.Position.y, 0);
                Dungeon.PreviousRoom = null;
                Dungeon.NextRoom.Explored = true;
                Dungeon.NextRoom = null;
                Dungeon.CurrentRoom = Dungeon.Rooms[Dungeon.PlayerPosition.x, Dungeon.PlayerPosition.y];
                break;
        }
        StartCoroutine(fadeScript.LoadLevel("Room", fadeScript.BeginFade(1)));
    }
}
