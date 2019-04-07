using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ParcelSpriteSelector : MonoBehaviour
{
    public Sprite emptyParcel;
    public Sprite treasureParcel;
    public Sprite battleParcel;
    public Sprite trapParcel;
    public Sprite bookParcel;
    public Sprite unexploredParcel;
    public Sprite glowingEffect;

    public void SelectSprite(IParcel parcel, Image image)
    {

        if (parcel.Explored)
        {
            if (parcel is TreasureParcel)
            {
                image.sprite = treasureParcel;
            }
            else if (parcel is EmptyParcel)
            {
                image.sprite = emptyParcel;
            }
            else if (parcel is BattleParcel)
            {
                image.sprite = battleParcel;
            }
            else if (parcel is TrapParcel)
            {
                image.sprite = trapParcel;
            }
            else if (parcel is BookParcel)
            {
                image.sprite = bookParcel;
            }
        }
        else
        {
            image.sprite = unexploredParcel;
        }
    }
}
