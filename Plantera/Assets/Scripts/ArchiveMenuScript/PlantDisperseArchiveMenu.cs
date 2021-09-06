using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlantDisperseArchiveMenu : MonoBehaviour
{
    [SerializeField] GameObject panel;
    [SerializeField] Text disperseName;
    [SerializeField] Text characteristicsDescription;
    [SerializeField] Text plantName;
    [SerializeField] Image plantImage;
    [SerializeField] Text plantDescription;
    [SerializeField] GameObject[] plantSelections;

    int currentWay = 0;
    [SerializeField] PlantArchiveData[] animal;
    [SerializeField] PlantArchiveData[] wind;
    [SerializeField] PlantArchiveData[] water;
    [SerializeField] PlantArchiveData[] explosion;
    [TextArea(20, 20)]
    [SerializeField] string[] characteristicsDescriptions;

    public void ViewDisperse(int characteristic)
    {
        panel.SetActive(true);
        characteristicsDescription.text = characteristicsDescriptions[characteristic];
        if (characteristic == 0)
        {
            currentWay = 0;
            disperseName.text = "Haiwan dan Manusia";
            for (int i = 0; i < plantSelections.Length; i++)
            {
                if (i < animal.Length)
                {
                    plantSelections[i].SetActive(true);
                    plantSelections[i].GetComponentInChildren<Text>().text = animal[i].plantName;
                }
                else plantSelections[i].SetActive(false);
            }
        }
        else if (characteristic == 1)
        {
            currentWay = 1;
            disperseName.text = "Angin";
            for (int i = 0; i < plantSelections.Length; i++)
            {
                if (i < wind.Length)
                {
                    plantSelections[i].SetActive(true);
                    plantSelections[i].GetComponentInChildren<Text>().text = wind[i].plantName;
                }
                else plantSelections[i].SetActive(false);
            }
        }
        else if (characteristic == 2)
        {
            currentWay = 2;
            disperseName.text = "Air";
            for (int i = 0; i < plantSelections.Length; i++)
            {
                if (i < water.Length)
                {
                    plantSelections[i].SetActive(true);
                    plantSelections[i].GetComponentInChildren<Text>().text = water[i].plantName;
                }
                else plantSelections[i].SetActive(false);
            }
        }
        else if (characteristic == 3)
        {
            currentWay = 3;
            disperseName.text = "Mekanisme Letupan";
            for (int i = 0; i < plantSelections.Length; i++)
            {
                if (i < explosion.Length)
                {
                    plantSelections[i].SetActive(true);
                    plantSelections[i].GetComponentInChildren<Text>().text = explosion[i].plantName;
                }
                else plantSelections[i].SetActive(false);
            }
        }
        ViewPlant(0);
    }
    public void ViewPlant(int value)
    {
        if (currentWay == 0)
        {
            plantName.text = animal[value].plantName;
            plantImage.sprite = animal[value].plantImage;
            plantDescription.text = animal[value].description;
        }
        else if (currentWay == 1)
        {
            plantName.text = wind[value].plantName;
            plantImage.sprite = wind[value].plantImage;
            plantDescription.text = wind[value].description;
        }
        else if (currentWay == 2)
        {
            plantName.text = water[value].plantName;
            plantImage.sprite = water[value].plantImage;
            plantDescription.text = water[value].description;
        }
        else if (currentWay == 3)
        {
            plantName.text = explosion[value].plantName;
            plantImage.sprite = explosion[value].plantImage;
            plantDescription.text = explosion[value].description;
        }
    }
    public void SetDispersePanel(bool value)
    {
        panel.SetActive(value);
    }
}
