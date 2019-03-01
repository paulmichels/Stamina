using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelContainer : MonoBehaviour
{
    public List<Room> rooms = new List<Room>();
    public List<Path> paths = new List<Path>();
    public Vector2 worldSize;
    public int numberOfPath;
    public Sprite StartRoom, EndRoom, UnexploredRoom, EmptyRoom, TreasureRoom, BattleRoom, BossRoom;
    public Sprite UnexploredPath, EmptyPath, BattlePath, TreasurePath, TrapPath;
    public Sprite GlowingEffect;
    public Sprite PlayerIcon;

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
