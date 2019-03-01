using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuScript : MonoBehaviour
{
    public GameObject SceneFader;
    public Camera mainCam;

    public void StartGame()
    {
        float fadeTime = SceneFader.GetComponent<FadeScript>().BeginFade(1);
        StartCoroutine(SceneFader.GetComponent<FadeScript>().LoadLevel("Map", fadeTime));
    }

    public void QuitGame()
    {
        Debug.Log("Quitter");
        Application.Quit();
    }
}
