using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room {
	public Vector2 gridPos;
	public int type;
    public string name;
	public bool doorTop, doorBot, doorLeft, doorRight;

	public Room(Vector2 gridPos, int type){
		this.gridPos = gridPos;
		this.type = type;
        name += "Room(" + gridPos.x+","+gridPos.y+")";
	}
}
