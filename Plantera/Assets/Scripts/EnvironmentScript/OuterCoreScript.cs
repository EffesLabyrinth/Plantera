using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OuterCoreScript : MonoBehaviour
{
    public AudioClip bossMusic;
    [SerializeField] GameObject[] bosses;
    [SerializeField] GameObject[] spikes;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(gameObject.activeSelf && collision.CompareTag("Player"))
        {
            
            for (int i = 0; i < bosses.Length; i++)
            {
                bosses[i].SetActive(true);
            }
            for (int i = 0; i < spikes.Length; i++)
            {
                spikes[i].SetActive(true);
                gameObject.SetActive(false);
            }
            Camera.main.GetComponent<CameraMovement>().StartBossFightView();
            SoundManager.instance.PlayMusic(bossMusic);
        }
    }
}
