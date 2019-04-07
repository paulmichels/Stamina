using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ControlPlayerScript : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{

    public GameObject Player;
    private PlayerScript playerScript;
    public enum Direction : short { left, right };
    public Direction direction;
    
    public void OnPointerDown(PointerEventData eventData)
    {
        playerScript = Player.GetComponent<PlayerScript>();
        switch (direction)
        {
            case Direction.left:
                playerScript.GoLeft();
                break;

            case Direction.right:
                playerScript.GoRight();
                break;
        }
    }
    
    public void OnPointerUp(PointerEventData eventData)
    {
        playerScript.Idle();
    }
}