using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinimapPanelScript : MonoBehaviour
{
    public GameObject previousRoomObject;
    public GameObject nextRoomObject;
    public GameObject pathObject;
    public GameObject parcelSpriteSelector;
    public GameObject roomSpriteSelector;
    
    private void Awake()
    {
        CreatePath();
        roomSpriteSelector.GetComponent<RoomSpriteSelector>().SelectSprite(Dungeon.NextRoom, nextRoomObject.GetComponent<Image>());
        roomSpriteSelector.GetComponent<RoomSpriteSelector>().SelectSprite(Dungeon.PreviousRoom, previousRoomObject.GetComponent<Image>());
    }

    private void CreatePath()
    {
        GridLayoutGroup gridLayoutGroup = pathObject.GetComponent<GridLayoutGroup>();
        Vector2 sizePathObject = pathObject.GetComponent<RectTransform>().sizeDelta;
        gridLayoutGroup.cellSize = new Vector2(sizePathObject.x/ Dungeon.NumberOfParcels, sizePathObject.x / Dungeon.NumberOfParcels);
        if (Dungeon.direction == Dungeon.Direction.Down || Dungeon.direction == Dungeon.Direction.Right)
        {
            for (int z = 0; z < Dungeon.NumberOfParcels; z++)
            {
                CreateTile(gridLayoutGroup, z);
            }
        }
        else
        {
            for (int z = Dungeon.NumberOfParcels - 1; z >= 0; z--)
            {
                CreateTile(gridLayoutGroup, z);
            }
        }
    }

    private void CreateTile(GridLayoutGroup gridLayoutGroup, int z)
    {
        GameObject parcelTile = Instantiate(new GameObject(), pathObject.transform);
        parcelTile.transform.name = "PathTile" + z;
        Image image = parcelTile.AddComponent<Image>();
        parcelSpriteSelector.GetComponent<ParcelSpriteSelector>().SelectSprite(Dungeon.Parcels[Dungeon.PlayerPosition.x, Dungeon.PlayerPosition.y, z], image);

        //Creation de l'effet 
        GameObject highlight = Instantiate(new GameObject(), parcelTile.transform);
        highlight.AddComponent<GlowingScript>();
        Image glowingImage = highlight.AddComponent<Image>();
        glowingImage.sprite = parcelSpriteSelector.GetComponent<ParcelSpriteSelector>().glowingEffect;
        glowingImage.enabled = false;

        //Ajuste la taille
        RectTransform rectTransform = highlight.GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(gridLayoutGroup.cellSize.x, gridLayoutGroup.cellSize.y);
    }
    
    public void RefreshParcelSprites(Vector3Int position, bool isCurrentParcel)
    {
        GameObject pathTile = GameObject.Find("PathTile" + position.z);
        parcelSpriteSelector.GetComponent<ParcelSpriteSelector>().SelectSprite(Dungeon.Parcels[position.x, position.y, position.z], pathTile.GetComponent<Image>());
        pathTile.transform.GetChild(0).gameObject.GetComponent<Image>().enabled = isCurrentParcel;
    }
}
