using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapPanelScript : MonoBehaviour
{
    private RectTransform rectTransform;

    public GameObject roomSpriteSelector;
    public GameObject parcelSpriteSelector;
    public GameObject tilePrefab;

    private void Awake()
    {
        rectTransform = gameObject.GetComponent<RectTransform>();
        AddGridComponent();
        FillGrid();
    }

    private void AddGridComponent()
    {
        GridLayoutGroup gridLayoutGroup = gameObject.AddComponent<GridLayoutGroup>(); //Ajoute le composant à l'objet
        gridLayoutGroup.childAlignment = TextAnchor.MiddleCenter;
        if (Dungeon.DungeonSizeXMax - Dungeon.DungeonSizeXMin + 1 > Dungeon.DungeonSizeYMax - Dungeon.DungeonSizeYMin + 1)
        {
            gridLayoutGroup.cellSize = new Vector2(rectTransform.sizeDelta.x / (Dungeon.DungeonSizeXMax - Dungeon.DungeonSizeXMin + 1), //Taille des cellules
                                         rectTransform.sizeDelta.y / (Dungeon.DungeonSizeXMax - Dungeon.DungeonSizeXMin + 1));
            gridLayoutGroup.constraint = GridLayoutGroup.Constraint.FixedColumnCount; //Ajout de la contrainte colonne max
            gridLayoutGroup.constraintCount = Dungeon.DungeonSizeXMax - Dungeon.DungeonSizeXMin + 1; //Colonne max
        }
        else
        {
            gridLayoutGroup.cellSize = new Vector2(rectTransform.sizeDelta.x / (Dungeon.DungeonSizeYMax - Dungeon.DungeonSizeYMin + 1), //Taille des cellules
                                         rectTransform.sizeDelta.y / (Dungeon.DungeonSizeYMax - Dungeon.DungeonSizeYMin + 1));
            gridLayoutGroup.constraint = GridLayoutGroup.Constraint.FixedRowCount; //Ajout de la contrainte colonne max
            gridLayoutGroup.constraintCount = Dungeon.DungeonSizeYMax - Dungeon.DungeonSizeYMin + 1; //Colonne max
        }
    }

    private void FillGrid()
    {
        for (int y = Dungeon.DungeonSizeYMax; y >= Dungeon.DungeonSizeYMin; y--)
        {
            for(int x = Dungeon.DungeonSizeXMin; x <= Dungeon.DungeonSizeXMax; x++)
            {
                if(y % 2 == 0) //Ligne alterne salle / chemins horizontaux
                {
                    if(x % 2 == 0)
                    {
                        CreateRoom(x,y);
                    }
                    else
                    {
                        CreatePath(x,y,false);
                    }
                }
                else //Ligne chemins alterne verticaux / vide
                {
                    if(x % 2 == 0)
                    {
                        CreatePath(x,y,true);
                    }
                    else
                    {
                        CreateVoid(x,y);
                    }
                }
            }
        }
    }

    private void CreateRoom(int x, int y)
    {
        GameObject tile = Instantiate(tilePrefab, gameObject.transform);
        //Titre pour le débug
        tile.transform.name = "Room(" + x + "," + y + ")";
        IRoom room = Dungeon.Rooms[x, y];
        if (room != null)
        {
            roomSpriteSelector.GetComponent<RoomSpriteSelector>().SelectSprite(room, tile.AddComponent<Image>());
            if (room.Position.x == Dungeon.PlayerPosition.x && room.Position.y == Dungeon.PlayerPosition.y)
            {
                PlacePlayerIcon(tile);
            }

            GridLayoutGroup gridLayoutGroup = gameObject.GetComponent<GridLayoutGroup>();
            if (room.Position.y == Dungeon.PlayerPosition.y + 2 && room.Position.x == Dungeon.PlayerPosition.x)
            {
                NeighboorEvent(tile, gridLayoutGroup, Dungeon.Direction.Up);
            }

            if (room.Position.y == Dungeon.PlayerPosition.y - 2 && room.Position.x == Dungeon.PlayerPosition.x)
            {
                NeighboorEvent(tile, gridLayoutGroup, Dungeon.Direction.Down);
            }

            if (room.Position.x== Dungeon.PlayerPosition.x + 2 && room.Position.y == Dungeon.PlayerPosition.y)
            {
                NeighboorEvent(tile, gridLayoutGroup, Dungeon.Direction.Right);
            }

            if (room.Position.x== Dungeon.PlayerPosition.x - 2 && room.Position.y == Dungeon.PlayerPosition.y)
            {
                NeighboorEvent(tile, gridLayoutGroup, Dungeon.Direction.Left);
            }
        }
    }

    private void CreatePath(int x, int y, bool isHorizontal)
    {
        GameObject tile = Instantiate(tilePrefab, gameObject.transform);
        //Titre pour le débug
        tile.transform.name = "Path("+x + "," + y+")";
        IParcel parcel = Dungeon.Parcels[x, y, 0];
        if (Dungeon.DungeonSizeXMax - Dungeon.DungeonSizeXMin + 1 > Dungeon.DungeonSizeYMax - Dungeon.DungeonSizeYMin + 1)
        {
            if (parcel != null)
            {
                GridLayoutGroup gridLayoutGroup = tile.AddComponent<GridLayoutGroup>();
                gridLayoutGroup.cellSize = new Vector2(rectTransform.sizeDelta.x / ((Dungeon.DungeonSizeXMax - Dungeon.DungeonSizeXMin + 1) * Dungeon.NumberOfParcels), //Taille des cellules
                                             rectTransform.sizeDelta.y / ((Dungeon.DungeonSizeXMax - Dungeon.DungeonSizeXMin + 1) * Dungeon.NumberOfParcels));
                gridLayoutGroup.childAlignment = TextAnchor.MiddleCenter;
                if (isHorizontal)
                {
                    gridLayoutGroup.constraint = GridLayoutGroup.Constraint.FixedColumnCount; //Ajout de la contrainte colonne max
                    gridLayoutGroup.constraintCount = 1; //Colonne max
                }
                else
                {
                    gridLayoutGroup.constraint = GridLayoutGroup.Constraint.FixedRowCount; //Ajout de la contrainte ligne max
                    gridLayoutGroup.constraintCount = 1; //Ligne max
                }

                for (int z = 0; z < Dungeon.NumberOfParcels; z++)
                {
                    GameObject pathTile = Instantiate(tilePrefab, tile.transform);
                    pathTile.transform.name = "PathTile" + z;
                    parcel = Dungeon.Parcels[x, y, z];
                    if (parcel != null)
                    {
                        parcelSpriteSelector.GetComponent<ParcelSpriteSelector>().SelectSprite(parcel, pathTile.AddComponent<Image>());
                    }
                }
            }
        }
        else
        {
            if (parcel != null)
            {
                GridLayoutGroup gridLayoutGroup = tile.AddComponent<GridLayoutGroup>();
                gridLayoutGroup.cellSize = new Vector2(rectTransform.sizeDelta.x / ((Dungeon.DungeonSizeYMax - Dungeon.DungeonSizeYMin + 1) * Dungeon.NumberOfParcels), //Taille des cellules
                                             rectTransform.sizeDelta.y / ((Dungeon.DungeonSizeYMax - Dungeon.DungeonSizeYMin + 1) * Dungeon.NumberOfParcels));
                gridLayoutGroup.childAlignment = TextAnchor.MiddleCenter;
                if (isHorizontal)
                {
                    gridLayoutGroup.constraint = GridLayoutGroup.Constraint.FixedColumnCount; //Ajout de la contrainte colonne max
                    gridLayoutGroup.constraintCount = 1; //Colonne max
                }
                else
                {
                    gridLayoutGroup.constraint = GridLayoutGroup.Constraint.FixedRowCount; //Ajout de la contrainte ligne max
                    gridLayoutGroup.constraintCount = 1; //Ligne max
                }

                for (int z = 0; z < Dungeon.NumberOfParcels; z++)
                {
                    GameObject pathTile = Instantiate(new GameObject(), tile.transform);
                    pathTile.transform.name = "PathTile" + z;
                    parcel = Dungeon.Parcels[x, y, z];
                    if (parcel != null)
                    {
                        parcelSpriteSelector.GetComponent<ParcelSpriteSelector>().SelectSprite(parcel, pathTile.AddComponent<Image>());
                    }
                }
            }
        }
    }

    private void CreateVoid(int x, int y)
    {
        GameObject tile = Instantiate(tilePrefab, gameObject.transform);
        tile.transform.name = "Void("+x+","+y+")";
    }

    private void PlacePlayerIcon(GameObject parent)
    {
        GameObject heroIcon = Instantiate(new GameObject(), parent.transform); //Creation de l'effet 
        Image image = heroIcon.AddComponent<Image>();
        image.sprite = roomSpriteSelector.GetComponent<RoomSpriteSelector>().playerIcon;
        GridLayoutGroup gridLayoutGroup = gameObject.GetComponent<GridLayoutGroup>();
        RectTransform rectTransform = heroIcon.GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(gridLayoutGroup.cellSize.x / 2, gridLayoutGroup.cellSize.y / 2);
    }

    private void NeighboorEvent(GameObject tile, GridLayoutGroup gridLayoutGroup, Dungeon.Direction direction)
    {
        //Ajout du composant bouton et charge nouvelle scene au click
        Button button = tile.AddComponent<Button>();
        button.onClick.AddListener(delegate { GoToPath(direction); });

        //Ajout de l'effet glowing
        GameObject highlight = Instantiate(new GameObject(), tile.transform); //Creation de l'effet 
        highlight.AddComponent<GlowingScript>();
        Image image = highlight.AddComponent<Image>();
        image.sprite = roomSpriteSelector.GetComponent<RoomSpriteSelector>().glowingEffect;

        //Ajuste la taille
        RectTransform rectTransform = highlight.GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(gridLayoutGroup.cellSize.x, gridLayoutGroup.cellSize.y);
    }

    private void GoToPath(Dungeon.Direction direction)
    {
        Dungeon.PreviousRoom = Dungeon.Rooms[Dungeon.PlayerPosition.x, Dungeon.PlayerPosition.y];
        switch (direction)
        {
            case Dungeon.Direction.Up:
                Dungeon.NextRoom = Dungeon.Rooms[Dungeon.PlayerPosition.x, Dungeon.PlayerPosition.y + 2];
                Dungeon.PlayerPosition.Set(Dungeon.PlayerPosition.x, Dungeon.PlayerPosition.y + 1, Dungeon.NumberOfParcels - 1);
                Dungeon.direction = direction;
                break;

            case Dungeon.Direction.Down:
                Dungeon.NextRoom = Dungeon.Rooms[Dungeon.PlayerPosition.x, Dungeon.PlayerPosition.y - 2];
                Dungeon.PlayerPosition.Set(Dungeon.PlayerPosition.x, Dungeon.PlayerPosition.y - 1, 0);
                Dungeon.direction = direction;
                break;

            case Dungeon.Direction.Right:
                Dungeon.NextRoom = Dungeon.Rooms[Dungeon.PlayerPosition.x + 2, Dungeon.PlayerPosition.y];
                Dungeon.PlayerPosition.Set(Dungeon.PlayerPosition.x + 1, Dungeon.PlayerPosition.y, 0);
                Dungeon.direction = direction;
                break;

            case Dungeon.Direction.Left:
                Dungeon.NextRoom = Dungeon.Rooms[Dungeon.PlayerPosition.x - 2, Dungeon.PlayerPosition.y];
                Dungeon.PlayerPosition.Set(Dungeon.PlayerPosition.x - 1, Dungeon.PlayerPosition.y, Dungeon.NumberOfParcels - 1);
                Dungeon.direction = direction;
                break;
        }
        StartCoroutine(FadeScript.instance.LoadLevel("Path", FadeScript.instance.BeginFade(1)));
    }
}
