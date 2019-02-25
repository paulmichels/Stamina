using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public SpriteRenderer Room1, Room2, Room3, Room4, Room5, Room6, Room7, Room8, Room9, Room10, Room11, Room12, Room13, Room14, Room15, Room16;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
}
