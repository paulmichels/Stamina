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
        Dungeon.parcels[position.x, position.y, position.z].isExplored = true;
        Dungeon.playerPosition.Set(Dungeon.playerPosition.x, Dungeon.playerPosition.y, position.z);
        minimapPanelScript.RefreshParcelSprites(position, true);
        TriggerParcelEvent();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        minimapPanelScript.RefreshParcelSprites(position, false);
    }

    //Seulement les évenements invisibles
    private void TriggerParcelEvent()
    {
        Parcel parcel = Dungeon.parcels[position.x, position.y, position.z];
        if (!parcel.isCompleted)
        {
            switch (Dungeon.parcels[position.x, position.y, position.z].type)
            {
                case Parcel.Type.Battle:
                    BattleEvent();
                    break;

                case Parcel.Type.Trap:
                    TrapEvent();
                    break;
            }
        }
    }

    private void BattleEvent()
    {

    }

    private void TrapEvent()
    {

    }
}
