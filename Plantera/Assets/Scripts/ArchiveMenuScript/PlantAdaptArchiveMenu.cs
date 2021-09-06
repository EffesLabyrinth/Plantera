using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlantAdaptArchiveMenu : MonoBehaviour
{
    [SerializeField] GameObject panel;
    [SerializeField] Text climateName;
    [SerializeField] Text characteristicsDescription;
    [SerializeField] Text plantName;
    [SerializeField] Image plantImage;
    [SerializeField] Text plantDescription;
    [SerializeField] GameObject[] plantSelections;

    int currentSeason = 0;
    [SerializeField] PlantArchiveData[] windy;
    [SerializeField] PlantArchiveData[] cold;
    [SerializeField] PlantArchiveData[] hot;
    [TextArea(20,20)]
    [SerializeField] string[] characteristicsDescriptions;

    public void ViewClimate(int characteristic)
    {
        panel.SetActive(true);
        characteristicsDescription.text = characteristicsDescriptions[characteristic];
        if (characteristic == 0)
        {
            currentSeason = 0;
            climateName.text = "Musim Angin Kencang";
            for (int i = 0; i < plantSelections.Length; i++)
            {
                if (i < windy.Length)
                {
                    plantSelections[i].SetActive(true);
                    plantSelections[i].GetComponentInChildren<Text>().text = windy[i].plantName;
                }
                else plantSelections[i].SetActive(false);
            }
        }
        else if (characteristic == 1)
        {
            currentSeason = 1;
            climateName.text = "Iklim Sejuk";
            for (int i = 0; i < plantSelections.Length; i++)
            {
                if (i < cold.Length)
                {
                    plantSelections[i].SetActive(true);
                    plantSelections[i].GetComponentInChildren<Text>().text = cold[i].plantName;
                }
                else plantSelections[i].SetActive(false);
            }
        }
        else if (characteristic == 2)
        {
            currentSeason = 2;
            climateName.text = "Iklim Panas dan Musim Kering";
            for (int i = 0; i < plantSelections.Length; i++)
            {
                if (i < hot.Length)
                {
                    plantSelections[i].SetActive(true);
                    plantSelections[i].GetComponentInChildren<Text>().text = hot[i].plantName;
                }
                else plantSelections[i].SetActive(false);
            }
        }
        ViewPlant(0);
    }
    public void ViewPlant(int value)
    {
        if (currentSeason == 0)
        {
            plantName.text = windy[value].plantName;
            plantImage.sprite = windy[value].plantImage;
            plantDescription.text = windy[value].description;
        }
        else if (currentSeason == 1)
        {
            plantName.text = cold[value].plantName;
            plantImage.sprite = cold[value].plantImage;
            plantDescription.text = cold[value].description;
        }
        else if (currentSeason == 2)
        {
            plantName.text = hot[value].plantName;
            plantImage.sprite = hot[value].plantImage;
            plantDescription.text = hot[value].description;
        }
    }
    public void SetClimatePanel(bool value)
    {
        panel.SetActive(value);
    }
}
