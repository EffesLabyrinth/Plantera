using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIFlash : MonoBehaviour
{
    public Image flashScreen;
    public float flashDuration;
    float startFlashDuration;

    private void OnEnable()
    {
        flashScreen.color = new Color(flashScreen.color.r,flashScreen.color.g,flashScreen.color.b,1f);
        startFlashDuration = flashDuration;
    }
    private void Update()
    {
        if (startFlashDuration > 0)
        {
            float opacity = startFlashDuration / flashDuration;
            if (opacity < 0) opacity = 0;
            flashScreen.color = new Color(flashScreen.color.r, flashScreen.color.g, flashScreen.color.b, opacity);
            startFlashDuration -= Time.deltaTime;
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
