using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlantPanelScript : MonoBehaviour
{
    [SerializeField] private ControllerManager controllerManager;
    public Button[] tiles;
    public TurretObject[] turretObjects;
    public GameObject[] characteristics;
    public GameObject[] charText;
    public GameObject nearTurret;
    
    private void OnEnable()
    {
        UpdatePanel();
    }

    public void PlantTurret(int tileNo)
    {
        turretObjects[tileNo].model.GetComponent<IPlantTurret>().PlantTurret(controllerManager.playerManager.transform.position);
        int count = turretObjects[tileNo].materialCosts.Length;
        PlayerInventory playerInventory = controllerManager.playerManager.playerInventory;
        for (int i = 0; i < count; i++)
        {
            playerInventory.RemoveItem(turretObjects[tileNo].materialCosts[i].material, turretObjects[tileNo].materialCosts[i].count);
        }
        controllerManager.buttonPlant.gameObject.GetComponent<Button>().interactable = false;
        PlayerManager.instance.playerAnimation.TriggerPlant();
        Invoke("SetPlantButtonInteract", 0.3f);
        controllerManager.buttonPlant.ButtonPress();
    }
    public void SetPlantButtonInteract()
    {
        controllerManager.buttonPlant.gameObject.GetComponent<Button>().interactable = true;
    }

    public void UpdatePanel()
    {
        bool[] unlockedPlant = PlayerManager.instance.playerStat.unlockedPlant;
        PlayerInventory playerInventory = controllerManager.playerManager.playerInventory;
        for (int i = 0; i < tiles.Length; i++)
        {
            if (unlockedPlant[i])
            {
                tiles[i].GetComponentInChildren<Text>().text = turretObjects[i].name;
                Image image = tiles[i].GetComponentsInChildren<Image>()[1];
                image.enabled = true;
                image.sprite = turretObjects[i].plantIco;

                bool isAvailable = true;
                int count = turretObjects[i].materialCosts.Length;
                for (int j = 0; j < count; j++)
                {
                    if (!playerInventory.CheckItemIsInInventory(turretObjects[i].materialCosts[j].material, turretObjects[i].materialCosts[j].count))
                    {
                        isAvailable = false;
                        break;
                    }
                }

                if (isAvailable) tiles[i].interactable = true;
                else tiles[i].interactable = false;

                characteristics[i].SetActive(true);
                charText[i].SetActive(true);
            }
            else
            {
                tiles[i].interactable = false;
                tiles[i].GetComponentInChildren<Text>().text = "Locked";
                tiles[i].GetComponentsInChildren<Image>()[1].enabled = false;
                characteristics[i].SetActive(false);
                charText[i].SetActive(false);
            }
        }
    }
    private void Update()
    {
        if (PlayerManager.instance.playerStat.unlockedPlant[3])
        {
            bool isAvailable = true;
            int count = turretObjects[3].materialCosts.Length;
            for (int j = 0; j < count; j++)
            {
                if (!PlayerManager.instance.playerInventory.CheckItemIsInInventory(turretObjects[3].materialCosts[j].material, turretObjects[3].materialCosts[j].count))
                {
                    isAvailable = false;
                    break;
                }
            }

            tiles[3].interactable = false;
            nearTurret = null;
            if (isAvailable)
            {
                Collider2D[] collider = PlayerManager.instance.playerController.CheckPlantCover();
                for (int i = 0; i < collider.Length; i++)
                {
                    PlantStat plant = collider[i].gameObject.GetComponent<PlantStat>();
                    if ( !plant.isCovered && plant.id != 3)
                    {
                        tiles[3].interactable = true;
                        nearTurret = collider[i].gameObject;
                        break;
                    }
                }
            }
        }
    }
}
