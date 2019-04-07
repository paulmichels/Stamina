using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    public Camera mainCam;
    public Button playButton;
    public Button optionsButton;
    public Button quitButton;

    public void StartGame()
    {
        StartCoroutine(FadeOut(playButton.GetComponent<Image>()));
        StartCoroutine(FadeOut(optionsButton.GetComponent<Image>()));
        StartCoroutine(FadeOut(quitButton.GetComponent<Image>()));
        StartCoroutine(CameraIn(mainCam));
        StartCoroutine(WaitTime(0.75f));
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    IEnumerator FadeOut(Image image)
    {
        for(float f = 1f; f >= -0.05f; f -= 0.05f)
        {
            Color color = image.color;
            color.a = f;
            image.color = color;
            yield return new WaitForSeconds(0.025f);
        }
    }

    IEnumerator CameraIn(Camera camera)
    {
        Transform transform = camera.GetComponent<Transform>();
        for (float f = 0f; f <= 1450.0f; f += 12.5f * Time.deltaTime)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 12.5f);
            yield return new WaitForSeconds(0.01f);
        }
    }

    IEnumerator WaitTime(float time)
    {
        yield return new WaitForSeconds(time);
        StartCoroutine(FadeScript.instance.LoadLevel("Map", FadeScript.instance.BeginFade(1)));
    }
}
