using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TreasureRoom : IRoom
{
    public Vector2Int Position { get; set; }
    public bool Explored { get; set; }
    public bool Completed { get; set; }

    private GameObject treasure;

    public TreasureRoom(Vector2Int position)
    {
        Position = position;
    }

    public void TriggerEvent()
    {
        Transform[] trans = GameObject.Find("Middleground").GetComponentsInChildren<Transform>(true);
        foreach (Transform t in trans)
        {
            if (t.gameObject.name == "Treasure")
            {
                treasure = t.gameObject;
            }
        }
        CreateTreasure();
    }

    private void CreateTreasure()
    {
        treasure.GetComponent<Button>().onClick.AddListener(OnClick);
        treasure.SetActive(true);
        //treasure.AddComponent(typeof(EventTrigger));
        //EventTrigger trigger = treasure.GetComponent<EventTrigger>();
        //EventTrigger.Entry entry = new EventTrigger.Entry();
        //entry.eventID = EventTriggerType.PointerClick;
        //entry.callback.AddListener((eventData) => { OnClick(); });
        //trigger.triggers.Add(entry);
    }

    private void OnClick()
    {
        if (!Completed)
        {
            Debug.Log("Trésor ouvert");
            Completed = true;
        } else
        {
            Debug.Log("Trésor déjà ouvert");
        }   
    }

}
