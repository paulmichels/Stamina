using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateRoomEvent : MonoBehaviour
{

    private GameManager gameManager;
    public GameObject Treasure;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        switch (gameManager.CurrentRoom.type)
        {
            case Room.Type.Treasure:
                Instantiate(Treasure, gameObject.transform);
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
