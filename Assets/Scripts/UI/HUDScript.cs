using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HUDScript : MonoBehaviour
{

    private FadeScript fadeScript;

    private void Awake()
    {
        fadeScript = FadeScript.instance;
    }

    public void quitDungeon()
    {
        StartCoroutine(fadeScript.LoadLevel("Map", fadeScript.BeginFade(1)));
    }

}
