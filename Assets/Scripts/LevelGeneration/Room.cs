using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room {

    public enum Type : short { Start, End, Empty, Treasure, Battle };
    public Vector2 gridPos;
	public bool doorTop, doorBot, doorLeft, doorRight;
    public bool isExplored;
    public Type type;

	public Room(Vector2 gridPos, Type type, bool isExplored){
        this.type = type;
        this.gridPos = gridPos;
        this.isExplored = isExplored;
	}
}
