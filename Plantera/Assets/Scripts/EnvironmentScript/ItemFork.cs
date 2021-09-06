using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemFork : MonoBehaviour,IInteractable
{
    [SerializeField] Collider2D collider2d;
    [SerializeField] Material noOutline;
    [SerializeField] Material haveOutline;
    [SerializeField] SpriteRenderer spriteRenderer;

    public void SetIteractable(bool value)
    {
        collider2d.enabled = value;
        
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            spriteRenderer.material = haveOutline;
            GameObject.FindGameObjectWithTag("ControllerManager").GetComponent<ControllerManager>().buttonPlant.SetInteractItemAction(InteractAction);
        }
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            spriteRenderer.material = noOutline;
            GameObject.FindGameObjectWithTag("ControllerManager").GetComponent<ControllerManager>().buttonPlant.RemoveInteractItemAction(InteractAction);
        }
    }
    public void InteractAction()
    {
        ControllerManager controllerManager = GameObject.FindGameObjectWithTag("ControllerManager").GetComponent<ControllerManager>();
        controllerManager.buttonPlant.SetModeAttack();
        controllerManager.buttonPlant.SetIsEnabledPlantAttack(true);

        controllerManager.playerManager.playerAnimation.EnableFork();

        PlayEventManager.instance.TriggerOnGatherEvent("ItemFork", 1);

        GameObject.FindGameObjectWithTag("ControllerManager").GetComponent<ControllerManager>().buttonPlant.RemoveInteractItemAction(InteractAction);
        Destroy(gameObject);
    }
}
