using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ParcelScript : MonoBehaviour
{
    private LevelContainer levelContainer;
    private BattleEvent battleHandler;
    private GameObject gameManager, path;
    public int id;

    private void Start()
    {
        gameManager = GameObject.Find("GameManager");
        levelContainer = GameObject.Find("LevelCreator").GetComponent<LevelContainer>();
        battleHandler = GameObject.Find("BattleHandler").GetComponent<BattleEvent>();
        path = GameObject.Find("Path");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject parcel = path.transform.GetChild(id).gameObject;
        GameObject glowing = parcel.transform.GetChild(0).gameObject;
        glowing.GetComponent<Image>().enabled = true;
        GameObject.Find("LevelCreator").GetComponent<LevelContainer>().paths.Find(obj => obj.Equals(gameManager.GetComponent<GameManager>().paths[id])).isExplored = true;
        RefreshPathSprite(parcel, gameManager.GetComponent<GameManager>().paths[id]);
        CreateEvent(parcel, gameManager.GetComponent<GameManager>().paths[id]);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        GameObject parcel = path.transform.GetChild(id).gameObject;
        GameObject glowing = parcel.transform.GetChild(0).gameObject;
        glowing.GetComponent<Image>().enabled = false;
    }

    private void RefreshPathSprite(GameObject gameObject, Path path)
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
    }

    private void CreateEvent(GameObject parcel, Path path)
    {
        switch (path.type)
        {
            case Path.Type.Battle:
                battleHandler.SetUpBattleHUD();
                battleHandler.SpawnEnnemies(parcel);
                break;

            case Path.Type.Trap:
                
                break;
        }
    }
}
