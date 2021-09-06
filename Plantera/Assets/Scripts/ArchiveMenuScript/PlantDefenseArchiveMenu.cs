using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlantDefenseArchiveMenu : MonoBehaviour
{
    [SerializeField] GameObject panel;
    [SerializeField] Image characteristicImage;
    [SerializeField] Text characteristicName;
    [SerializeField] Text plantName;
    [SerializeField] Image plantImage;
    [SerializeField] Text plantDescription;
    [SerializeField] GameObject[] plantSelections;
    [SerializeField] Sprite[] characteristicSprites;

    int currentCharacteristic = 0;
    [SerializeField] PlantArchiveData[] thorn;
    [SerializeField] PlantArchiveData[] latex;
    [SerializeField] PlantArchiveData[] finefur;
    [SerializeField] PlantArchiveData[] poison;
    [SerializeField] PlantArchiveData[] smelly;

    public void ViewDefenseCharacteristic(int characteristic)
    {
        panel.SetActive(true);
        if (characteristic == 0)
        {
            currentCharacteristic = 0;
            characteristicName.text = "Berduri";
            characteristicImage.sprite = characteristicSprites[0];
            for (int i = 0; i < plantSelections.Length; i++)
            {
                if(i < thorn.Length)
                {
                    plantSelections[i].SetActive(true);
                    plantSelections[i].GetComponentInChildren<Text>().text = thorn[i].plantName;
                }
                else plantSelections[i].SetActive(false);
            }
        }
        else if (characteristic == 1)
        {
            currentCharacteristic = 1;
            characteristicName.text = "Getah";
            characteristicImage.sprite = characteristicSprites[1];
            for (int i = 0; i < plantSelections.Length; i++)
            {
                if (i < latex.Length)
                {
                    plantSelections[i].SetActive(true);
                    plantSelections[i].GetComponentInChildren<Text>().text = latex[i].plantName;
                }
                else plantSelections[i].SetActive(false);
            }
        }
        else if (characteristic == 2)
        {
            currentCharacteristic = 2;
            characteristicName.text = "Bulu Halus";
            characteristicImage.sprite = characteristicSprites[2];
            for (int i = 0; i < plantSelections.Length; i++)
            {
                if (i < finefur.Length)
                {
                    plantSelections[i].SetActive(true);
                    plantSelections[i].GetComponentInChildren<Text>().text = finefur[i].plantName;
                }
                else plantSelections[i].SetActive(false);
            }
        }
        else if (characteristic == 3)
        {
            currentCharacteristic = 3;
            characteristicName.text = "Berbau Busuk";
            characteristicImage.sprite = characteristicSprites[3];
            for (int i = 0; i < plantSelections.Length; i++)
            {
                if (i < smelly.Length)
                {
                    plantSelections[i].SetActive(true);
                    plantSelections[i].GetComponentInChildren<Text>().text = smelly[i].plantName;
                }
                else plantSelections[i].SetActive(false);
            }
        }
        else if (characteristic == 4)
        {
            currentCharacteristic = 4;
            characteristicName.text = "Beracun";
            characteristicImage.sprite = characteristicSprites[4];
            for (int i = 0; i < plantSelections.Length; i++)
            {
                if (i < poison.Length)
                {
                    plantSelections[i].SetActive(true);
                    plantSelections[i].GetComponentInChildren<Text>().text = poison[i].plantName;
                }
                else plantSelections[i].SetActive(false);
            }
        }
        ViewPlant(0);
    }
    public void ViewPlant(int value)
    {
        if (currentCharacteristic == 0)
        {
            plantName.text = thorn[value].plantName;
            plantImage.sprite = thorn[value].plantImage;
            plantDescription.text = thorn[value].description;
        }
        else if(currentCharacteristic == 1)
        {
            plantName.text = latex[value].plantName;
            plantImage.sprite = latex[value].plantImage;
            plantDescription.text = latex[value].description;
        }
        else if (currentCharacteristic == 2)
        {
            plantName.text = finefur[value].plantName;
            plantImage.sprite = finefur[value].plantImage;
            plantDescription.text = finefur[value].description;
        }
        else if (currentCharacteristic == 3)
        {
            plantName.text = smelly[value].plantName;
            plantImage.sprite = smelly[value].plantImage;
            plantDescription.text = smelly[value].description;
        }
        else if (currentCharacteristic == 4)
        {
            plantName.text = poison[value].plantName;
            plantImage.sprite = poison[value].plantImage;
            plantDescription.text = poison[value].description;
        }
    }
    public void SetCharacteristicPanel(bool value)
    {
        panel.SetActive(value);
    }
}
