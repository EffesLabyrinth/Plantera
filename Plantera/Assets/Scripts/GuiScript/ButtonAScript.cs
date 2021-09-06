using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ButtonAScript : MonoBehaviour
{
    [SerializeField] private ControllerManager controllerManager;

    bool plantmode;

    public bool isEnabledPlantAttack = true;

    public GameObject icoPlant;
    public GameObject icoAttack;
    public GameObject plantPanel;

    //void Start()
    //{
    //    plantmode = true;
    //    icoPlant.SetActive(true);
    //    icoAttack.SetActive(false);
    //}

    public void ChangeMode()
    {
        if (plantmode) SetModeAttack();
        else SetModePlant();
    }
    public void SetModeAttack()
    {
        plantmode = false;
        icoPlant.SetActive(false);
        icoAttack.SetActive(true);
        plantPanel.SetActive(false);
        Time.fixedDeltaTime = 0.02f;
        Time.timeScale = 1f;
    }
    public void SetModePlant()
    {
        plantmode = true;
        icoPlant.SetActive(true);
        icoAttack.SetActive(false);
        Time.fixedDeltaTime = 0.02f;
        Time.timeScale = 1f;
    }

    public void ButtonPress()
    {
        if (isEnabledPlantAttack)
        {
            if (!plantmode)
            {
                PlayerManager.instance.playerController.Attack();
            }
            else
            {
                if (plantPanel.activeSelf)
                {
                    plantPanel.SetActive(false);
                    Time.fixedDeltaTime = 0.02f;
                    Time.timeScale = 1f;
                }
                else
                {
                    plantPanel.SetActive(true);
                    Time.timeScale = 0.1f;
                    Time.fixedDeltaTime = Time.timeScale * 0.02f;
                }
            }
        }
    }
    public void SetInteractItemAction(UnityAction interactActions)
    {
        icoPlant.SetActive(false);
        icoAttack.SetActive(false);

        Button button = GetComponent<Button>();
        button.onClick.AddListener(interactActions);
    }
    public void RemoveInteractItemAction(UnityAction interactActions)
    {
        Button button = GetComponent<Button>();
        button.onClick.RemoveListener(interactActions);
    }
    public void SetIsEnabledPlantAttack(bool value)
    {
        isEnabledPlantAttack = value;
        if (isEnabledPlantAttack)
        {
            if (plantmode)
            {
                icoPlant.SetActive(true);
                icoAttack.SetActive(false);
            }
            else
            {
                icoPlant.SetActive(false);
                icoAttack.SetActive(true);
            }
        }
        else
        {
            icoPlant.SetActive(false);
            icoAttack.SetActive(false);
        }
    }
}
