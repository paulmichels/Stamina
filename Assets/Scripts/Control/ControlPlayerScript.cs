using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;// Required when using Event data.

public class ControlPlayerScript : MonoBehaviour, IPointerUpHandler, IPointerDownHandler// These are the interfaces the OnPointerUp method requires.
{

    public GameObject Player;
    private PlayerScript playerScript;
    public enum Direction : short { left, right };
    public Direction direction;

    //Bouton pressé
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

    //Bouton relâché
    public void OnPointerUp(PointerEventData eventData)
    {
        playerScript.Idle();
    }
}