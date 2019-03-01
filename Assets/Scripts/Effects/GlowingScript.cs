using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GlowingScript : MonoBehaviour
{

    private Image image;
    private bool fadeOut;

    // Start is called before the first frame update
    void Start()
    {
        fadeOut = true;
    }

    // Update is called once per frame
    void Update()
    {
        Image image = GetComponent<Image>();
        if (fadeOut)
        {
            Color color = image.color;
            color.a -= 0.025f;
            image.color = color;
            if(color.a <=0.0f)
            {
                fadeOut = false;
            }
        }
        else
        {
            Color color = image.color;
            color.a += 0.025f;
            image.color = color;
            if (color.a >= 1.0f)
            {
                fadeOut = true;
            }
        }
    }
}
