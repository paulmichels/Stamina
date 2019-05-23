using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookScript : MonoBehaviour
{
    private GameObject actionButton;

    private void Awake()
    {
        Transform[] trans = GameObject.Find("Minimap").GetComponentsInChildren<Transform>(true);
        foreach (Transform t in trans)
        {
            if (t.gameObject.name == "ActionButton")
            {
                actionButton = t.gameObject;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //Contenu du livre
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        actionButton.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        actionButton.SetActive(false);
    }
}
