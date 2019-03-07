using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiatePathScene : MonoBehaviour
{

    private GameManager gameManager;
    public GameObject background;
    public GameObject door;
    public GameObject treasure;
    public GameObject battle;
    public GameObject trap;

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        for(int i = 1; i <= gameManager.paths.Count+1; i++)
        {
            GameObject gameobject = Instantiate(background, background.transform.parent);
            RectTransform rectTransform = gameobject.GetComponent<RectTransform>();
            rectTransform.position = new Vector3(rectTransform.position.x +i * Math.Abs(rectTransform.sizeDelta.x * rectTransform.localScale.x), rectTransform.position.y, rectTransform.position.z);
            if (i == gameManager.paths.Count+1) //Dernière itération, on place une porte
            {
                GameObject exitDoor = Instantiate(door, GameObject.Find("Entities").transform);
                exitDoor.GetComponent<DoorScript>().type = DoorScript.Type.Exit;
                exitDoor.GetComponent<DoorScript>().SceneFader = GameObject.Find("SceneFader");
                exitDoor.GetComponent<Transform>().position = new Vector2(rectTransform.position.x, GameObject.Find("EnterDoor").GetComponent<Transform>().position.y);
            }
            else //Une parcelle avec un evenement potentiel
            {
                //GameObject collider = Instantiate(new GameObject(), gameobject.transform);
                BoxCollider2D boxcollider = gameobject.AddComponent<BoxCollider2D>();
                boxcollider.isTrigger = true;
                ParcelScript parcelScript = gameobject.AddComponent<ParcelScript>();
                parcelScript.id = i - 1;
                switch (gameManager.paths[i-1].type)
                {
                    case Path.Type.Battle:
                        CreateBattle(gameobject);
                        break;

                    case Path.Type.Treasure:
                        CreateTreasure(gameobject);
                        break;

                    case Path.Type.Trap:
                        CreateTrap(gameobject);
                        break;
                }
            }
        }
    }

    private void CreateTrap(GameObject gameobject)
    {
        GameObject gameEvent =  Instantiate(trap, this.transform.Find("Middleground"));
        SpriteRenderer eventSpriteRenderer = gameEvent.GetComponent<SpriteRenderer>();
        eventSpriteRenderer.sortingLayerName = "Middleground";
        Transform transform = gameEvent.GetComponent<Transform>();
        transform.position = new Vector3(gameobject.GetComponent<Transform>().position.x, GameObject.Find("EnterDoor").GetComponent<Transform>().position.y, gameobject.GetComponent<Transform>().position.z);
    }

    private void CreateTreasure(GameObject gameobject)
    {
        GameObject gameEvent = Instantiate(treasure, this.transform.Find("Middleground"));
        SpriteRenderer eventSpriteRenderer = gameEvent.GetComponent<SpriteRenderer>();
        Transform transform = gameEvent.GetComponent<Transform>();
        transform.position = new Vector3(gameobject.GetComponent<Transform>().position.x, GameObject.Find("EnterDoor").GetComponent<Transform>().position.y, gameobject.GetComponent<Transform>().position.z);
    }

    private void CreateBattle(GameObject gameobject)
    {
        GameObject gameEvent = Instantiate(battle, this.transform.Find("Middleground"));
        SpriteRenderer eventSpriteRenderer = gameEvent.GetComponent<SpriteRenderer>();
        eventSpriteRenderer.sortingLayerName = "Middleground";
        Transform transform = gameEvent.GetComponent<Transform>();
        transform.position = new Vector3(gameobject.GetComponent<Transform>().position.x, GameObject.Find("EnterDoor").GetComponent<Transform>().position.y, gameobject.GetComponent<Transform>().position.z);
    }
}
