using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiatePathScene : MonoBehaviour
{
    private GameObject backgroundLayer, middlegroundLayer, foregroundLayer;

    public GameObject background;
    public GameObject door;

    private void Awake()
    {
        backgroundLayer = transform.GetChild(0).gameObject;
        middlegroundLayer = transform.GetChild(1).gameObject;
        foregroundLayer = transform.GetChild(2).gameObject;

        for (int i = 1; i <= Dungeon.numberOfParcels + 1; i++)
        {
            GameObject backgroundObject = Instantiate(background, background.transform.parent);
            RectTransform rectTransform = backgroundObject.GetComponent<RectTransform>();
            rectTransform.position = new Vector3(rectTransform.position.x +i * Math.Abs(rectTransform.sizeDelta.x * rectTransform.localScale.x), rectTransform.position.y, rectTransform.position.z);
            if (i == Dungeon.numberOfParcels + 1) //Dernière itération, on place une porte
            {
                GameObject exitDoor = Instantiate(door, middlegroundLayer.transform);
                exitDoor.GetComponent<DoorScript>().type = DoorScript.Type.Exit;
                exitDoor.GetComponent<Transform>().position = new Vector3(rectTransform.position.x, middlegroundLayer.transform.Find("EnterDoor").GetComponent<Transform>().position.y, 0);
            }
            else //Une parcelle avec un evenement potentiel
            {
                BoxCollider2D boxcollider = backgroundObject.AddComponent<BoxCollider2D>();
                boxcollider.isTrigger = true;
                ParcelScript parcelScript = backgroundObject.AddComponent<ParcelScript>();

                if (Dungeon.direction == Dungeon.Direction.Down || Dungeon.direction == Dungeon.Direction.Right)
                {
                    parcelScript.position = new Vector3Int(Dungeon.playerPosition.x, Dungeon.playerPosition.y, Dungeon.playerPosition.z - 1 + i);
                }
                else
                {
                    parcelScript.position = new Vector3Int(Dungeon.playerPosition.x, Dungeon.playerPosition.y, Dungeon.playerPosition.z + 1 - i);
                }
            }
        }
    }

    private void ParcelEvent()
    {

    }
}
