using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InstantiatePathScene : MonoBehaviour
{
    private GameObject backgroundLayer,
        middlegroundLayer,
        foregroundLayer;
    public GameObject background,
        door,
        treasure,
        book;


    private void Start()
    {
        backgroundLayer = transform.GetChild(0).gameObject;
        middlegroundLayer = transform.GetChild(1).gameObject;
        foregroundLayer = transform.GetChild(2).gameObject;

        for (int i = 1; i <= Dungeon.NumberOfParcels + 1; i++)
        {
            GameObject backgroundObject = Instantiate(background, background.transform.parent);
            RectTransform rectTransform = backgroundObject.GetComponent<RectTransform>();
            rectTransform.position = new Vector3(rectTransform.position.x +i * Math.Abs(rectTransform.sizeDelta.x * rectTransform.localScale.x), rectTransform.position.y, rectTransform.position.z);
            if (i == Dungeon.NumberOfParcels + 1) //Dernière itération, on place une porte
            {
                GameObject exitDoor = Instantiate(door, middlegroundLayer.transform);
                exitDoor.GetComponent<DoorScript>().type = DoorScript.Type.Exit;
                exitDoor.GetComponent<Transform>().position = new Vector3(rectTransform.position.x-500, middlegroundLayer.transform.Find("EnterDoor").GetComponent<Transform>().position.y, 0);
            }
            else //Une parcelle avec un evenement potentiel
            {
                BoxCollider2D boxcollider = backgroundObject.AddComponent<BoxCollider2D>();
                boxcollider.isTrigger = true;
                ParcelScript parcelScript = backgroundObject.AddComponent<ParcelScript>();
                IParcel parcel = null;
                if (Dungeon.direction == Dungeon.Direction.Down || Dungeon.direction == Dungeon.Direction.Right)
                {
                    parcelScript.position = new Vector3Int(Dungeon.PlayerPosition.x, Dungeon.PlayerPosition.y, Dungeon.PlayerPosition.z - 1 + i);
                    parcel = Dungeon.Parcels[Dungeon.PlayerPosition.x, Dungeon.PlayerPosition.y, Dungeon.PlayerPosition.z - 1 + i];
                }
                else
                {
                    parcelScript.position = new Vector3Int(Dungeon.PlayerPosition.x, Dungeon.PlayerPosition.y, Dungeon.PlayerPosition.z + 1 - i);
                    parcel = Dungeon.Parcels[Dungeon.PlayerPosition.x, Dungeon.PlayerPosition.y, Dungeon.PlayerPosition.z + 1 - i];
                }

                if(parcel.GetType() == typeof(TreasureParcel))
                {
                    GameObject treasureObject = Instantiate(treasure);
                    RectTransform treasureTransform = treasureObject.GetComponent<RectTransform>();
                    treasureTransform.position = new Vector3(rectTransform.position.x, rectTransform.position.y - 100, rectTransform.position.z);
                }
                else if (parcel.GetType() == typeof(BookParcel))
                {
                    GameObject bookObject = Instantiate(book);
                    RectTransform bookTransform = bookObject.GetComponent<RectTransform>();
                    bookTransform.position = new Vector3(rectTransform.position.x, rectTransform.position.y - 100, rectTransform.position.z);
                }
                else if(parcel.GetType() == typeof(BattleParcel))
                {
                    GameObject position1 = Instantiate(
                        new GameObject(), 
                        new Vector3(rectTransform.position.x - 140, rectTransform.position.y - 100, rectTransform.position.z + 100), 
                        Quaternion.identity, 
                        middlegroundLayer.transform
                    );
                    GameObject position2 = Instantiate(
                        new GameObject(),
                        new Vector3(rectTransform.position.x, rectTransform.position.y - 100, rectTransform.position.z + 100),
                        Quaternion.identity,
                        middlegroundLayer.transform
                    );
                    GameObject position3 = Instantiate(
                        new GameObject(),
                        new Vector3(rectTransform.position.x + 140, rectTransform.position.y - 100, rectTransform.position.z + 100),
                        Quaternion.identity,
                        middlegroundLayer.transform
                    );
                    GameObject position4 = Instantiate(
                        new GameObject(),
                        new Vector3(rectTransform.position.x + 280, rectTransform.position.y - 100, rectTransform.position.z + 100),
                        Quaternion.identity,
                        middlegroundLayer.transform
                    );
                    if (Dungeon.direction == Dungeon.Direction.Down || Dungeon.direction == Dungeon.Direction.Right)
                    {
                        position1.name = "Parcel" + (Dungeon.PlayerPosition.z + i) + "Position1";
                        position2.name = "Parcel" + (Dungeon.PlayerPosition.z + i) + "Position2";
                        position3.name = "Parcel" + (Dungeon.PlayerPosition.z + i) + "Position3";
                        position4.name = "Parcel" + (Dungeon.PlayerPosition.z + i) + "Position4";
                    }
                    else
                    {
                        position1.name = "Parcel" + (Dungeon.PlayerPosition.z - i + 2) + "Position1";
                        position2.name = "Parcel" + (Dungeon.PlayerPosition.z - i + 2) + "Position2";
                        position3.name = "Parcel" + (Dungeon.PlayerPosition.z - i + 2) + "Position3";
                        position4.name = "Parcel" + (Dungeon.PlayerPosition.z - i + 2) + "Position4";
                    }
                }
            }
        }
    }
}
