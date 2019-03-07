using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InstantiateMinimap : MonoBehaviour
{
    private LevelContainer levelContainer;
    private GameManager gameManager;
    public GameObject previousRoom, nextRoom, path;

    // Start is called before the first frame update
    void Start()
    {
        levelContainer = GameObject.Find("LevelCreator").GetComponent<LevelContainer>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        DivideByPath();
        InstantiateRoomSprite(previousRoom, gameManager.PreviousRoom);
        InstantiateRoomSprite(nextRoom, gameManager.NextRoom);
        Vector2 posPrevious = gameManager.PreviousRoom.gridPos;
        Vector2 posNext = gameManager.NextRoom.gridPos;
        if (posPrevious.x > posNext.x || posPrevious.y < posNext.y)
        {
            gameManager.paths.Reverse();
        }
            for (int i = 0; i < gameManager.paths.Count ; i++)
        {
            InstantiatePathSprite(path.transform.GetChild(i).gameObject, gameManager.paths[i]);
        }
    }

    private void DivideByPath()
    {
        GridLayoutGroup gridLayoutGroup = path.GetComponent<GridLayoutGroup>();
        gridLayoutGroup.cellSize = new Vector2(path.GetComponent<RectTransform>().sizeDelta.x/gameManager.paths.Count, path.GetComponent<RectTransform>().sizeDelta.x / gameManager.paths.Count);
        foreach (Path parcel in gameManager.paths)
        {
            GameObject gameObject = Instantiate(new GameObject(), path.transform);
            gameObject.AddComponent<Image>();

            GameObject highlight = Instantiate(new GameObject(), gameObject.transform); //Creation de l'effet 
            highlight.AddComponent<GlowingScript>();
            Image image = highlight.AddComponent<Image>();
            image.sprite = levelContainer.GlowingEffect;
            image.enabled = false;

            //Ajuste la taille
            RectTransform rectTransform = highlight.GetComponent<RectTransform>();
            rectTransform.sizeDelta = new Vector2(gridLayoutGroup.cellSize.x, gridLayoutGroup.cellSize.y);
        }
    }

    private void InstantiateRoomSprite(GameObject gameObject, Room room)
    {
        if (room.isExplored)
        {
            switch (room.type)
            {
                case Room.Type.Start:
                    gameObject.GetComponent<Image>().sprite = levelContainer.StartRoom;
                    break;

                case Room.Type.End:
                    gameObject.GetComponent<Image>().sprite = levelContainer.EndRoom;
                    break;

                case Room.Type.Empty:
                    gameObject.GetComponent<Image>().sprite = levelContainer.EmptyRoom;
                    break;

                case Room.Type.Battle:
                    gameObject.GetComponent<Image>().sprite = levelContainer.BattleRoom;
                    break;

                case Room.Type.Treasure:
                    gameObject.GetComponent<Image>().sprite = levelContainer.TreasureRoom;
                    break;
            }
        } else
        {
            gameObject.GetComponent<Image>().sprite = levelContainer.UnexploredRoom;
        }
        
    }

    private void InstantiatePathSprite(GameObject gameObject, Path path)
    {
        if (path.isExplored)
        {
            switch (path.type)
            {
                case Path.Type.Empty:
                    gameObject.GetComponent<Image>().sprite = levelContainer.EmptyPath;
                    break;

                case Path.Type.Battle:
                    gameObject.GetComponent<Image>().sprite = levelContainer.BattlePath;
                    break;

                case Path.Type.Treasure:
                    gameObject.GetComponent<Image>().sprite = levelContainer.TreasurePath;
                    break;

                case Path.Type.Trap:
                    gameObject.GetComponent<Image>().sprite = levelContainer.TrapPath;
                    break;
            }
        } else
        {
            gameObject.GetComponent<Image>().sprite = levelContainer.UnexploredPath;
        }
        
    }

}
