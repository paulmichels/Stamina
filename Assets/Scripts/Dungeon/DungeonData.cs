using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DungeonData
{
    public enum Zone : short { Mathematiques, Algorithmique, Physique, Communication, Reseaux }
    public enum Direction : short { Up, Down, Left, Right }
    public static Dungeon dungeon { get; set; }
    public static IRoom EndRoom { get; set; }
    public static IRoom PreviousRoom { get; set; }
    public static IRoom NextRoom { get; set; }
    public static Direction direction { get; set; }
    public static Zone zone { get; set; }
}
