using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleEvent : MonoBehaviour
{
    public GameObject player, leftArrow, rightArrow, bottomHUD, enemy;
    private PlayerScript playerScript;
    
    public void SetUpBattleHUD()
    {
        playerScript = player.GetComponent<PlayerScript>();
        playerScript.Idle();
        if(leftArrow != null && rightArrow != null)
        {
            leftArrow.SetActive(false);
            rightArrow.SetActive(false);
        }
        else
        {
            bottomHUD.SetActive(true);
        }
    }

    public void EndBattle()
    {
        if (leftArrow != null && rightArrow != null)
        {
            leftArrow.SetActive(false);
            rightArrow.SetActive(false);
        }
        else
        {
            bottomHUD.SetActive(false);
        }
    }

    public void SpawnEnnemies(GameObject parcel)
    {
        GameObject enemyObject = Instantiate(enemy, GameObject.Find("Entities").transform);
        enemyObject.GetComponent<Transform>().position = new Vector3(parcel.GetComponent<Transform>().position.x, enemyObject.GetComponent<Transform>().position.y, enemyObject.GetComponent<Transform>().position.z);
    }
}
