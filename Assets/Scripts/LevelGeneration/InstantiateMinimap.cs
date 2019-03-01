using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InstantiateMinimap : MonoBehaviour
{
    public GameObject SceneFader;
    private GameObject levelCreator;
    private GameManager gameManager;
    private RectTransform rectTransform;
    private LevelContainer levelContainer;

    void Start()
    {
        levelCreator = GameObject.Find("LevelCreator");
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        rectTransform = gameObject.GetComponent<RectTransform>();
        levelContainer = levelCreator.GetComponent<LevelContainer>();
        AddGridComponent();
        FillGrid();
        AssociateSprites();
        if (gameManager.CurrentRoom != null)
        {
            Debug.Log("Dans une pièce");
            AddGlowingEffect();
        }
        if(gameManager.paths != null)
        {
            Debug.Log("Dans un chemin");
        }
    }

    private void AddGridComponent()
    {
        GridLayoutGroup gridLayoutGroup = gameObject.AddComponent<GridLayoutGroup>(); //Ajoute le composant à l'objet
        gridLayoutGroup.cellSize = new Vector2(rectTransform.sizeDelta.x/levelContainer.worldSize.x, //Taille des cellules
                                     rectTransform.sizeDelta.y / levelContainer.worldSize.y);
        gridLayoutGroup.constraint = GridLayoutGroup.Constraint.FixedColumnCount; //Ajout de la contrainte colonne max
        gridLayoutGroup.constraintCount = (int)levelContainer.worldSize.x; //Colonne max
    }

    private void FillGrid()
    {
        for (int y = ((int)levelContainer.worldSize.y/ 2)-1; y >= -((int)levelContainer.worldSize.y/2)-1; y--)
        {
            for(int x = -((int)levelContainer.worldSize.x/2)-1; x <= ((int)levelContainer.worldSize.x/2)-1; x++)
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
        GameObject tile = Instantiate(new GameObject(), gameObject.transform);
        tile.transform.name = x + "," + y;
        Image image = tile.AddComponent<Image>();
        image.enabled = false;
    }

    private void CreatePath(int x, int y, bool isHorizontal)
    {
        GameObject tile = Instantiate(new GameObject(), gameObject.transform);
        tile.transform.name = x + "," + y;
        GridLayoutGroup gridLayoutGroup = tile.AddComponent<GridLayoutGroup>();
        gridLayoutGroup.cellSize = new Vector2(rectTransform.sizeDelta.x / (levelContainer.worldSize.x* levelContainer.numberOfPath), //Taille des cellules
                                     rectTransform.sizeDelta.y / (levelContainer.worldSize.y* levelContainer.numberOfPath));
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
        
        for(int i = 1; i <= levelContainer.numberOfPath; i++)
        {
            GameObject pathTile = Instantiate(new GameObject(), tile.transform);
            pathTile.transform.name = "PathTile"+i;
            Image image = pathTile.AddComponent<Image>();
            image.enabled = false;
        }
        
    }

    private void CreateVoid(int x, int y)
    {
        GameObject tile = Instantiate(new GameObject(), gameObject.transform);
        tile.AddComponent<RectTransform>();
        tile.transform.name = x+","+y;
    }


    //TODO : Cacher si non découvert
    private void AssociateSprites()
    {
        foreach(Room room in levelContainer.rooms)
        {
            GameObject gameObject = GameObject.Find(room.gridPos.x+","+room.gridPos.y);
            if (gameObject != null)
            {
                RoomContainer roomContainer = gameObject.AddComponent<RoomContainer>();
                roomContainer.room = room;
                Image image = gameObject.GetComponent<Image>();
                image.enabled = true;
                switch (room.type)
                {
                    case Room.Type.Start:
                        image.sprite = levelContainer.StartRoom;
                        break;

                    case Room.Type.End:
                        image.sprite = levelContainer.EndRoom;
                        break;

                    case Room.Type.Empty:
                        image.sprite = levelContainer.EmptyRoom;
                        break;

                    case Room.Type.Battle:
                        image.sprite = levelContainer.BattleRoom;
                        break;

                    case Room.Type.Treasure:
                        image.sprite = levelContainer.TreasureRoom;
                        break;
                }
            }
            if (room == gameManager.CurrentRoom)
            {
                GameObject heroIcon = Instantiate(new GameObject(), gameObject.transform); //Creation de l'effet 
                Image image = heroIcon.AddComponent<Image>();
                image.sprite = levelContainer.PlayerIcon;
                GridLayoutGroup gridLayoutGroup = gameObject.GetComponentInParent<GridLayoutGroup>();
                RectTransform rectTransform = heroIcon.GetComponent<RectTransform>();
                rectTransform.sizeDelta = new Vector2(gridLayoutGroup.cellSize.x / 2, gridLayoutGroup.cellSize.y / 2);
            }
        }

        foreach (Path path in levelContainer.paths)
        {
            GameObject gameObject = GameObject.Find(path.gridPos.x + "," + path.gridPos.y);
            PathContainer pathContainer = gameObject.GetComponent<PathContainer>();
            if (pathContainer == null)
            {
                pathContainer = gameObject.AddComponent<PathContainer>();
            }
           
            pathContainer.paths.Add(path);
            if (gameObject != null)
            {
                Transform child = gameObject.transform.Find("PathTile"+(path.id+1));
                Image image = child.gameObject.GetComponent<Image>();
                image.enabled = true;
                switch (path.type)
                {
                    case Path.Type.Empty:
                        image.sprite = levelContainer.EmptyPath;
                        break;

                    case Path.Type.Battle:
                        image.sprite = levelContainer.BattlePath;
                        break;

                    case Path.Type.Treasure:
                        image.sprite = levelContainer.TreasurePath;
                        break;

                    case Path.Type.Trap:
                        image.sprite = levelContainer.TrapPath;
                        break;
                }
            }
        }
    }

    private void AddGlowingEffect()
    {
        GridLayoutGroup gridLayoutGroup = gameObject.GetComponentInParent<GridLayoutGroup>();
        GameObject neighbour = null;
        if (gameManager.CurrentRoom.doorLeft)
        {
            neighbour = GameObject.Find((gameManager.CurrentRoom.gridPos.x - 2) + "," + gameManager.CurrentRoom.gridPos.y);
            NeighboorEvent(neighbour, gridLayoutGroup, new Vector2(gameManager.CurrentRoom.gridPos.x - 1, gameManager.CurrentRoom.gridPos.y));
        }
        
        if (gameManager.CurrentRoom.doorRight)
        {
            neighbour = GameObject.Find((gameManager.CurrentRoom.gridPos.x + 2) + "," + gameManager.CurrentRoom.gridPos.y);
            NeighboorEvent(neighbour, gridLayoutGroup, new Vector2(gameManager.CurrentRoom.gridPos.x + 1, gameManager.CurrentRoom.gridPos.y));
        }
        
        if (gameManager.CurrentRoom.doorBot)
        {
            neighbour = GameObject.Find(gameManager.CurrentRoom.gridPos.x + "," + (gameManager.CurrentRoom.gridPos.y - 2));
            NeighboorEvent(neighbour, gridLayoutGroup, new Vector2(gameManager.CurrentRoom.gridPos.x, gameManager.CurrentRoom.gridPos.y - 1));
        }
        
        if (gameManager.CurrentRoom.doorTop)
        {
            neighbour = GameObject.Find(gameManager.CurrentRoom.gridPos.x + "," + (gameManager.CurrentRoom.gridPos.y + 2));
            NeighboorEvent(neighbour, gridLayoutGroup, new Vector2(gameManager.CurrentRoom.gridPos.x, gameManager.CurrentRoom.gridPos.y + 1));
        }
    }

    private void NeighboorEvent(GameObject neighbour, GridLayoutGroup gridLayoutGroup, Vector2 pathPos)
    {
        //Ajout du composant bouton et charge nouvelle scene au click
        Button button = neighbour.AddComponent<Button>();
        button.onClick.AddListener(delegate { GoToPath(neighbour.GetComponent<RoomContainer>(), pathPos); });

        //Ajout de l'effet glowing
        GameObject highlight = Instantiate(new GameObject(), neighbour.transform); //Creation de l'effet 
        highlight.AddComponent<GlowingScript>();
        Image image = highlight.AddComponent<Image>();
        image.sprite = levelContainer.GlowingEffect;

        //Ajuste la taille
        RectTransform rectTransform = highlight.GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(gridLayoutGroup.cellSize.x, gridLayoutGroup.cellSize.y);
    }

    private void GoToPath(RoomContainer roomContainer, Vector2 pathPos)
    {
        gameManager.paths = GameObject.Find(pathPos.x+ ","+ pathPos.y).GetComponent<PathContainer>().paths;
        gameManager.PreviousRoom = gameManager.CurrentRoom;
        gameManager.NextRoom = roomContainer.room;
        gameManager.CurrentRoom = null;
        float fadeTime = SceneFader.GetComponent<FadeScript>().BeginFade(1);
        StartCoroutine(SceneFader.GetComponent<FadeScript>().LoadLevel("Path", fadeTime));
    }
}
