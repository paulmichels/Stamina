using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HUDScript : MonoBehaviour
{

    public void quitDungeon()
    {
        Destroy(GameObject.Find("LevelCreator"));
        SceneManager.LoadScene("Map");
    }

}
