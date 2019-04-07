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
                Dungeon.playerPosition = new Vector3Int(Dungeon.previousRoom.gridPos.x, Dungeon.previousRoom.gridPos.y, 0);
                Dungeon.previousRoom = null;
                Dungeon.nextRoom = null;
                break;

            case Type.Exit:
                Dungeon.playerPosition = new Vector3Int(Dungeon.nextRoom.gridPos.x, Dungeon.nextRoom.gridPos.y, 0);
                Dungeon.previousRoom = null;
                Dungeon.nextRoom.isExplored = true;
                Dungeon.nextRoom = null;
                break;
        }
        StartCoroutine(fadeScript.LoadLevel("Room", fadeScript.BeginFade(1)));
    }
}
