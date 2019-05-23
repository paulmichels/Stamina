using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ParcelScript : MonoBehaviour
{
    private MinimapPanelScript minimapPanelScript;
    public Vector3Int position { get; set; }

    private void Awake()
    {
        minimapPanelScript = GameObject.Find("Minimap").GetComponent<MinimapPanelScript>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Dungeon.Parcels[position.x, position.y, position.z].Explored = true;
        Dungeon.PlayerPosition.Set(Dungeon.PlayerPosition.x, Dungeon.PlayerPosition.y, position.z);
        minimapPanelScript.RefreshParcelSprites(position, true);
        Dungeon.Parcels[position.x, position.y, position.z].TriggerEvent();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        minimapPanelScript.RefreshParcelSprites(position, false);
    }
}
