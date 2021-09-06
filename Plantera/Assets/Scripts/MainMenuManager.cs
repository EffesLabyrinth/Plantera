using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using System.Xml.Serialization;
public class MainMenuManager : MonoBehaviour
{
    public Animator pressAnywhereButton;
    public Animator gameTitleAndButtons;
    public GameObject buttons;
    public Button loadButton;
    public Button archiveButton;
    bool inMainMenu;
    public float inMainMenuDelay;
    float startInMainMenuDelay;
    public AudioClip levelClip;
    public AudioClip buttonClip;
    void Awake()
    {
        pressAnywhereButton.gameObject.SetActive(true);
        buttons.SetActive(false);
        inMainMenu = false;
    }
    private void Start()
    {
        if (PlayerManager.instance) Destroy(PlayerManager.instance.gameObject);
        if (GuiManager.instance) Destroy(GuiManager.instance.gameObject);
        if (CompanionManager.instance) Destroy(CompanionManager.instance.gameObject);
        if (PlayEventManager.instance) Destroy(PlayEventManager.instance.gameObject);

        string savePath = Application.persistentDataPath;
        if (File.Exists(savePath + "/player.save"))
        {
            loadButton.interactable = true;

            var serializer = new XmlSerializer(typeof(PlayerSaveData));
            var stream = new FileStream(savePath + "/player.save", FileMode.Open);
            PlayerSaveData playerSaveData = serializer.Deserialize(stream) as PlayerSaveData;
            stream.Close();

            archiveButton.interactable = playerSaveData.archiveEnabled;
        }
        else
        {
            loadButton.interactable = false;
            archiveButton.interactable = false;
        }

        SoundManager.instance.PlayMusic(levelClip);
    }
    public void PressAnywhere()
    {
        pressAnywhereButton.gameObject.SetActive(false);
        gameTitleAndButtons.SetTrigger("TitleFloatIn");
        startInMainMenuDelay = inMainMenuDelay;
        inMainMenu = true;
    }
    public void StartGameFloatIn()
    {
        if (inMainMenu)
        {
            SoundManager.instance.PlaySfx(buttonClip);
            gameTitleAndButtons.SetTrigger("StartGameFloatIn");
            inMainMenu = false;
            startInMainMenuDelay = 0;
        }
    }
    public void StartGameFloatOut()
    {
        if (!inMainMenu)
        {
            SoundManager.instance.PlaySfx(buttonClip);
            gameTitleAndButtons.SetTrigger("StartGameFloatOut");
            inMainMenu = true;
            startInMainMenuDelay = inMainMenuDelay;
        }
    }
    public void OptionPanelFloatIn()
    {
        if (inMainMenu)
        {
            SoundManager.instance.PlaySfx(buttonClip);
            gameTitleAndButtons.SetTrigger("OptionFloatIn");
            inMainMenu = false;
            startInMainMenuDelay = 0;
        }
    }
    public void OptionPanelFloatOut()
    {
        if (!inMainMenu)
        {
            SoundManager.instance.PlaySfx(buttonClip);
            gameTitleAndButtons.SetTrigger("OptionFloatOut");
            inMainMenu = true;
            startInMainMenuDelay = inMainMenuDelay;
        }
    }
    public void ExitGame()
    {
        string savePath = Application.persistentDataPath;
        DirectoryInfo dir = new DirectoryInfo(savePath);
        FileInfo[] info = dir.GetFiles("*.tempsave");
        foreach (FileInfo f in info)
        {
            File.Delete(f.FullName);
        }
        Application.Quit();
    }
    public void StartNewGame()
    {
        SoundManager.instance.PlaySfx(buttonClip);
        SceneManager.LoadScene("New_Game");
    }
    public void LoadGame()
    {
        SoundManager.instance.PlaySfx(buttonClip);
        SceneManager.LoadScene("Load_Game");
    }
    public void Archive()
    {
        SoundManager.instance.PlaySfx(buttonClip);
        SceneManager.LoadScene("Archive_Menu");
    }
    private void Update()
    {
        if (inMainMenu)
        {
            if (startInMainMenuDelay > 0) startInMainMenuDelay -= Time.deltaTime;
            else
            {
                gameTitleAndButtons.SetTrigger("TitleFloatOut");
                inMainMenu = false;
                Invoke("FloatOut", 0.5f);
            }
        }
    }
    public void FloatOut()
    {
        pressAnywhereButton.gameObject.SetActive(true);
    }
    public void ClickRefresh()
    {
        if(inMainMenu) startInMainMenuDelay = inMainMenuDelay;
    }
}
