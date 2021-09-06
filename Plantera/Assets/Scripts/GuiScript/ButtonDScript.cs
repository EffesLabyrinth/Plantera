using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonDScript : MonoBehaviour
{
    [SerializeField] private ControllerManager controllerManager;
    public void ButtonPress()
    {
        controllerManager.playerManager.playerInventory.GatherMaterialFromGatherableMaterial();
        if (GuiManager.instance.characteristicPanel.gameObject.activeSelf) GuiManager.instance.characteristicPanel.UpdateCharacteristics();
        if (GuiManager.instance.plantPanel.gameObject.activeSelf) GuiManager.instance.plantPanel.UpdatePanel();
    }
}
