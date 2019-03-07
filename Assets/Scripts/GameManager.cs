using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Room PreviousRoom, NextRoom, CurrentRoom;
    public List<Path> paths;
    public Path CurrentPath;

    private void Start()
    {
        DontDestroyOnLoad(this);    
    }
}
