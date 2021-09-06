using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System.Xml.Serialization;

public class DeathPanelScript : MonoBehaviour
{
    public void SetDeathPanelActive()
    {
        GuiManager.instance.pausePanel.gameObject.SetActive(false);
        gameObject.SetActive(true);
        SoundManager.instance.LowerMusicPitch();
        Time.timeScale = 0f;
    }
    public void ReturnToMainMenu()
    {
        SoundManager.instance.PlaySfx(GuiManager.instance.buttonClick);
        Time.fixedDeltaTime = 0.02f;
        Time.timeScale = 1f;
        SceneManager.LoadScene("Main_Menu");
    }
    public void LoadLastSavedFile()
    {
        SoundManager.instance.PlaySfx(GuiManager.instance.buttonClick);
        Time.fixedDeltaTime = 0.02f;
        Time.timeScale = 1f;

        if (SceneManager.GetActiveScene().name == "Tutorial_1_Forest_1")
        {
            PlayerManager.instance.gameObject.SetActive(true);
            PlayerManager.instance.playerStat.isAlive = true;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            return;
        }

        PlayerSaveData playerSaveData = new PlayerSaveData();
        string savePath = Application.persistentDataPath;
        if (File.Exists(savePath + "/player.save"))
        {
            var serializer = new XmlSerializer(typeof(PlayerSaveData));
            var stream = new FileStream(savePath + "/player.save", FileMode.Open);
            playerSaveData = serializer.Deserialize(stream) as PlayerSaveData;
            stream.Close();
        }
        Destroy(GuiManager.instance.gameObject);
        PlayerManager.instance.playerStat.ClearDelegate();
        PlayerManager.instance.gameObject.SetActive(true);
        PlayerManager.instance.playerStat.isAlive = true;
        PlayerManager.instance.playerSaveData = playerSaveData;
        PlayerManager.instance.playerSaveData.isFromLoad = true;
        SceneManager.LoadScene(playerSaveData.currentScene);
    }
    public void LoadCurrentSceneSavePoint()
    {
        Time.fixedDeltaTime = 0.02f;
        Time.timeScale = 1f;
        if (SceneManager.GetActiveScene().name == "Tutorial_1_Forest_1")
        {
            PlayerManager.instance.gameObject.SetActive(true);
            PlayerManager.instance.playerStat.isAlive = true;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            return;
        }
        PlayerManager.instance.playerSaveData.isFromLoad = true;
        PlayerManager.instance.gameObject.SetActive(true);
        PlayerManager.instance.playerStat.isAlive = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
