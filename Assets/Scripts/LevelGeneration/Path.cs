using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path
{
    public enum Type : short { Empty, Battle, Treasure, Trap };
    public Vector2 gridPos;
    public bool isExplored;
    public Type type;
    public int id;

    public Path(Vector2 gridPos, int id, bool isExplored)
    {
        this.gridPos = gridPos;
        this.isExplored = isExplored;
        this.id = id;
        RandomizeType();
    }

    private void RandomizeType()
    {
        int random = Random.Range(0, 11);
        if (random >= 1 && random <= 6) // Le chemin est un chemin vide (60% de chance)
        {
            type = Type.Empty;
        }
        else if (random >= 7 && random <= 8) // Le chemin contient un combat (20% de chance)
        {
            type = Type.Battle;
        }
        else if (random == 9) // Le chemin contient un trésor (10% de chance)
        {
            type = Type.Treasure;
        }
        else if (random == 10) // Le chemin contient un piège (10% de chance)
        {
            type = Type.Trap;
        }
    }
}
