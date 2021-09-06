using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class characteristicsPanelScript : MonoBehaviour
{
    [SerializeField] Text duri;
    [SerializeField] Text getah;
    [SerializeField] Text bulu;
    [SerializeField] Text racun;
    [SerializeField] Text bau;
    [SerializeField] Text kulitTebal;

    private void OnEnable()
    {
        UpdateCharacteristics();
    }
    public void UpdateCharacteristics()
    {
        duri.text = "X " + PlayerManager.instance.playerInventory.SearchForItem(Characteristic.duri);
        getah.text = "X " + PlayerManager.instance.playerInventory.SearchForItem(Characteristic.getah);
        bulu.text = "X " + PlayerManager.instance.playerInventory.SearchForItem(Characteristic.buluhalus);
        racun.text = "X " + PlayerManager.instance.playerInventory.SearchForItem(Characteristic.racun);
        bau.text = "X " + PlayerManager.instance.playerInventory.SearchForItem(Characteristic.bau);
        kulitTebal.text = "X " + PlayerManager.instance.playerInventory.SearchForItem(Characteristic.kulittebal);

    }
}
