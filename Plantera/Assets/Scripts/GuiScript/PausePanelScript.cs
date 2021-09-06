using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System.Xml.Serialization;
using UnityEngine.UI;

public class PausePanelScript : MonoBehaviour
{
    public string mainMenuScene;
    float timeScaleBeforePaused;
    [SerializeField] Button saveButton;

    private void OnEnable()
    {
        if (SceneManager.GetActiveScene().name == "Tutorial_1_Forest_1") saveButton.interactable = false;
        else saveButton.interactable = true; 
    }

    public void PauseGame()
    {
        if (!gameObject.activeSelf)
        {
            SoundManager.instance.PlaySfx(GuiManager.instance.buttonClick);
            gameObject.SetActive(true);
            timeScaleBeforePaused = Time.timeScale;
            Time.timeScale = 0f;
        }
    }
    public void ContinueGame()
    {
        SoundManager.instance.PlaySfx(GuiManager.instance.buttonClick);
        Time.timeScale = timeScaleBeforePaused;
        gameObject.SetActive(false);
    }
    public void SaveGame()
    {
        SoundManager.instance.PlaySfx(GuiManager.instance.buttonClick);

        if (SceneManager.GetActiveScene().name == "Forest_4" && !PlayerManager.instance.playerStat.bossKilled[0]) ;
        else if (SceneManager.GetActiveScene().name == "Wind_4" && !PlayerManager.instance.playerStat.bossKilled[1]) ;
        else if (SceneManager.GetActiveScene().name == "Cold_3" && !PlayerManager.instance.playerStat.bossKilled[2]) ;
        else if (SceneManager.GetActiveScene().name == "Hot_4" && !PlayerManager.instance.playerStat.bossKilled[3]) ;
        else if (SceneManager.GetActiveScene().name == "Forest_6" && !PlayerManager.instance.playerStat.bossKilled[4]) ;
        else PlayerManager.instance.UpdatePlayerSaveData();


        string savePath = Application.persistentDataPath;
        var serializer = new XmlSerializer(typeof(PlayerSaveData));
        var stream = new FileStream(savePath + "/player.save", FileMode.Create);
        serializer.Serialize(stream, PlayerManager.instance.playerSaveData);
        stream.Close();
    }
    public void ReturnToMainMenu()
    {
        SoundManager.instance.PlaySfx(GuiManager.instance.buttonClick);
        Time.timeScale = 1f;
        SceneManager.LoadScene(mainMenuScene);
    }
}
