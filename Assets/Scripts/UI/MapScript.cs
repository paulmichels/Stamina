using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapScript : MonoBehaviour
{
    private FadeScript fadeScript;

    private void Awake()
    {
        fadeScript = FadeScript.instance;
    }

    public void BackToMenu()
    {
        StartCoroutine(fadeScript.LoadLevel("MainMenu", fadeScript.BeginFade(1)));
    }

    public void StartLevel()
    {
        Debug.Log("Debut Level");
        Dungeon.CreateDungeon(10, 4, Dungeon.Zone.Algorithmique);
        StartCoroutine(fadeScript.LoadLevel("Room", fadeScript.BeginFade(1)));
    }
}
