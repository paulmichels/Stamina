using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapScript : MonoBehaviour
{
    public GameObject SceneFader;
    public GameObject LevelCreator;
    public AudioClip Music;
    
    public void BackToMenu()
    {
        float fadeTime = SceneFader.GetComponent<FadeScript>().BeginFade(1);
        StartCoroutine(SceneFader.GetComponent<FadeScript>().LoadLevel("MainMenu", fadeTime));
    }

    public void StartLevel()
    {
        LevelCreator.GetComponent<LevelGeneration>().CreateLevel(LevelCreator);
        GameObject.DontDestroyOnLoad(LevelCreator);
        float fadeTime = SceneFader.GetComponent<FadeScript>().BeginFade(1);
        StartCoroutine(SceneFader.GetComponent<FadeScript>().LoadLevel("Room", fadeTime));
    }
}
