using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomSpriteSelector : MonoBehaviour
{
    public Sprite startRoom;
    public Sprite endRoom;
    public Sprite unexploredRoom;
    public Sprite emptyRoom;
    public Sprite treasureRoom;
    public Sprite battleRoom;
    public Sprite bossRoom;
    public Sprite glowingEffect;
    public Sprite playerIcon;

    public void SelectSprite(IRoom room, Image image)
    {
        if (room.Explored)
        {
            if (room is StartRoom)
            {
                image.sprite = startRoom;
            }
            else if (room is EndRoom)
            {
                image.sprite = endRoom;
            }
            else if (room is BattleRoom)
            {
                image.sprite = battleRoom;
            }
            else if (room is EmptyRoom)
            {
                image.sprite = emptyRoom;
            }
            else if (room is BossRoom)
            {
                image.sprite = bossRoom;
            }
            else if (room is TreasureRoom)
            {
                image.sprite = treasureRoom;
            }
        }
        else
        {
            image.sprite = unexploredRoom;
        }
    }
}
