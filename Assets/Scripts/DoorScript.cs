using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public enum Type : short { Enter, Exit };
    public Type type;

    public GameObject SceneFader;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameManager gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        switch (type)
        {
            case Type.Enter:
                gameManager.CurrentRoom = gameManager.PreviousRoom;
                gameManager.PreviousRoom = null;
                gameManager.paths = null;
                break;

            case Type.Exit:
                gameManager.CurrentRoom = gameManager.NextRoom;
                gameManager.NextRoom = null;
                gameManager.paths = null;
                break;
        }

        float fadeTime = SceneFader.GetComponent<FadeScript>().BeginFade(1);
        StartCoroutine(SceneFader.GetComponent<FadeScript>().LoadLevel("Room", fadeTime));
    }
}
