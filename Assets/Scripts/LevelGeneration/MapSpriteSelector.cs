using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSpriteSelector : MonoBehaviour {
	
	public Sprite 	spU, spD, spR, spL,
			spUD, spRL, spUR, spUL, spDR, spDL,
			spULD, spRUL, spDRU, spLDR, spUDRL;
	public bool up, down, left, right;
	public int type; // 0: normal, 1: enter
	public Color normalColor, enterColor, battleColor, treasureColor, endColor;
	Color mainColor;
	SpriteRenderer rend;

	void Start () {
		rend = GetComponent<SpriteRenderer>();
        rend.enabled = false;
		mainColor = normalColor;
		PickSprite();
		PickColor();
	}

	void PickSprite(){ //picks correct sprite based on the four door bools
		if (up){
			if (down){
				if (right){
					if (left){
						rend.sprite = spUDRL;
					}else{
						rend.sprite = spDRU;
					}
				}else if (left){
					rend.sprite = spULD;
				}else{
					rend.sprite = spUD;
				}
			}else{
				if (right){
					if (left){
						rend.sprite = spRUL;
					}else{
						rend.sprite = spUR;
					}
				}else if (left){
					rend.sprite = spUL;
				}else{
					rend.sprite = spU;
				}
			}
			return;
		}
		if (down){
			if (right){
				if(left){
					rend.sprite = spLDR;
				}else{
					rend.sprite = spDR;
				}
			}else if (left){
				rend.sprite = spDL;
			}else{
				rend.sprite = spD;
			}
			return;
		}
		if (right){
			if (left){
				rend.sprite = spRL;
			}else{
				rend.sprite = spR;
			}
		}else{
			rend.sprite = spL;
		}
	}

	void PickColor(){ //changes color based on what type the room is
		if (type == 0){
			mainColor = enterColor;
		}else if (type >= 1 && type <= 3){
			mainColor = normalColor;
		} else if (type >= 4 && type <= 7){
            mainColor = battleColor;
        } else if(type >= 8 && type <= 9){
            mainColor = treasureColor;
        } else if (type == 10){
            mainColor = endColor;
        }
        rend.color = mainColor;
	}
}